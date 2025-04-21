using Amicitia.IO.Binary;

namespace DvSceneLib;

public class DvNodeCharacterMotion : DvNode
{
    public bool Unk0 = false;
    public bool UpdateTransform = false;
    public bool UseRootBone = false;
    public uint FrameStart = 0;
    public uint FrameEnd = 0;
    public uint Field0C = 0;
    public string StateName = "Dst0000";
    public float Speed = 1f;
    public uint Field18 = 0;
    public uint Field1C = 0;
    public uint Field20 = 0;
    public uint Field24 = 0;
    public uint Field28 = 0;

    public DvNodeCharacterMotion() { }
    public DvNodeCharacterMotion(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        Unk0 = (Flags & 1) != 0;
        UpdateTransform = (Flags & 2) != 0;
        UseRootBone = (Flags & 4) != 0;
        // DiEvent using ticks for these values (1 frame = 100 ticks)
        FrameStart = reader.Read<uint>() / 100;
        FrameEnd = reader.Read<uint>() / 100;
        Field0C = reader.Read<uint>();
        // Mostly is Dst0000 
        StateName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS, 8);
        Speed = reader.Read<float>();
        Field18 = reader.Read<uint>();
        Field1C = reader.Read<uint>();
        Field20 = reader.Read<uint>();
        Field24 = reader.Read<uint>();
        Field28 = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (Unk0) Flags |= 1;
        if (UpdateTransform) Flags |= 2;
        if (UseRootBone) Flags |= 4;
        writer.Write(Flags);
        // DiEvent using ticks for these values (1 frame = 100 ticks)
        writer.Write(FrameStart * 100);
        writer.Write(FrameEnd * 100);
        writer.Write(Field0C);
        // Mostly is Dst0000
        writer.WriteDvString(StateName, Utils.StringEncoding.ShiftJIS, 8);
        writer.Write(Speed);
        writer.Write(Field18);
        writer.Write(Field1C);
        writer.Write(Field20);
        writer.Write(Field24);
        writer.Write(Field28);
    }

}