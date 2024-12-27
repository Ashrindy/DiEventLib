using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Shadow Resolution", "Changes the resolution of shadows")]
public class DvElementShadowResolution : DvNodeElement
{
    public uint Width = 0;
    public uint Height = 0;    

    public DvElementShadowResolution() : base(DvElementID.ShadowResolution) 
    { 
        Width = 2048;
        Height = 2048;
    }
    public DvElementShadowResolution(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Width = reader.Read<uint>();
        Height = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Width);
        writer.Write(Height);
    }
}
