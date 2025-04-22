using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Shadow Resolution", "Changes the resolution of shadows")]
public class DvElementShadowResolution : DvNodeElement
{
    public uint Width = 2048;
    public uint Height = 2048;    

    public DvElementShadowResolution() : base(DvElementID.ShadowResolution) { }
    public DvElementShadowResolution(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Width = reader.Read<uint>();
        Height = reader.Read<uint>();
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Width);
        writer.Write(Height);
    }
}
