using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Bloom Param", "Edits the parameters for bloom")]
public class DvElementBloomParam : DvNodeElement
{
    public struct Parameters
    {
        public float Strength;
        public float SampleRadius;
        public float BlurQuality;
    }

    public bool CurveEnabled = false;
    public Parameters Params = new();
    public Parameters FinishParams = new();
    public float[] Unk0 = new float[4];
    public float[] CurveData = new float[32];

    public DvElementBloomParam() : base(DvElementID.BloomParam) { }
    public DvElementBloomParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        CurveEnabled = reader.Read<bool>();
        reader.Align(4);
        Params = reader.Read<Parameters>();
        FinishParams = reader.Read<Parameters>();
        Unk0 = reader.ReadArray<float>(4);
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(CurveEnabled);
        writer.Align(4);
        writer.Write(Params);
        writer.Write(FinishParams);
        writer.WriteArray(Unk0);
        writer.WriteArray(CurveData);
    }
}
