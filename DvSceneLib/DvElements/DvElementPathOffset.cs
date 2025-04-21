using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Path Offset", "Adjusts a paths position, rotation and scale")]
public class DvElementPathOffset : DvNodeElement
{
    public Vector3 Position = new(0,0,0);
    public Vector3 Rotation = new(0,0,0);
    public Vector3 Scale = new(1,1,1);
    public bool Enabled = true;
    public uint[] Field_40 = new uint[3];

    public DvElementPathOffset() : base(DvElementID.PathOffset) { }
    public DvElementPathOffset(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var mtx = reader.Read<Matrix4x4>();
        Quaternion tempRot;
        Matrix4x4.Decompose(mtx, out Scale, out tempRot, out Position);
        Rotation = Utils.ToEulerAngles(tempRot);
        Enabled = !reader.Read<bool>();
        reader.Align(4);
        Field_40 = reader.ReadArray<uint>(3);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.ComposeMatrix(Position, Scale, Utils.ToQuaternion(Rotation)));
        writer.Write(!Enabled);
        writer.Align(4);
        writer.WriteArray(Field_40);
    }
}
