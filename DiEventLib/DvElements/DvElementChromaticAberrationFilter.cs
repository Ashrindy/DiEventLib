using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementChromaticAberrationFilter : DvNodeObject
{
    public ChromaticAberration Data1 { get; set; }
    public float Field_08 { get; set; }
    public ChromaticAberration Data2 { get; set; }
    public float[] CurveData { get; set; }
    public DvElementChromaticAberrationFilter() { }
    public DvElementChromaticAberrationFilter(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data1 = reader.Read<ChromaticAberration>();
        Field_08 = reader.Read<float>();
        Data2 = reader.Read<ChromaticAberration>();
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Data1);
        writer.Write(Field_08);
        writer.Write(Data2);
        writer.WriteArray(CurveData);
    }
}

public struct ChromaticAberration
{
    public RGB32 ColorOffset { get; set; }
    public float SphereCurve { get; set; }
    public Vector2 Scale { get; set; }
    public Vector2 Position { get; set; }
}
