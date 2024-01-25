using Amicitia.IO.Binary;
using System.Numerics;
using System.Text;

namespace DiEventLib;

public class DvElementPathAdjustment : DvNodeObject
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;
    public uint[] Field_40 { get; set; }

    public DvElementPathAdjustment() { }
    public DvElementPathAdjustment(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        var mtx = reader.Read<Matrix4x4>();
        Matrix4x4.Decompose(mtx, out Scale, out Rotation, out Position);
        Field_40 = reader.ReadArray<uint>(4);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        var mtxPos = Matrix4x4.CreateTranslation(Position);
        var mtxRot = Matrix4x4.CreateFromQuaternion(Rotation);
        var mtxSca = Matrix4x4.CreateScale(Scale);
        writer.Write(mtxPos * mtxRot * mtxSca);
        writer.WriteArray(Field_40);
    }
}
