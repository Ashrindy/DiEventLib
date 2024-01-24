﻿using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvNodeCameraMotion : DvNodeObject
{
    public uint Flags { get; set; }
    public uint FrameStart { get; set; }
    public uint FrameEnd { get; set; }
    public uint Field0C { get; set; }

    public DvNodeCameraMotion() { }
    public DvNodeCameraMotion(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        // DiEvent using ticks for these values (1 frame = 100 ticks)
        FrameStart = reader.Read<uint>() / 100;
        FrameEnd = reader.Read<uint>() / 100;
        Field0C = reader.Read<uint>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }

}