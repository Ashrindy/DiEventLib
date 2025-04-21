using Amicitia.IO.Binary;
using DvSceneLib.IO.Template;

namespace DvSceneLib;

public class DvObject
{
    protected int Count;
    protected int AllocatedSize;
}

public class DvCommon
{
    public uint Version { get; set; } = 0;
    public uint Flags { get; set; } = 0;
    public float Start { get; set; } = 0;
    public float End { get; set; } = 0;
    public uint NodeDrawNum { get; set; } = 0;
    public DvCutInfo CutInfo { get; set; } = new();
    public DvPageInfo PageInfo { get; set; } = new();
    public DvDisableFrameInfo DisableFrameInfo { get; set; } = new();
    public DvResourceCutInfo ResourceCutInfo { get; set; } = new();
    public DvSoundInfo SoundInfo { get; set; } = new();
    public DvNode Node { get; set; } = new();
    public float ChainCameraIn { get; set; } = -1;
    public float ChainCameraOut { get; set; } = -1;
    public int Type { get; set; } = 0;
    public int SkipPointTick { get; set; } = 0;

    public DvCommon() 
    {
    }

    public void Read(BinaryObjectReader reader, DiEventDataBase db = null)
    {
        Version = reader.Read<uint>();
        Flags = reader.Read<uint>();
        Start = reader.Read<float>();
        End = reader.Read<float>();
        NodeDrawNum = reader.Read<uint>();
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => CutInfo.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => PageInfo.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => DisableFrameInfo.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => ResourceCutInfo.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => SoundInfo.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => { if (db != null) Node = DvNodeReader.ReadNode(reader, db); else Node = DvNodeReader.ReadNode(reader); });
        ChainCameraIn = reader.Read<float>();
        ChainCameraOut = reader.Read<float>();
        Type = reader.Read<int>();
        SkipPointTick = reader.Read<int>();
        reader.Skip(4);
    }

    public void Write(BinaryObjectWriter writer, DiEventDataBase db = null)
    {
        writer.Write(Version);
        writer.Write(Flags);
        writer.Write(Start);
        writer.Write(End);
        writer.Write(NodeDrawNum);
        long cutInfoPointerPos = writer.Position;
        long pageInfoPointerPos = writer.Position + 4;
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
            long pageInfoPointer = writer.Position;
            writer.Seek(pageInfoPointerPos, SeekOrigin.Begin);
            writer.Write((uint)pageInfoPointer - 0x20);
            writer.Seek(pageInfoPointer, SeekOrigin.Begin);
            PageInfo.Write(writer);
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
            long nodePointer = writer.Position;
            writer.Seek(nodePointerPos, SeekOrigin.Begin);
            writer.Write((uint)nodePointer - 0x20);
            writer.Seek(nodePointer, SeekOrigin.Begin);
            if (db != null)
                ((DvNodeTemplate)Node).WriteNode(writer, db);
            else
                Node.WriteNode(writer);
        }
    }
}

