using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCameraOffset : DvNodeObject
{
    public uint Field_00 { get; set; }
    public float[] Data { get; set; } // Data 1-3 has some values most of the times, could be some kind of a matrix or a list of vectors
    public float[] AnimData { get; set; }

    public DvElementCameraOffset() { }
    public DvElementCameraOffset(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Data = reader.ReadArray<float>(11);
        AnimData = reader.ReadArray<float>(256);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(Data);
        writer.WriteArray(AnimData);
    }
}
