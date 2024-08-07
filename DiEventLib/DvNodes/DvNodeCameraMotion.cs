using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvNodeCameraMotion : DvNode
{
    public uint Flags = 0;
    public uint FrameStart = 0;
    public uint FrameEnd = 0;
    public uint Field0C = 0;

    public DvNodeCameraMotion() { }

    public DvNodeCameraMotion(BinaryObjectReader reader) : base(DvNodeCategory.CameraMotion)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        // DiEvent using ticks for these values (1 frame = 100 ticks)
        FrameStart = reader.Read<uint>() / 100;
        FrameEnd = reader.Read<uint>() / 100;
        Field0C = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        // DiEvent using ticks for these values (1 frame = 100 ticks)
        writer.Write(FrameStart * 100);
        writer.Write(FrameEnd * 100);
        writer.Write(Field0C);
    }

}