using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Point Light", "Adds a point light for lighting up the cutscene")]
public class DvElementVariablePointLight : DvNodeElement
{
    public struct Parameters
    {
        public float Range;
        public float Intensity;
        public float Falloff;
        public float Angle;
    }

    public int Unk0 = 0;
    public Vector3 Position = new(0, 0, 0);
    public Vector3 FinishPosition = new(0, 0, 0);
    public RGB32 Color = new();
    public RGB32 FinishColor = new();
    public Parameters Params = new();
    public Parameters FinishParams = new();
    public int Unk1 = 0;
    public int[] Unk2 = new int[10];
    public float[] CurveData = new float[128];

    public DvElementVariablePointLight() : base(DvElementID.VariablePointLight) { }
    public DvElementVariablePointLight(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Unk0 = reader.Read<int>();
        Position = reader.Read<Vector3>();
        FinishPosition = reader.Read<Vector3>();
        Color = reader.Read<RGB32>();
        FinishColor = reader.Read<RGB32>();
        Params = reader.Read<Parameters>();
        FinishParams = reader.Read<Parameters>();
        Unk1 = reader.Read<int>();
        Unk2 = reader.ReadArray<int>(10);
        CurveData = reader.ReadArray<float>(128);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Unk0);
        writer.Write(Position);
        writer.Write(FinishPosition);
        writer.Write(Color);
        writer.Write(FinishColor);
        writer.Write(Params);
        writer.Write(FinishParams);
        writer.Write(Unk1);
        writer.WriteArray(Unk2);
        writer.WriteArray(CurveData);
    }
}
