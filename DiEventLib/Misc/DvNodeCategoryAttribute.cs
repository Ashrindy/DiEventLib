namespace DiEventLib.Misc;

[AttributeUsage(AttributeTargets.Class)]
public class DvNodeCategoryAttribute : Attribute
{
    public string CategoryName { get; }

    public DvNodeCategoryAttribute(string categoryName)
    {
        CategoryName = categoryName;
    }
}
