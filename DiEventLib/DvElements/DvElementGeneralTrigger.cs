using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementGeneralTrigger : DvNodeObject
{
    public uint Field_00 { get; set; }
    public string TriggerName { get; set; }

    public DvElementGeneralTrigger() { }
    public DvElementGeneralTrigger(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        TriggerName = reader.ReadString(Encoding.UTF8, StringBinaryFormat.FixedLength, 64);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, TriggerName, 64);
    }
}
