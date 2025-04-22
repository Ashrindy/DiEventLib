using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Chromatic Aberration Filter", "Adds chromatic aberration to the cutscene")]
public class DvElementChromaticAberrationFilterParam : DvNodeElement
{
    public struct ChromaticAberration
    {
        public Vector3 ColorOffset;
        public float SphereCurve;
        public Vector2 Scale;
        public Vector2 Position;
    }

    public ChromaticAberration Node;
    public bool CurveEnabled = false;
    public ChromaticAberration FinishNode;
    public float[] CurveData = new float[32];

    public DvElementChromaticAberrationFilterParam() : base(DvElementID.ChromaticAberrationFilterParam) { }
    public DvElementChromaticAberrationFilterParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Node = reader.Read<ChromaticAberration>();
        CurveEnabled = reader.Read<bool>();
        reader.Align(4);
        FinishNode = reader.Read<ChromaticAberration>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Node);
        writer.Write(CurveEnabled);
        writer.Align(4);
        writer.Write(FinishNode);
        writer.WriteArray(CurveData);
    }
}
