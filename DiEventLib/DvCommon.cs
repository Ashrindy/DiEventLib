using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvObject
{
    public int Count;
    public int AllocatedSize;
}

public class DvCommon : IBinarySerializable
{
    public uint Version { get; set; }
    public uint Flags { get; set; }
    public float Start { get; set; }
    public float End { get; set; }
    public uint NodeDrawNum { get; set; }
    public DvCutInfo CutInfo { get; set; } = new();
    public DvPageConditionQTE PageConditionQTE { get; set; } = new();
    public DvDisableFrameInfo DisableFrameInfo { get; set; } = new();
    public DvResourceCutInfo ResourceCutInfo { get; set; } = new();
    public DvSoundInfo SoundInfo { get; set; } = new();
    public DvNode Node { get; set; } = new();
    public float ChainCameraIn { get; set; }
    public float ChainCameraOut { get; set; }
    public int Type { get; set; }
    public int SkipPointTick { get; set; }
    public void Read(BinaryObjectReader reader)
    {
        Version = reader.Read<uint>();
        Flags = reader.Read<uint>();
        Start = reader.Read<float>();
        End = reader.Read<float>();
        NodeDrawNum = reader.Read<uint>();
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => CutInfo.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => PageConditionQTE.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => DisableFrameInfo.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => ResourceCutInfo.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => SoundInfo.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => Node.Read(reader));
        ChainCameraIn = reader.Read<float>();
        ChainCameraOut = reader.Read<float>();
        Type = reader.Read<int>();
        SkipPointTick = reader.Read<int>();
        reader.Skip(4);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Version);
        writer.Write(Flags);
        writer.Write(Start);
        writer.Write(End);
        writer.Write(NodeDrawNum);
        long cutInfoPointerPos = writer.Position;
        long pageConditionQTEPointerPos = writer.Position + 4;
        long disableFrameInfoPointerPos = writer.Position + 8;
        long resourceCutInfoPointerPos = writer.Position + 12;
        long soundInfoPointerPos = writer.Position + 16;
        long nodePointerPos = writer.Position + 20;
        writer.WriteNulls(24);
        writer.Write(ChainCameraIn);
        writer.Write(ChainCameraOut);
        writer.Write(Type);
        writer.Write(SkipPointTick);
        writer.WriteNulls(4);

        {
            long cutInfoPointer = writer.Position;
            writer.Seek(cutInfoPointerPos, SeekOrigin.Begin);
            writer.Write(cutInfoPointer - 0x20);
            writer.Seek(cutInfoPointer, SeekOrigin.Begin);
            CutInfo.Write(writer);
        }

        {
            long pageConditionQTEPointer = writer.Position;
            writer.Seek(pageConditionQTEPointerPos, SeekOrigin.Begin);
            writer.Write((uint)pageConditionQTEPointer - 0x20);
            writer.Seek(pageConditionQTEPointer, SeekOrigin.Begin);
            PageConditionQTE.Write(writer);
        }

        {
            long disableFrameInfoPointer = writer.Position;
            writer.Seek(disableFrameInfoPointerPos, SeekOrigin.Begin);
            writer.Write((uint)disableFrameInfoPointer - 0x20);
            writer.Seek(disableFrameInfoPointer, SeekOrigin.Begin);
            DisableFrameInfo.Write(writer);
        }

        {
            long resourceCutInfoPointer = writer.Position;
            writer.Seek(resourceCutInfoPointerPos, SeekOrigin.Begin);
            writer.Write((uint)resourceCutInfoPointer - 0x20);
            writer.Seek(resourceCutInfoPointer, SeekOrigin.Begin);
            ResourceCutInfo.Write(writer);
        }

        {
            long soundInfoPointer = writer.Position;
            writer.Seek(soundInfoPointerPos, SeekOrigin.Begin);
            writer.Write((uint)soundInfoPointer - 0x20);
            writer.Seek(soundInfoPointer, SeekOrigin.Begin);
            SoundInfo.Write(writer);
        }

        {
            long soundInfoPointer = writer.Position;
            writer.Seek(nodePointerPos, SeekOrigin.Begin);
            writer.Write((uint)soundInfoPointer - 0x20);
            writer.Seek(soundInfoPointer, SeekOrigin.Begin);
            Node.Write(writer);
        }
    }
}

