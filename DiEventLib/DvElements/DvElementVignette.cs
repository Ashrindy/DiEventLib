using Amicitia.IO.Binary;
using System.Numerics;
using System.Text;

namespace DiEventLib;

public class DvElementVignette : DvNodeObject
{
    public float Field_00 { get; set; }
    public float Field_04 { get; set; }
    public Vector2 Position { get; set; }
    public float VerticalAspect { get; set; }
    public float HorizontalAspect { get; set; }
    public float Radius { get; set; }
    public float Field_18 { get; set; }
    public float Field_1c { get; set; }
    public float Alpha { get; set; }
    public float[] Unk1 { get; set; }
    public float Feather { get; set; }
    public float Radius2 { get; set; }
    public float[] Unk2 { get; set; }
    public float[] CurveData { get; set; }

    public DvElementVignette() { }
    public DvElementVignette(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<float>();
        Field_04 = reader.Read<float>();
        Position = reader.Read<Vector2>();
        VerticalAspect = reader.Read<float>();
        HorizontalAspect = reader.Read<float>();
        Radius = reader.Read<float>();
        Field_18 = reader.Read<float>();
        Field_1c = reader.Read<float>();
        Alpha = reader.Read<float>();
        Unk1 = reader.ReadArray<float>(4);
        Feather = reader.Read<float>();
        Radius2 = reader.Read<float>();
        Unk2 = reader.ReadArray<float>(36);
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.Write(Field_04);
        writer.Write(Position);
        writer.Write(VerticalAspect);
        writer.Write(HorizontalAspect);
        writer.Write(Radius);
        writer.Write(Field_18);
        writer.Write(Field_1c);
        writer.Write(Alpha);
        writer.WriteArray(Unk1);
        writer.Write(Feather);
        writer.Write(Radius2);
        writer.WriteArray(Unk2);
        writer.WriteArray(CurveData);
    }
}
