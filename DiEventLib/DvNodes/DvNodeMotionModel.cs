using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvNodeMotionModel : DvNode
{
    public uint Flags = 0;
    public uint FrameStart = 0;
    public uint FrameEnd = 0;
    public uint Field0C = 0;
    public string StateName = "Dst0000";
    public float Field14 = 1f;  // Is speed ???
    public uint Field18 = 0;
    public uint Field1C = 0;
    public uint Field20 = 0;
    public uint Field24 = 0;
    public uint Field28 = 0;

    public DvNodeMotionModel() { }
    public DvNodeMotionModel(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<uint>();
        // DiEvent using ticks for these values (1 frame = 100 ticks)
        FrameStart = reader.Read<uint>() / 100;
        FrameEnd = reader.Read<uint>() / 100;
        Field0C = reader.Read<uint>();
        // Mostly is Dst0000 
        StateName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS, 8);
        Field14 = reader.Read<float>();
        Field18 = reader.Read<uint>();
        Field1C = reader.Read<uint>();
        Field20 = reader.Read<uint>();
        Field24 = reader.Read<uint>();
        Field28 = reader.Read<uint>();
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Write(FrameStart * 100);
        writer.Write(FrameEnd * 100);
        writer.Write(Field0C);
        writer.WriteDvString(StateName, Utils.StringEncoding.ShiftJIS, 8);
        writer.Write(Field14);
        writer.Write(Field18);
        writer.Write(Field1C);
        writer.Write(Field20);
        writer.Write(Field24);
        writer.Write(Field28);
    }

}