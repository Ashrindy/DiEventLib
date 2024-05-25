using Amicitia.IO.Binary;
using System.Text;
namespace DiEventLib;

public abstract class DvNodeObject : IBinarySerializable
{
    public abstract void Read(BinaryObjectReader reader);

    public abstract void Write(BinaryObjectWriter writer);
}

public abstract class DvNode : IBinarySerializable
{
    public Guid Guid { get; set; } = Guid.NewGuid();
    public DvNodeCategory Category { get; set; } = DvNodeCategory.DummyNode;
    public int Count { get; set; } = 0;
    public int NodeFlags { get; set; } = 0;
    public int Priority { get; set; } = 0;
    public string Name { get; set; } = "";
    public List<DvNode> Children { get; set; } = new();
    //public DvNodeObject NodeObject { get; set; }

    public abstract void Read(BinaryObjectReader reader);
    public abstract void Write(BinaryObjectWriter writer);


    protected void NodeRead(BinaryObjectReader reader)
    {
        Guid = reader.Read<Guid>();
        Category = reader.Read<DvNodeCategory>();
        var nodeSize = reader.Read<int>() * 4;
        Count = reader.Read<int>();
        NodeFlags = reader.Read<int>();
        Priority = reader.Read<int>();
        reader.Skip(12);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Name = reader.ReadString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, 64);
        //Console.WriteLine($"{Name} ({Category})");

        //ChildNodes.AddRange(reader.ReadObjectArray<DvNode>(childCount));
    }

    protected void NodeWrite(BinaryObjectWriter writer)
    {
        writer.Write(Guid);
        writer.Write(Category);
        var nodeSizePos = writer.Position;
        writer.WriteNulls(4);
        writer.Write(Children.Count);
        writer.Write(NodeFlags);
        writer.Write(Priority);
        writer.WriteNulls(12);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        writer.WriteString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, Name, 64);

        long preWritePos = writer.Position;
        //NodeObject.Write(writer);
        long postWritePos = writer.Position;

        writer.Seek(nodeSizePos, SeekOrigin.Begin);
        writer.Write((int)(postWritePos - preWritePos) / 4);
        writer.Seek(postWritePos, SeekOrigin.Begin);

        writer.WriteObjectCollection(Children);
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