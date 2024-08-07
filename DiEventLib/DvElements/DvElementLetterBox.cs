using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Letter Box", "Makes the cutscene use a fake 21:9 aspect ratio")]
public class DvElementLetterBox : DvNodeElement
{
    public float[] CurveData;

    public DvElementLetterBox() : base(DvElementID.LetterBox)
    { 
        CurveData = new float[32];
        for(int i = 0; i < 32; i++)
        {
            CurveData[i] = 1;
        }
    }
    public DvElementLetterBox(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        CurveData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(CurveData);
    }
}
