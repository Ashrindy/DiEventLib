using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementPathAdjustment : DvNodeElement
{
    public Vector3 Position = new(0,0,0);
    public Vector3 Rotation = new(0,0,0);
    public Vector3 Scale = new(0,0,0);
    public uint[] Field_40 { get; set; }

    public DvElementPathAdjustment() : base(DvElementID.PathAdjustment)
    {
        Field_40 = new uint[4];
        for (int i = 0; i < 4; i++)
        {
            Field_40[i] = 0;
        }
    }
    public DvElementPathAdjustment(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var mtx = reader.Read<Matrix4x4>();
        Quaternion tempRot;
        Matrix4x4.Decompose(mtx, out Scale, out tempRot, out Position);
        Rotation = Utils.ToEulerAngles(tempRot);
        Field_40 = reader.ReadArray<uint>(4);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.ComposeMatrix(Position, Scale, Utils.ToQuaternion(Rotation)));
        writer.WriteArray(Field_40);
    }
}
