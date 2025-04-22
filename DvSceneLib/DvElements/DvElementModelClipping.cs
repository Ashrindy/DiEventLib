using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Model Clipping", "")]
public class DvElementModelClipping : DvNodeElement
{
    public byte[] Data = new byte[20];

    public DvElementModelClipping() : base(DvElementID.ModelClipping) { }
    public DvElementModelClipping(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Data = reader.ReadArray<byte>(20);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.WriteArray(Data);
    }
}
