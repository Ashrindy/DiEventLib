using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementLipAnimation : DvNodeObject
{
    public uint Field_00 { get; set; } = 0;
    public string FileName { get; set; } = "";
    public uint[] Field_04 { get; set; }
    public float[] Data { get; set; }
    public float[] CurveData { get; set; }
    public DvElementLipAnimation() 
    {
        Field_04 = new uint[3];
        for(int i = 0; i < 3; i++)
        {
            Field_04[i] = 0;
        }
        Data = new float[32];
        for (int i = 0; i < 32; i++)
        {
            Data[i] = 0;
        }
        CurveData = new float[32];
        for (int i = 0; i < 32; i++)
        {
            CurveData[i] = 0;
        }
    }
    public DvElementLipAnimation(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        FileName = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 64);
        Field_04 = reader.ReadArray<uint>(3);
        Data = reader.ReadArray<float>(32);
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, FileName, 64);
        writer.WriteArray(Field_04);
        writer.WriteArray(Data);
        writer.WriteArray(CurveData);
    }
}
