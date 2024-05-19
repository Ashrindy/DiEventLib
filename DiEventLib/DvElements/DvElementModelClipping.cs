using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementModelClipping : DvNodeObject
{
    public byte[] Data { get; set; }
    public DvElementModelClipping() 
    {
        Data = new byte[20];
        for(int i = 0; i < 20; i++)
        {
            Data[i] = 0;
        }
    }
    public DvElementModelClipping(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<byte>(20);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}
