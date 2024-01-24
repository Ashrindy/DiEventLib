using Amicitia.IO.Binary;
using System;
using System.Collections.Generic;
namespace DiEventLib;

public class DvResource : DvObject, IBinarySerializable
{
    public List<ResourceEntry> Entries { get; set; } = new();

    public void Read(BinaryObjectReader reader)
    {
        Count = reader.Read<int>();
        AllocatedSize = reader.Read<int>();
        reader.Skip(8);
        Entries.AddRange(reader.ReadObjectArray<ResourceEntry>(Count));

    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Count);
        writer.Write(AllocatedSize);
        writer.Skip(8);
        writer.WriteObjectCollection(Entries);
    }
}

public class ResourceEntry : IBinarySerializable
{
    public enum DvResourceType : uint
    {
        Unknown1 = 0x0,
        Unknown2 = 0x1,
        Character = 0x2,
        Unknown3 = 0x3,
        CameraMotion = 0x4,
        PathMotion = 0x5,
        AssetMotion = 0x6,
        CharacterMotion = 0x7,
        Unknown5 = 0x8,
        Unknown6 = 0x9,
        Model = 0xA
    }
    public Guid Guid { get; set; }
    public DvResourceType Type { get; set; }
    public int Field14 { get; set; }
    public int Field18 { get; set; }
    public string Name { get; set; }

    // TODO: Find Start and End like in Yakuza games
    public void Read(BinaryObjectReader reader)
    {
        Guid = reader.Read<Guid>();
        Type = reader.Read<DvResourceType>();
        Field14 = reader.Read<int>();
        Field18 = reader.Read<int>();
        Name = reader.ReadString(StringBinaryFormat.FixedLength,64);
        reader.Skip(0x2D4);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Guid);
        writer.Write(Type);
        writer.Write(Field14);
        writer.Write(Field18);
        writer.WriteString(StringBinaryFormat.FixedLength, Name, 64);
        writer.WriteArray(new byte[0x2D4]);
    }
}