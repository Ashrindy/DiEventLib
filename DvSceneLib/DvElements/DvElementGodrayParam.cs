using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Godray Param", "Edits the parameters of the godray")]
public class DvElementGodrayParam : DvNodeElement
{
    public RGB32F Color = new();
    public float Intensity = 1;
    public float[] CurveData = new float[32];

    public DvElementGodrayParam() : base(DvElementID.GodrayParam) { }
    public DvElementGodrayParam(BinaryObjectReader reader) 
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Color = reader.Read<RGB32F>();
        Intensity = reader.Read<float>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Color);
        writer.Write(Intensity);
        writer.WriteArray(CurveData);
    }
}
