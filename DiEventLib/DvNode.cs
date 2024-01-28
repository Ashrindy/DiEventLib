using Amicitia.IO.Binary;
using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
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
    public int Flags { get; set; }
    public int Priority { get; set; }
    public string Name { get; set; }
    public List<DvNode> ChildNodes { get; set; } = new();
    public DvNodeObject NodeObject { get; set; } = new DvNodePath();

    public void Read(BinaryObjectReader reader)
    {
        Guid = reader.Read<Guid>();
        Category = reader.Read<DvNodeCategory>();
        var nodeSize = reader.Read<int>() * 4;
        var childCount = reader.Read<int>();
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
                NodeObject = new DvNodeElement(reader, nodeSize);
                break;

            default:
                reader.Skip(nodeSize);
                break;
        }

        Console.WriteLine($"{Name} ({Category})");

        ChildNodes.AddRange(reader.ReadObjectArray<DvNode>(childCount));
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Guid);
        writer.Write(Category);
        var nodeSizePos = writer.Position;
        writer.WriteNulls(4);
        writer.Write(ChildNodes.Count);
        writer.Write(Flags);
        writer.Write(Priority);
        writer.WriteNulls(12);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        writer.WriteString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, Name, 64);

        long preWritePos = writer.Position;
        NodeObject.Write(writer);
        long postWritePos = writer.Position;

        writer.Seek(nodeSizePos, SeekOrigin.Begin);
        writer.Write((int)(postWritePos - preWritePos) / 4);
        writer.Seek(postWritePos, SeekOrigin.Begin);

        writer.WriteObjectCollection(ChildNodes);
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