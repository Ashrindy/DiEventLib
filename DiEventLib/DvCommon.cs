using Amicitia.IO.Binary;
using Amicitia.IO.Binary.Extensions;
using System;
using System.Collections.Generic;
namespace DiEventLib;

public class DvObject
{
    public uint Pointer { get; set; }
    public int Count;
    public int AllocatedSize;
    public long Padding;
}

public class DvCommon : IBinarySerializable
{
    public uint Pointer { get; set; }
    public uint Version { get; set; }
    public uint Flags { get; set; }
    public float Start { get; set; }
    public float End { get; set; }
    public uint NodeDrawNum { get; set; }
    private uint NodesPointer { get; set; }
    public CutInfo CutInfo { get; set; } = new();
    public DvPageConditionQTE PageConditionQTE { get; set; } = new();
    public DisableFrameInfo DisableFrameInfo { get; set; } = new();
    public ResourceCutInfo ResourceCutInfo { get; set; } = new();
    public SoundInfo SoundInfo { get; set; } = new();
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
        CutInfo.Pointer = reader.Read<uint>();
        PageConditionQTE.Pointer = reader.Read<uint>();
        DisableFrameInfo.Pointer = reader.Read<uint>();
        ResourceCutInfo.Pointer = reader.Read<uint>();
        SoundInfo.Pointer = reader.Read<uint>();
        NodesPointer = reader.Read<uint>();
        reader.ReadAtOffset(CutInfo.Pointer + 0x20, () => CutInfo.Read(reader));
        reader.ReadAtOffset(PageConditionQTE.Pointer + 0x20, () => PageConditionQTE.Read(reader));
        reader.ReadAtOffset(DisableFrameInfo.Pointer + 0x20, () => DisableFrameInfo.Read(reader));
        reader.ReadAtOffset(ResourceCutInfo.Pointer + 0x20, () => ResourceCutInfo.Read(reader));
        reader.ReadAtOffset(SoundInfo.Pointer + 0x20, () => SoundInfo.Read(reader));
        reader.ReadAtOffset(NodesPointer + 0x20, () => Node.Read(reader));
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
        writer.Write(CutInfo.Pointer);
        writer.Write(PageConditionQTE.Pointer);
        writer.Write(DisableFrameInfo.Pointer);
        writer.Write(ResourceCutInfo.Pointer);
        writer.Write(SoundInfo.Pointer);
        writer.Write(NodesPointer);
        writer.Write(ChainCameraIn);
        writer.Write(ChainCameraOut);
        writer.Write(Type);
        writer.Write(SkipPointTick);
        writer.Seek(CutInfo.Pointer + 0x20, SeekOrigin.Begin);
        CutInfo.Write(writer);
        writer.Seek(PageConditionQTE.Pointer + 0x20, SeekOrigin.Begin);
        PageConditionQTE.Write(writer);
        writer.Seek(DisableFrameInfo.Pointer + 0x20, SeekOrigin.Begin);
        DisableFrameInfo.Write(writer);
        writer.Seek(ResourceCutInfo.Pointer + 0x20, SeekOrigin.Begin);
        ResourceCutInfo.Write(writer);
        writer.Seek(SoundInfo.Pointer + 0x20, SeekOrigin.Begin);
        SoundInfo.Write(writer);
        writer.Seek(NodesPointer + 0x20, SeekOrigin.Begin);
        Node.Write(writer);
        writer.Skip(4);
    }
}

