using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementOverrideASM : DvNodeObject
{
    public string ASMName1 { get; set; }
    public string ASMName2 { get; set; }
    public DvElementOverrideASM() { }
    public DvElementOverrideASM(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        ASMName1 = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 64);
        ASMName2 = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 64);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, ASMName1, 64);
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, ASMName2, 64);
    }
}
