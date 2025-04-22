using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Cross Fade", "Cross fades between Movie Views")]
public class DvElementCrossFade : DvNodeElement
{
    public bool CurveEnabled = false;
    public int Unk0 = 0;
    public float[] CurveData = new float[32];

    public DvElementCrossFade() : base(DvElementID.CrossFade) { }
    public DvElementCrossFade(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        CurveEnabled = reader.Read<bool>();
        reader.Align(4);
        Unk0 = reader.Read<int>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(CurveEnabled);
        writer.Align(4);
        writer.Write(Unk0);
        writer.WriteArray(CurveData);
    }
}
