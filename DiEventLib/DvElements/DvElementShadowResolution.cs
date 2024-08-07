using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Shadow Resolution", "Changes the resolution of shadows")]
public class DvElementShadowResolution : DvNodeElement
{
    public uint ShadowRes1 = 0;
    public uint ShadowRes2 = 0;    

    public DvElementShadowResolution() : base(DvElementID.ShadowResolution) 
    { 
        ShadowRes1 = 2048;
        ShadowRes2 = 2048;
    }
    public DvElementShadowResolution(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        ShadowRes1 = reader.Read<uint>();
        ShadowRes2 = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(ShadowRes1);
        writer.Write(ShadowRes2);
    }
}
