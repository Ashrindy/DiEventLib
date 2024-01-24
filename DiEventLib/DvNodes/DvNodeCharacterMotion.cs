﻿using Amicitia.IO.Binary;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace DiEventLib;

public class DvNodeCharacterMotion : DvNodeObject
{
    public uint Flags { get; set; }
    public uint FrameStart { get; set; }
    public uint FrameEnd { get; set; }
    public uint Field0C { get; set; }
    public string StateName { get; set; }
    public float Field14 { get; set; }  // Is speed ???
    public uint Field18 { get; set; }
    public uint Field1C { get; set; }
    public uint Field20 { get; set; }
    public uint Field24 { get; set; }
    public uint Field28 { get; set; }

    public DvNodeCharacterMotion() { }
    public DvNodeCharacterMotion(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        // DiEvent using ticks for these values (1 frame = 100 ticks)
        FrameStart = reader.Read<uint>() / 100;
        FrameEnd = reader.Read<uint>() / 100;
        Field0C = reader.Read<uint>();
        // Mostly is Dst0000 
        StateName = reader.ReadString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, 8);
        Field14 = reader.Read<float>();
        Field18 = reader.Read<uint>();
        Field1C = reader.Read<uint>();
        Field20 = reader.Read<uint>();
        Field24 = reader.Read<uint>();
        Field28 = reader.Read<uint>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }

}