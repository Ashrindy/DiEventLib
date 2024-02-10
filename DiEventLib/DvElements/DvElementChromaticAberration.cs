using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementChromaticAberration : DvNodeObject
{
    public float Red { get; set; }
    public float Green { get; set; }
    public float Blue { get; set; }
    public float SphereIntensity { get; set; }
    public float VerticalPlanetIntensity { get; set; }
    public float HorizontalPlanetIntensity { get; set; }
    public Vector2 Position { get; set; }
    public float Field_08 { get; set; }
    public float Unk1 { get; set; }
    public float Unk2 { get; set; }
    public float Field_10 { get; set; }
    public float Field_14 { get; set; }
    public float Field_18 { get; set; }
    public float Field_1c { get; set; }
    public Vector2 UnkVector { get; set; }
    public float[] CurveData { get; set; }
    public DvElementChromaticAberration() { }
    public DvElementChromaticAberration(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Red = reader.Read<float>();
        Green = reader.Read<float>();
        Blue = reader.Read<float>();
        SphereIntensity = reader.Read<float>();
        VerticalPlanetIntensity = reader.Read<float>();
        HorizontalPlanetIntensity = reader.Read<float>();
        Position = reader.Read<Vector2>();
        Field_08 = reader.Read<float>();
        Unk1 = reader.Read<float>();
        Unk2 = reader.Read<float>();
        Field_10 = reader.Read<float>();
        Field_14 = reader.Read<float>();
        Field_18 = reader.Read<float>();
        Field_1c = reader.Read<float>();
        UnkVector = reader.Read<Vector2>();
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Red);
        writer.Write(Green);
        writer.Write(Blue);
        writer.Write(SphereIntensity);
        writer.Write(VerticalPlanetIntensity);
        writer.Write(HorizontalPlanetIntensity);
        writer.Write(Position);
        writer.Write(Field_08);
        writer.Write(Unk1);
        writer.Write(Unk2);
        writer.Write(Field_10);
        writer.Write(Field_14);
        writer.Write(Field_18);
        writer.Write(Field_1c);
        writer.Write(UnkVector);
        writer.WriteArray(CurveData);
    }
}
