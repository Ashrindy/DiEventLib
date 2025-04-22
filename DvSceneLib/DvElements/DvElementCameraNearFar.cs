using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Near Far Setting", "Modifies the NearZ and FarZ")]
public class DvElementCameraNearFar : DvNodeElement
{
    public bool EnabledNearClip = true;
    public bool EnabledFarClip = true;
    public float NearClip = 0;
    public float FarClip = 1000;

    public DvElementCameraNearFar() : base(DvElementID.CameraNearFar) { }
    public DvElementCameraNearFar(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        EnabledNearClip = (Flags & 1) == 0;
        EnabledFarClip = (Flags & 2) == 0;
        NearClip = reader.Read<float>();
        FarClip = reader.Read<float>();
        reader.Skip(20);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (!EnabledNearClip) Flags |= 1;
        if (!EnabledFarClip) Flags |= 2;
        writer.Write(Flags);
        writer.Write(NearClip);
        writer.Write(FarClip);
        writer.WriteNulls(20);
    }
}
