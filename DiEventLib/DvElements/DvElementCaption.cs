using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementCaption : DvNodeObject
{
    public string Name { get; set; }
    public Language Language { get; set; }

    public DvElementCaption() { }
    public DvElementCaption(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Name = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 16);
        Language = reader.Read<Language>();
        reader.Skip(4);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
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
