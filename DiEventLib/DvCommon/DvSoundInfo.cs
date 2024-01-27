using Amicitia.IO.Binary;
using System;
using System.Collections.Generic;
namespace DiEventLib;

public class DvSoundInfo : DvObject, IBinarySerializable
{
    public List<float> Frames { get; set; } = new();    // ???

    public void Read(BinaryObjectReader reader)
    {
        Count = reader.Read<int>();
        AllocatedSize = reader.Read<int>();
        reader.Skip(8);
        Frames.AddRange(reader.ReadArray<float>(Count));
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Count);
        writer.Write(AllocatedSize);
        writer.WriteNulls(8);
        writer.WriteCollection(Frames);
    }
}