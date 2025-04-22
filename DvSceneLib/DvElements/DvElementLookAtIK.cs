using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Look At IK", "Makes the parent node look at a specific node")]
public class DvElementLookAtIK : DvNodeElement
{
    public struct Object
    {
        public int Unk0;
        public Guid GUID;
        public Vector3 Offset;
    }

    public bool CurveEnabled = false;
    public Object Obj = new();
    public Object FinishObj = new();
    public float[] CurveData = new float[64];

    public DvElementLookAtIK() : base(DvElementID.LookAtIK) { }
    public DvElementLookAtIK(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        CurveEnabled = reader.Read<bool>();
        reader.Align(4);
        Obj = reader.Read<Object>();
        FinishObj = reader.Read<Object>();
        CurveData = reader.ReadArray<float>(64);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(CurveEnabled);
        writer.Align(4);
        writer.Write(Obj);
        writer.Write(FinishObj);
        writer.WriteArray(CurveData);
    }
}
