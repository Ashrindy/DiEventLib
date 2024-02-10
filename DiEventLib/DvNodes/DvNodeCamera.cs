using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvNodeCamera : DvNodeObject
{
    public uint Flags { get; set; }
    public int FrameProgressionCount { get; set; }
    public int CaptionCount { get; set; }
    public uint Field0C { get; set; }   // Is caption list ???
    public List<float> FrameProgression { get; set; } = new();
    public List<float> FrameProgressionSpeed { get; set; } = new();

    public DvNodeCamera() { }
    public DvNodeCamera(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        FrameProgressionCount = reader.Read<int>();
        CaptionCount = reader.Read<int>();
        Field0C = reader.Read<uint>();
        FrameProgression.AddRange(reader.ReadArray<float>(FrameProgressionCount));
        FrameProgressionSpeed.AddRange(reader.ReadArray<float>(FrameProgressionCount));
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Write(FrameProgressionCount);
        writer.Write(CaptionCount); 
        writer.Write(Field0C);
        writer.WriteCollection(FrameProgression);
        writer.WriteCollection(FrameProgressionSpeed);
    }
}