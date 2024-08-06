namespace DiEventLib.Misc;

[AttributeUsage(AttributeTargets.Class)]
public class DvNodeDescriptionAttribute : Attribute
{
    public string NodeName { get; }
    public string Description { get; }

    public DvNodeDescriptionAttribute(string nodeName, string description)
    {
        NodeName = nodeName;
        Description = description;
    }
}
