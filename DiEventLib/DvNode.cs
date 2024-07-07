using Amicitia.IO.Binary;
using System.IO;
using System.Text;
using System.Xml.Linq;
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
    public int NodeFlags { get; set; }
    public int Priority { get; set; }
    public string NodeName { get; set; }
    public List<DvNode> Children { get; set; } = new();

    public DvNode(BinaryObjectReader reader)
    {
        Read(reader);
        
    }

    public DvNode()
    {
    }

    public DvNode(DvNodeCategory category, string name)
    {
        Category = category;
        NodeName = name;
    }

    public DvNode(DvNodeCategory category)
    {
        Category = category;
    }


    public void Read(BinaryObjectReader reader) 
    {
        Guid = reader.Read<Guid>();
        Category = reader.Read<DvNodeCategory>();
        NodeSize = reader.Read<int>() * 4;
        ChildCount = reader.Read<int>();
        NodeFlags = reader.Read<int>();
        Priority = reader.Read<int>();
        reader.Skip(12);
        NodeName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
    }

    public T AddChild<T>() where T : DvNode, new()
    {
        var node = new T();
        Children.Add(node);
        ChildCount = Children.Count;
        return node;
    }

    public void AddChild(DvNode node)
    {
        Children.Add(node);
        ChildCount = Children.Count;
    }

    public void Write(BinaryObjectWriter writer) { }
  

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
        writer.WriteDvString(NodeName, Utils.StringEncoding.ShiftJIS);

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
