using Amicitia.IO.Binary;
using System;
using System.Collections.Generic;
namespace DiEventLib;

public class CutInfo : DvObject, IBinarySerializable
{
    public List<float> FrameCut { get; set; } = new();

    public void Read(BinaryObjectReader reader)
    {
        Count = reader.Read<int>();
        AllocatedSize = reader.Read<int>();
        reader.Skip(8);
        FrameCut.AddRange(reader.ReadArray<float>(Count));
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Count);
        writer.Write(AllocatedSize);
        writer.Skip(8);
        writer.WriteCollection(FrameCut);
    }
}