using Amicitia.IO.Binary;
using System.Numerics;
using System.Text;

namespace DiEventLib;

public class DvElementVignette : DvNodeObject
{
    public float[] Data1 { get; set; }
    public float Data2 { get; set; }
    public float[] Data3 { get; set; }
    public float Data4 { get; set; }
    public float[] Data5 { get; set; }
    public float[] CurveData { get; set; }

    public DvElementVignette() { }
    public DvElementVignette(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data1 = reader.ReadArray<float>(9);
        Data2 = reader.Read<float>();
        Data3 = reader.ReadArray<float>(24);
        Data4 = reader.Read<float>();
        Data5 = reader.ReadArray<float>(15);
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data1);
        writer.Write(Data2);
        writer.WriteArray(Data3);
        writer.Write(Data4);
        writer.WriteArray(Data5);
        writer.WriteArray(CurveData);
    }
}
