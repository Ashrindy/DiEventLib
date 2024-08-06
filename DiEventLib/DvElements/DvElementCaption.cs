using Amicitia.IO.Binary;
using DiEventLib.Misc;
using System.Text;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Caption", "Displays captions in cutscene")]
public class DvElementCaption : DvNodeElement
{
    [DvValue("Caption Name", DvValueType.String,16)]
    public string Name { get; set; }
    [DvValue("Language", DvValueType.Enum)]
    public Language Language { get; set; }

    public DvElementCaption() : base(DvElementID.Caption) { }
    public DvElementCaption(string name, Language language) : base(DvElementID.Caption)
    {
        Name = name;
        Language = language;
    }

    public DvElementCaption(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        //ElementRead(reader);
        Name = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 16);
        Language = reader.Read<Language>();
        reader.Skip(4);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, Name, 16);
        writer.Write(Language);
        writer.WriteNulls(4);
    }
}

public enum Language : uint
{
    English = 0,
    French = 1,
    Italian = 2,
    German = 3,
    Spanish = 4,
    Polish = 5,
    Portuguese = 6,
    Russian = 7,
    Japanese = 8,
    Chinese = 9,
    Chinese_Simplified = 10,
    Korean = 11,
};
