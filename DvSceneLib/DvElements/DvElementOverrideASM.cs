using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Override ASM", "Override the ASM file for a certain amount of time")]
public class DvElementOverrideASM : DvNodeElement
{
    public string ASMName = "";
    public string TargetASMName = "";

    public DvElementOverrideASM() : base(DvElementID.OverrideASM) { }
    public DvElementOverrideASM(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        ASMName = reader.ReadDvString(Utils.StringEncoding.Default);
        TargetASMName = reader.ReadDvString(Utils.StringEncoding.Default);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.WriteDvString(ASMName, Utils.StringEncoding.Default);
        writer.WriteDvString(TargetASMName, Utils.StringEncoding.Default);
    }
}
