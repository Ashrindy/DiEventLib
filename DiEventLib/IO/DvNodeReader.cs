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
            //case DvNodeCategory.RootPath:
            //    break;
            case DvNodeCategory.Path: node = new DvNodePath(reader); break;
            case DvNodeCategory.PathMotion: break;
            case DvNodeCategory.Camera: node = new DvNodeCamera(reader); break;
            case DvNodeCategory.CameraMotion: node = new DvNodeCameraMotion(reader); break;
            case DvNodeCategory.Character: break;
            case DvNodeCategory.CharacterMotion: break;
            case DvNodeCategory.CharacterBehavior:break;
            case DvNodeCategory.ModelCustom: break;
            case DvNodeCategory.Asset: break;
            case DvNodeCategory.MotionModel: break;
            case DvNodeCategory.ModelNode: break;
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
                    case DvElementID.Caption:       element = new DvElementCaption(reader);     break;
                    case DvElementID.Fade:          element = new DvElementFade(reader);        break;
                    case DvElementID.GameCamera:    element = new DvElementGameCamera(reader);  break;
                    case DvElementID.MovieView:     element = new DvElementMovieView(reader);   break;
                    case DvElementID.LetterBox:     element = new DvElementLetterBox(reader);   break;
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
            case DvNodeCategory.Stage: break;
            case DvNodeCategory.StageScenarioFlag: break;
            case DvNodeCategory.InstanceMotion: break;
            case DvNodeCategory.InstanceMotionData: break;
            case DvNodeCategory.FolderCondition: break;
            case DvNodeCategory.CharacterBehaviorSimpleTalk: break;
            case DvNodeCategory.InvalidNode: break;
            default:
                throw new NotSupportedException();
        }

        node.Guid = guid;
        node.Category = category;
        node.NodeSize = nodeSize;
        node.ChildCount = childCount;
        node.NodeFlags = nodeFlags;
        node.Priority = priority;
        node.NodeName = nodeName;

        for (int i = 0; i < childCount; i++)
        {
            var childNode = ReadNode(reader);
            node.Children.Add(childNode);
        }

        return node;
    }
}
