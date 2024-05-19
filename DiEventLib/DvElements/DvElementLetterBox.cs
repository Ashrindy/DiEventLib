using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementLetterBox : DvNodeObject
{
    public float[] CurveData { get; set; }

    public DvElementLetterBox() 
    { 
        CurveData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementLetterBox(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(CurveData);
    }
}
