using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementPathAdjustment : DvNodeObject
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public uint[] Field_40 { get; set; }

    public DvElementPathAdjustment() { }
    public DvElementPathAdjustment(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        var mtx = reader.Read<Matrix4x4>();
        Quaternion tempRot;
        Matrix4x4.Decompose(mtx, out Scale, out tempRot, out Position);
        Rotation = Utils.ToEulerAngles(tempRot);
        Field_40 = reader.ReadArray<uint>(4);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.ComposeMatrix(Position, Scale, Utils.ToQuaternion(Rotation)));
        writer.WriteArray(Field_40);
    }
}
