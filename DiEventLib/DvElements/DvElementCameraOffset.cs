using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCameraOffset : DvNodeElement
{
    public uint Field_00 { get; set; } = 0;
    public float[] Data { get; set; } // Data 1-3 has some values most of the times, could be some kind of a matrix or a list of vectors
    public float[] AnimData { get; set; }

    public DvElementCameraOffset() 
    { 
        Data = new float[11];
        for(int i = 0; i < 11; i++)
        {
            Data[i] = 0;
        }
        AnimData = new float[256];
        for(int i = 0;i < 256; i++)
        {
            AnimData[i] = 1;
        }
    }
    public DvElementCameraOffset(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        Data = reader.ReadArray<float>(11);
        AnimData = reader.ReadArray<float>(256);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(Data);
        writer.WriteArray(AnimData);
    }
}
