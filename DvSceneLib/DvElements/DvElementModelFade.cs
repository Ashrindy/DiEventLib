using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Model Fade", "")]
public class DvElementModelFade : DvNodeElement
{
    public int[] Unk0 = new int[8];
    public float[] CurveData = new float[128];

    public DvElementModelFade() : base(DvElementID.ModelFade) { }
    public DvElementModelFade(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Unk0 = reader.ReadArray<int>(8);
        CurveData = reader.ReadArray<float>(128);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Unk0);
        writer.WriteArray(CurveData);
    }
}
