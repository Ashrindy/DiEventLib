using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Time", "Changes the time of day/night")]
public class DvElementTime : DvNodeElement
{
    public struct Node
    {
        public int Hour;
        public int Minute;
    }

    public bool CurveEnabled = false;
    public Node TimeNode = new();
    public Node FinishTimeNode = new();
    public uint Field_14 = 0;
    public uint Field_18 = 0;
    public uint Field_1c = 0;
    public uint Field_20 = 0;
    public float[] CurveData = new float[32];

    public DvElementTime() : base(DvElementID.Time) { }
    public DvElementTime(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        CurveEnabled = reader.Read<bool>();
        reader.Align(4);
        TimeNode = reader.Read<Node>();
        FinishTimeNode = reader.Read<Node>();
        Field_14 = reader.Read<uint>();
        Field_18 = reader.Read<uint>();
        Field_1c = reader.Read<uint>();
        Field_20 = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(CurveEnabled);
        writer.Align(4);
        writer.Write(TimeNode);
        writer.Write(FinishTimeNode);
        writer.Write(Field_14);
        writer.Write(Field_18);
        writer.Write(Field_1c);
        writer.Write(Field_20);
        writer.WriteArray(CurveData);
    }
}
