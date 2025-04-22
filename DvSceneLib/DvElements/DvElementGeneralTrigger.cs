using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Text;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("General Trigger", "Triggers pre-defined events")]
public class DvElementGeneralTrigger : DvNodeElement
{
    public uint Field_00 = 0;
    public string TriggerName = "";

    public DvElementGeneralTrigger() : base(DvElementID.GeneralTrigger) { }
    public DvElementGeneralTrigger(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        TriggerName = reader.ReadString(Encoding.UTF8, StringBinaryFormat.FixedLength, 64);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, TriggerName, 64);
    }
}
