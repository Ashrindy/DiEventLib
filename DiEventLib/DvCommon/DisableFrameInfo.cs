using Amicitia.IO.Binary;
using System;
using System.Collections.Generic;
namespace DiEventLib;

public class DisableFrameInfo : DvObject, IBinarySerializable
{
    public struct DisableFrameData
    {
        public float FrameStart;
        public float FrameEnd;
    }
    public List<DisableFrameData> Frames { get; set; } = new();

    public void Read(BinaryObjectReader reader)
    {
        Count = reader.Read<int>();
        AllocatedSize = reader.Read<int>();
        reader.Skip(8);
        Frames.AddRange(reader.ReadArray<DisableFrameData>(Count));
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Count);
        writer.Write(AllocatedSize);
        writer.Skip(8);
        writer.WriteCollection(Frames);
    }
}