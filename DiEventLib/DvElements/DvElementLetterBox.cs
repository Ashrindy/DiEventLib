using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementLetterBox : DvNodeObject
{
    public float[] CurveData { get; set; }

    public DvElementLetterBox() { }
    public DvElementLetterBox(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        CurveData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }
}
