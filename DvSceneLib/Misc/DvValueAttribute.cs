namespace DvSceneLib.Misc;

[AttributeUsage(AttributeTargets.All)]
public class DvValueAttribute : Attribute
{
    public string? GroupName { get; }
    public string ValueName { get; }
    public DvValueType ValueType { get; }
    public int? StringSize { get; }

    public DvValueAttribute (string valueName, DvValueType valueType)
    {
        ValueName = valueName;
        ValueType = valueType;
    }

    public DvValueAttribute(string valueName, DvValueType valueType, int stringSize) : this(valueName, valueType)
    {
        StringSize = stringSize;
    }

    public DvValueAttribute (string groupName, string valueName, DvValueType valueType) : this(valueName, valueType)
    {
        GroupName = groupName;
    }

    public DvValueAttribute(string groupName, string valueName, DvValueType valueType, int stringSize) : this(groupName, valueName, valueType)
    {
        StringSize = stringSize;
    }
}

public enum DvValueType
{
    String,
    Enum,
    Color8,
    ColorF,
    Vector3,
    Int,
    Float,
}