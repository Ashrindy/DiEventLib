using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Lip Animation", "Adds animation to the characters lips")]
public class DvElementLipAnimation : DvNodeElement
{
    public int Unk0 = 0;
    public string FileName = "";
    public int Unk1 = 0;

    public DvElementLipAnimation() : base(DvElementID.LipAnimation) { }
    public DvElementLipAnimation(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Unk0 = reader.Read<int>();
        FileName = reader.ReadDvString(Utils.StringEncoding.Default);
        Unk1 = reader.Read<int>();
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Unk0);
        writer.WriteDvString(FileName, Utils.StringEncoding.Default);
        writer.Write(Unk1);
    }
}
