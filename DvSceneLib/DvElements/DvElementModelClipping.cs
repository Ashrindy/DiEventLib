using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Model Clipping", "")]
public class DvElementModelClipping : DvNodeElement
{
    public byte[] Data;
    public DvElementModelClipping() : base(DvElementID.ModelClipping)
    {
        Data = new byte[20];
        for(int i = 0; i < 20; i++)
        {
            Data[i] = 0;
        }
    }
    public DvElementModelClipping(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<byte>(20);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}
