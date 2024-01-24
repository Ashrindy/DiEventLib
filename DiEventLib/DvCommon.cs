using Amicitia.IO.Binary;
using System;
using System.Collections.Generic;
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
    public CutInfo CutInfo { get; set; } = new();
    public DvPageConditionQTE PageConditionQTE { get; set; } = new();
    public DisableFrameInfo DisableFrameInfo { get; set; } = new();
    public ResourceCutInfo ResourceCutInfo { get; set; } = new();
    public SoundInfo SoundInfo { get; set; } = new();
    public DvNode Nodes { get; set; } = new();
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
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => Nodes.Read(reader));
        ChainCameraIn = reader.Read<float>();
        ChainCameraOut = reader.Read<float>();
        Type = reader.Read<int>();
        SkipPointTick = reader.Read<int>();
        reader.Skip(4);
    }

    public void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }
}

