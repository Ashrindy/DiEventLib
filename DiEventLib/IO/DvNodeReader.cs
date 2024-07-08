using Amicitia.IO.Binary;
using System.IO;
using System.Text;

namespace DiEventLib;

public static class DvNodeReader
{
    public static DvNode ReadNode(BinaryObjectReader reader)
    { 
        var guid = reader.Read<Guid>();
        var category = reader.Read<DvNodeCategory>();
        var nodeSize = reader.Read<int>() * 4;
        var childCount = reader.Read<int>();
        var nodeFlags = reader.Read<int>();
        var priority = reader.Read<int>();
        reader.Skip(12);
        var nodeName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        var node = new DvNode();

        switch (category)
        {
            case DvNodeCategory.DummyNode: break;
            //case DvNodeCategory.RootPath: break;
            case DvNodeCategory.Path:
                node = new DvNodePath(reader);
                break;
            case DvNodeCategory.PathMotion:
                break;
            case DvNodeCategory.Camera:
                node = new DvNodeCamera(reader);
                break;
            case DvNodeCategory.CameraMotion:
                node = new DvNodeCameraMotion(reader);
                break;
            //case DvNodeCategory.Character: break;
            //case DvNodeCategory.CharacterMotion: break;
            //case DvNodeCategory.CharacterBehavior:break;
            //case DvNodeCategory.ModelCustom: break;
            //case DvNodeCategory.Asset: break;
            //case DvNodeCategory.MotionModel: break;
            //case DvNodeCategory.ModelNode: break;
            case DvNodeCategory.Element:

                var element = new DvNodeElement();

                var elementID = reader.Read<DvElementID>();
                var start = reader.Read<float>();
                var end = reader.Read<float>();
                var version = reader.Read<int>();
                var flags = reader.Read<uint>();
                var playType = reader.Read<ElementPlayType>();
                var updateTiming = reader.Read<ElementUpdateTiming>();
                reader.Skip(4);

                switch (elementID)
                {
                    //case DvElementID.DrawOff: break;
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
                        element = new DvElementGameCamera(reader);
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
                        element = new DvElementFade(reader);
                        break;
                    case DvElementID.LetterBox:
                        element = new DvElementLetterBox(reader);
                        break;
                    //case DvElementID.ModelClipping: break;
                    //case DvElementID.BossName: break;
                    case DvElementID.Caption:
                        element = new DvElementCaption(reader);
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
                        element = new DvElementMovieView(reader);
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
                        throw new NotSupportedException($"Not implemented element: {elementID.ToString()}");
                }
                element.ElementID = elementID;
                element.Start = start;
                element.End = end;
                element.Version = version;
                element.Flags = flags;
                element.PlayType = playType;
                element.UpdateTiming = updateTiming;

                node = element;

                break;
            //case DvNodeCategory.Stage: break;
            //case DvNodeCategory.StageScenarioFlag: break;
            //case DvNodeCategory.InstanceMotion: break;
            //case DvNodeCategory.InstanceMotionData: break;
            //case DvNodeCategory.FolderCondition: break;
            //case DvNodeCategory.CharacterBehaviorSimpleTalk: break;
            //case DvNodeCategory.InvalidNode: break;
            default:
                throw new NotSupportedException($"Not implemented category: {category.ToString()}");
        }

        node.Guid = guid;
        node.Category = category;
        node.NodeSize = nodeSize;
        node.NodeFlags = nodeFlags;
        node.Priority = priority;
        node.NodeName = nodeName;

        for (int i = 0; i < childCount; i++)
        {
            var childNode = ReadNode(reader);
            node.ChildNodes.Add(childNode);
        }

        return node;
    }
}
