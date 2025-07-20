using Amicitia.IO.Binary;

namespace DvSceneLib;

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
        writer.Write(Entries.Count);
        writer.Write(AllocatedSize);
        writer.WriteNulls(8);
        writer.WriteObjectCollection(Entries);
    }
}

public class ResourceEntry : IBinarySerializable
{
    public enum DvResourceType : uint
    {
        Character = 0x2,
        CameraMotion = 0x4,
        ModelMotion = 0x6,
        CharacterMotion = 0x7,
        Model = 0xA
    }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public DvResourceType Type { get; set; } = DvResourceType.Character;
    public int Field14 { get; set; } = 0;
    public int Field18 { get; set; } = 1;
    public string Name { get; set; } = "";
    public int Unk0 { get; set; } = 0;
    public int Unk1 { get; set; } = 0;
    public byte[] Data { get; set; } = new byte[0x0c];

    // TODO: Find Start and End like in Yakuza games
    public void Read(BinaryObjectReader reader)
    {
        Guid = reader.Read<Guid>();
        Type = reader.Read<DvResourceType>();
        Field14 = reader.Read<int>();
        Field18 = reader.Read<int>();
        Name = reader.ReadString(StringBinaryFormat.FixedLength, 0x300);
        Unk0 = reader.Read<int>();
        Unk1 = reader.Read<int>();
        Data = reader.ReadArray<byte>(0x0c);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Guid);
        writer.Write(Type);
        writer.Write(Field14);
        writer.Write(Field18);
        writer.WriteString(StringBinaryFormat.FixedLength, Name, 0x300);
        writer.Write(Unk0);
        writer.Write(Unk1);
        writer.WriteNulls(0x0C);
    }
}