using Amicitia.IO.Binary;

namespace DvSceneLib;

public class DvNodeCameraMotion : DvNode
{
    public bool Unk0 = false;
    public bool UseNearFarClip = false;
    public uint FrameStart = 0;
    public uint FrameEnd = 0;
    public uint Field0C = 0;

    public DvNodeCameraMotion() { }

    public DvNodeCameraMotion(BinaryObjectReader reader) : base(DvNodeCategory.CameraMotion)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        Unk0 = (Flags & 1) != 0;
        UseNearFarClip = (Flags & 2) != 0;
        // DiEvent using ticks for these values (1 frame = 100 ticks)
        FrameStart = reader.Read<uint>() / 100;
        FrameEnd = reader.Read<uint>() / 100;
        Field0C = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (Unk0)
            Flags |= 1;
        if (UseNearFarClip)
            Flags |= 2;
        writer.Write(Flags);
        // DiEvent using ticks for these values (1 frame = 100 ticks)
        writer.Write(FrameStart * 100);
        writer.Write(FrameEnd * 100);
        writer.Write(Field0C);
    }

}