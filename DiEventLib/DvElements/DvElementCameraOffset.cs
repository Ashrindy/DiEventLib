using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementCameraOffset : DvNodeObject
{
    public float[] Data { get; set; } // Data 1-3 has some values most of the times, could be some kind of a matrix or a list of vectors
    public float[] AnimData { get; set; }

    public DvElementCameraOffset() { }
    public DvElementCameraOffset(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<float>(12);
        AnimData = reader.ReadArray<float>(256);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
        writer.WriteArray(AnimData);
    }
}
