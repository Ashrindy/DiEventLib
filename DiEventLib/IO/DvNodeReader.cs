using Amicitia.IO.Binary;
using DiEventLib.IO.Template;
using System.IO;
using System.Text;
using System.Xml.Linq;

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
            case DvNodeCategory.Character:
                node = new DvNodeCharacter(reader);
                break;
            case DvNodeCategory.CharacterMotion:
                node = new DvNodeCharacterMotion(reader);
                break;
            //case DvNodeCategory.CharacterBehavior:break;
            case DvNodeCategory.ModelCustom:
                node = new DvNodeModelCustom(reader);
                break;
            //case DvNodeCategory.Asset: break;
            case DvNodeCategory.MotionModel:
                node = new DvNodeMotionModel(reader);
                break;
            case DvNodeCategory.ModelNode:
                node = new DvNodeModelNode(reader);
                break;
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
                    case DvElementID.DrawOff:
                        element = new DvElementDrawOff(reader);
                        break;
                    case DvElementID.PathAdjustment:
                        element = new DvElementPathAdjustment(reader);
                        break;
                    case DvElementID.CameraShake:
                        element = new DvElementCameraShake(reader);
                        break;
                    case DvElementID.CameraShakeLoop:
                        element = new DvElementCameraShakeLoop(reader);
                        break;
                    case DvElementID.Effect:
                        element = new DvElementEffect(reader);
                        break;
                    case DvElementID.PathInterpolation:
                        element = new DvElementPathInterpolation(reader);
                        break;
                    case DvElementID.Culling:
                        element = new DvElementCulling(reader);
                        break;
                    case DvElementID.NearFarSetting:
                        element = new DvElementNearFarSetting(reader);
                        break;
                    case DvElementID.UVAnimation:
                        element = new DvElementUVAnimation(reader);
                        break;
                    case DvElementID.VisibilityAnimation:
                        element = new DvElementVisibilityAnimation(reader);
                        break;
                    case DvElementID.MaterialAnimation:
                        element = new DvElementMaterialAnimation(reader);
                        break;
                    case DvElementID.CompositeAnimation:
                        element = new DvElementCompositeAnimation(reader);
                        break;
                    case DvElementID.CameraOffset:
                        element = new DvElementCameraOffset(reader);
                        break;
                    //case DvElementID.ModelFade: break;
                    case DvElementID.SonicCamera:
                        element = new DvElementSonicCamera(reader);
                        break;
                    case DvElementID.GameCamera:
                        element = new DvElementGameCamera(reader);
                        break;
                    case DvElementID.VAT:
                        element = new DvElementVAT(reader);
                        break;
                    //case DvElementID.Spotlight: break;
                    //case DvElementID.SpotlightModel:
                    //    element = new DvElementSpotlightModel(reader);
                    //    break;
                    //case DvElementID.Bloom: break;
                    case DvElementID.DOF:
                        element = new DvElementDOF(reader);
                        break;
                    case DvElementID.ColorContrast:
                        element = new DvElementColorContrast(reader);
                        break;
                    case DvElementID.CameraExposure:
                        element = new DvElementCameraExposure(reader);
                        break;
                    case DvElementID.ShadowResolution:
                        element = new DvElementShadowResolution(reader);
                        break;
                    case DvElementID.AtmosphereHeightFogParam:
                        element = new DvElementAtmosphereHeightFogParam(reader);
                        break;
                    case DvElementID.ChromaticAberrationFilter:
                        element = new DvElementChromaticAberrationFilter(reader);
                        break;
                    case DvElementID.Vignette:
                        element = new DvElementVignette(reader);
                        break;
                    case DvElementID.Fade:
                        element = new DvElementFade(reader);
                        break;
                    case DvElementID.LetterBox:
                        element = new DvElementLetterBox(reader);
                        break;
                    case DvElementID.ModelClipping:
                        element = new DvElementModelClipping(reader);
                        break;
                    case DvElementID.BossName:
                        element = new DvElementBossName(reader);
                        break;
                    case DvElementID.Caption:
                        element = new DvElementCaption(reader);
                        break;
                    case DvElementID.Sound:
                        element = new DvElementSound(reader);
                        break;
                    case DvElementID.Time:
                        element = new DvElementTime(reader);
                        break;
                    case DvElementID.Sun:
                        element = new DvElementSun(reader);
                        break;
                    case DvElementID.LookAtIK:
                        element = new DvElementLookAtIK(reader);
                        break;
                    case DvElementID.CameraBlur:
                        element = new DvElementCameraBlur(reader);
                        break;
                    case DvElementID.GeneralTrigger:
                        element = new DvElementGeneralTrigger(reader);
                        break;
                    case DvElementID.Dither:
                        element = new DvElementDither(reader);
                        break;
                    case DvElementID.QTE:
                        element = new DvElementQTE(reader);
                        break;
                    case DvElementID.FacialAnimation:
                        element = new DvElementFacialAnimation(reader);
                        break;
                    //case DvElementID.OverrideASM: break;
                    case DvElementID.Aura:
                        element = new DvElementAura(reader);
                        break;
                    case DvElementID.ChangeTimeScale:
                        element = new DvElementChangeTimeScale(reader);
                        break;
                    case DvElementID.CyberSpaceNoise:
                        element = new DvElementCyberSpaceNoise(reader);
                        break;
                    case DvElementID.AuraRoad:
                        element = new DvElementAuraRoad(reader);
                        break;
                    case DvElementID.MovieView:
                        element = new DvElementMovieView(reader);
                        break;
                    //case DvElementID.CrossFade: break;
                    case DvElementID.Weather:
                        element = new DvElementWeather(reader);
                        break;
                    //case DvElementID.ShadowMapParam: break;
                    case DvElementID.VariablePointLight:
                        element = new DvElementVariablePointLight(reader);
                        break;
                    case DvElementID.OpeningLogo:
                        element = new DvElementOpeningLogo(reader);
                        break;
                    case DvElementID.AdditionRange:
                        element = new DvElementAdditionRange(reader);
                        break;
                    //case DvElementID.FxColUpdate: break;
                    case DvElementID.TheEndCableObject:
                        element = new DvElementTheEndCableObject(reader);
                        break;
                    case DvElementID.RifleBeastLighting:
                        element = new DvElementRifleBeastLighting(reader);
                        break;
                    default:
                        Console.WriteLine($"Not implemented element: {elementID.ToString()} (Name: {nodeName}, GUID: {guid}, Size: {nodeSize - 32}). SKIPPING");
                        reader.Skip(nodeSize - 32);
                        break;
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
                Console.WriteLine($"Not implemented category: {category.ToString()} (Name: {nodeName}, GUID: {guid}, Size: {nodeSize - 32})");
                reader.Skip(nodeSize - 32);
                break;
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

    public static DvNode ReadNode(BinaryObjectReader reader, DiEventDataBase db)
    {
        var guid = reader.Read<Guid>();
        var category = reader.Read<int>();
        var nodeSize = reader.Read<int>() * 4;
        var childCount = reader.Read<int>();
        var nodeFlags = reader.Read<int>();
        var priority = reader.Read<int>();
        reader.Skip(12);
        var nodeName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        var node = new DvNodeTemplate();

        DiEventDataBase.Node dbNode = db.Nodes.Find(x => x.NodeCategory == (int)category);
        if(dbNode == null)
        {
            Console.WriteLine($"Not implemented category: {category.ToString()} (Name: {nodeName}, GUID: {guid}, Size: {nodeSize})");
            reader.Skip(nodeSize);
        }
        else
        {
            node.Category = dbNode.FullName;
            if (dbNode.Name == "Element")
            {
                node = new DvElementTemplate();
                node.Category = dbNode.FullName;
                node.Read(reader, dbNode);
                DiEventDataBase.Node dbElem = db.Elements.Find(x => x.NodeCategory == (int)node.Fields["Element ID"].Value);
                if (dbElem == null)
                {
                    Console.WriteLine($"Not implemented element: {((int)node.Fields["Element ID"].Value).ToString()} (Name: {nodeName}, GUID: {guid}, Size: {nodeSize - 32}). SKIPPING");
                    reader.Skip(nodeSize - 32);
                }
                else
                {
                    ((DvElementTemplate)node).ElementName = dbElem.FullName;
                    ((DvElementTemplate)node).ReadElement(reader, dbElem);
                }
            }
            else
                node.Read(reader, dbNode);
        }

        node.Guid = guid;
        node.NodeSize = nodeSize;
        node.NodeFlags = nodeFlags;
        node.Priority = priority;
        node.NodeName = nodeName;

        for (int i = 0; i < childCount; i++)
        {
            var childNode = ReadNode(reader, db);
            node.ChildNodes.Add(childNode);
        }

        return node;
    }
}
