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

public class DvNode
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
}

public enum DvNodeCategory : uint
{
    // TODO: figure out 0 value
    DummyNode = 0,
    //RootPath = 0,
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
