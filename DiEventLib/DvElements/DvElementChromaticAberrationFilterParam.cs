using Amicitia.IO.Binary;
using DiEventLib.Misc;
using System.Numerics;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Chromatic Aberration Filter", "Adds chromatic aberration to the cutscene")]
public class DvElementChromaticAberrationFilterParam : DvNodeElement
{
    public ChromaticAberration ChromaticAberrationBefore;
    public float Field_08 = 0;
    public ChromaticAberration ChromaticAberrationAfter;
    public float[] CurveData;
    public DvElementChromaticAberrationFilterParam() : base(DvElementID.ChromaticAberrationFilterParam)
    {
        ChromaticAberrationBefore = new ChromaticAberration
        {
            ColorOffset = new(0, 0, 0),
            SphereCurve = 0,
            Scale = new(0, 0),
            Position = new(0, 0)
        };
        ChromaticAberrationAfter = new ChromaticAberration
        {
            ColorOffset = new(0, 0, 0),
            SphereCurve = 0,
            Scale = new(0, 0),
            Position = new(0, 0)
        };
        CurveData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementChromaticAberrationFilterParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        ChromaticAberrationBefore = reader.Read<ChromaticAberration>();
        Field_08 = reader.Read<float>();
        ChromaticAberrationAfter = reader.Read<ChromaticAberration>();
        CurveData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(ChromaticAberrationBefore);
        writer.Write(Field_08);
        writer.Write(ChromaticAberrationAfter);
        writer.WriteArray(CurveData);
    }
}

public struct ChromaticAberration
{
    public Vector3 ColorOffset { get; set; }
    public float SphereCurve { get; set; }
    public Vector2 Scale { get; set; }
    public Vector2 Position { get; set; }
}
