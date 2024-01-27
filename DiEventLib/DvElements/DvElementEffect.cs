using Amicitia.IO.Binary;
using System.Numerics;
using System.Text;

namespace DiEventLib;

public class DvElementEffect : DvNodeObject
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;
    public uint Field9C { get; set; }
    public string FileName { get; set; }
    public uint[] FieldDC { get; set; }
    public float[] AnimData { get; set; }

    public DvElementEffect() { }
    public DvElementEffect(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        var mtx = reader.Read<Matrix4x4>();
        Matrix4x4.Decompose(mtx, out Scale, out Rotation, out Position);
        Field9C = reader.Read<uint>();
        FileName = reader.ReadString(StringBinaryFormat.FixedLength, 64);
        FieldDC = reader.ReadArray<uint>(8);
        AnimData = reader.ReadArray<float>(128);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        var mtxPos = Matrix4x4.CreateTranslation(Position);
        var mtxRot = Matrix4x4.CreateFromQuaternion(Rotation);
        var mtxSca = Matrix4x4.CreateScale(Scale);
        writer.Write(mtxPos * mtxRot * mtxSca);
        writer.Write(Field9C);
        writer.WriteString(StringBinaryFormat.FixedLength, FileName, 64);
        writer.WriteArray(FieldDC);
        writer.WriteArray(AnimData);
    }
}
