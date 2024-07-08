using Amicitia.IO.Binary;

namespace DiEventLib;

public static class DvNodeWriter
{
    public static void WriteNode(this DvNode node, BinaryObjectWriter writer)
    {
        writer.Write(node.Guid);
        writer.Write(node.Category);
        var nodeSizePos = writer.Position;
        writer.WriteNulls(4);
        writer.Write(node.ChildNodes.Count);
        writer.Write(node.NodeFlags);
        writer.Write(node.Priority);
        writer.WriteNulls(12);
        writer.WriteDvString(node.NodeName, Utils.StringEncoding.ShiftJIS);

        long preWritePos = writer.Position;

        //node.WriteNode()
        
        switch (node.Category)
        {
            case DvNodeCategory.DummyNode: break;
            //case DvNodeCategory.RootPath: break;
            case DvNodeCategory.Path:
                var path = node as DvNodePath;
                path.Write(writer);
                break;
            case DvNodeCategory.Camera:
                var camera = node as DvNodeCamera;
                camera.Write(writer);
                break;
            case DvNodeCategory.CameraMotion:
                var cameraMotion = node as DvNodeCameraMotion;
                cameraMotion.Write(writer);
                break;
            //case DvNodeCategory.Character: break;
            //case DvNodeCategory.CharacterMotion: break;
            //case DvNodeCategory.CharacterBehavior: break;
            //case DvNodeCategory.ModelCustom: break;
            //case DvNodeCategory.Asset: break;
            //case DvNodeCategory.MotionModel: break;
            //case DvNodeCategory.ModelNode: break;
            case DvNodeCategory.Element:
                var element = node as DvNodeElement;
                element.Write(writer);
                switch (element.ElementID)
                {
                    //case DvElementID.DrawOff:   break;
                    //case DvElementID.PathAdjustment: break;
                    //case DvElementID.CameraShake: break;
                    //case DvElementID.CameraShakeLoop: break;
                    //case DvElementID.Effect: break;
                    //case DvElementID.PathInterpolation: break;
                    //case DvElementID.Culling: break;
                    //case DvElementID.NearFarSetting: break;
                    //case DvElementID.UVAnimation: break;
                    //case DvElementID.VisibilityAnimation: break;
                    //case DvElementID.MaterialAnimation: break;
                    //case DvElementID.CompositeAnimation: break;
                    //case DvElementID.CameraOffset: break;
                    //case DvElementID.ModelFade: break;
                    //case DvElementID.SonicCamera: break;
                    case DvElementID.GameCamera:
                        var gameCamera = element as DvElementGameCamera;
                        gameCamera.Write(writer);
                        break;
                    //case DvElementID.VertexAnimation: break;
                    //case DvElementID.Spotlight: break;
                    //case DvElementID.SpotlightModel: break;
                    //case DvElementID.Bloom: break;
                    //case DvElementID.DOF: break;
                    //case DvElementID.ColorContrast: break;
                    //case DvElementID.CameraExposure: break;
                    //case DvElementID.ShadowResolution: break;
                    //case DvElementID.AtmosphereHeightFogParam: break;
                    //case DvElementID.ChromaticAberrationFilter: break;
                    //case DvElementID.VignetteParam: break;
                    case DvElementID.Fade:
                        var fade = element as DvElementFade;
                        fade.Write(writer);
                        break;
                    case DvElementID.LetterBox:
                        var letterBox = element as DvElementLetterBox;
                        letterBox.Write(writer);
                        break;
                    //case DvElementID.ModelClipping: break;
                    //case DvElementID.BossName: break;
                    case DvElementID.Caption:
                        var caption = element as DvElementCaption;
                        caption.Write(writer);
                        break;
                    //case DvElementID.Sound: break;
                    //case DvElementID.Time: break;
                    //case DvElementID.Sun: break;
                    //case DvElementID.LookAtIK: break;
                    //case DvElementID.CameraBlurParam: break;
                    //case DvElementID.GeneralTrigger: break;
                    //case DvElementID.DitherParam: break;
                    //case DvElementID.QTE: break;
                    //case DvElementID.LipAnimation: break;
                    //case DvElementID.OverrideASM: break;
                    //case DvElementID.Aura: break;
                    //case DvElementID.ChangeTimeScale: break;
                    //case DvElementID.CyberSpaceNoise: break;
                    //case DvElementID.AuraRoad: break;
                    case DvElementID.MovieView:
                        var movieView = element as DvElementMovieView;
                        movieView.Write(writer);
                        break;
                    //case DvElementID.CrossFade: break;
                    //case DvElementID.Weather: break;
                    //case DvElementID.ShadowMapParam: break;
                    //case DvElementID.VariablePointLight: break;
                    //case DvElementID.OpeningLogo: break;
                    //case DvElementID.AdditionRange: break;
                    //case DvElementID.TheEndCableObject: break;
                    //case DvElementID.RifleBeastLighting: break;
                    default:
                        throw new NotSupportedException($"Not implemented element: {element.ElementID.ToString()}");

                }
                break;
            //case DvNodeCategory.Stage: break;
            //case DvNodeCategory.StageScenarioFlag: break;
            //case DvNodeCategory.InstanceMotion: break;
            //case DvNodeCategory.InstanceMotionData: break;
            //case DvNodeCategory.FolderCondition: break;
            //case DvNodeCategory.CharacterBehaviorSimpleTalk: break;
            //case DvNodeCategory.InvalidNode: break;
            default:
                throw new NotSupportedException($"Not implemented category: {node.Category.ToString()}");
        }

        long postWritePos = writer.Position;

        writer.Seek(nodeSizePos, SeekOrigin.Begin);
        writer.Write((int)(postWritePos - preWritePos) / 4);
        writer.Seek(postWritePos, SeekOrigin.Begin);

        foreach (var child in node.ChildNodes)
        {
            child.WriteNode(writer);
        }

    }
}
