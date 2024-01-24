using Amicitia.IO.Binary;
using System;
using System.Text;
using System.Collections.Generic;
namespace DiEventLib;

public abstract class DvNodeObject : IBinarySerializable
{
    public abstract void Read(BinaryObjectReader reader);

    public abstract void Write(BinaryObjectWriter writer);
}


public class DvNode : IBinarySerializable
{
    public Guid Guid { get; set; }
    public DvNodeCategory Category { get; set; }
    public int NodeSize { get; set; }
    public int ChildCount { get; set; }
    public int Flags { get; set; }
    public int Priority { get; set; }
    public string Name { get; set; }
    List<DvNode> ChildNodes { get; set; } = new();
    public DvNodeObject NodeObject { get; set; } = new DvNodePath();

    public void Read(BinaryObjectReader reader)
    {
        Guid = reader.Read<Guid>();
        Category = reader.Read<DvNodeCategory>();
        NodeSize = reader.Read<int>() * 4;
        ChildCount = reader.Read<int>();
        Flags = reader.Read<int>();
        Priority = reader.Read<int>();
        reader.Skip(12);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Name = reader.ReadString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, 64);

        switch (Category)
        {
            case DvNodeCategory.Path:
                NodeObject = new DvNodePath(reader);
                break;

            //case DvNodeCategory.PathMotion:

            case DvNodeCategory.Camera:
                NodeObject = new DvNodeCamera(reader);
                break;

            case DvNodeCategory.CameraMotion:
                NodeObject = new DvNodeCameraMotion(reader);
                break;

            case DvNodeCategory.Character:
                NodeObject = new DvNodeCharacter(reader);
                break;

            case DvNodeCategory.CharacterMotion:
                NodeObject = new DvNodeCharacterMotion(reader);
                break;
            //case DvNodeCategory.CharacterBehavior:

            case DvNodeCategory.ModelCustom:
                NodeObject = new DvNodeModelCustom(reader);
                break;

            //case DvNodeCategory.Asset:
            case DvNodeCategory.MotionModel:
                NodeObject = new DvNodeMotionModel(reader);
                break;
            case DvNodeCategory.ModelNode:
                NodeObject = new DvNodeModelNode(reader);
                break;

            case DvNodeCategory.Element:
                NodeObject = new DvNodeElement(reader, NodeSize);
                break;

            default:
                reader.Skip(NodeSize);
                break;
        }

        ChildNodes.AddRange(reader.ReadObjectArray<DvNode>(ChildCount));
    }

    public void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }

}

public enum DvNodeCategory : uint
{
    DummyNode = 0,
    RootPath = 0,
    Path = 1,
    PathMotion,
    Camera,
    CameraMotion,
    Character,
    CharacterMotion,
    CharacterBehavior,
    ModelCustom,
    Asset,
    MotionModel,
    ModelNode,
    Element,
    // Not used in Sonic Frontiers
    Stage,
    StageScenarioFlag,
    InstanceMotion,
    InstanceMotionData,
    FolderCondition,
    CharacterBehaviorSimpleTalk,
    InvalidNode = 0xFFFFFFFF
};