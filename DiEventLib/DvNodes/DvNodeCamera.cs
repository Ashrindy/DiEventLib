using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvNodeCamera : DvNodeObject
{
    public uint Flags { get; set; }
    public uint Field0C { get; set; }   // Is caption list ???
    public List<float> FrameProgression { get; set; } = new();
    public List<float> FrameProgressionSpeed { get; set; } = new();

    public DvNodeCamera() { }
    public DvNodeCamera(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        int frameProgressionCount = reader.Read<int>();
        int captionCount = reader.Read<int>();
        Field0C = reader.Read<uint>();
        FrameProgression.AddRange(reader.ReadArray<float>(frameProgressionCount));
        FrameProgressionSpeed.AddRange(reader.ReadArray<float>(frameProgressionCount));
    }

    public override void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }

}