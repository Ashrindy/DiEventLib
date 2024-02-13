using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementEffect : DvNodeObject
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }
    public Vector3 Scale { get; set; }
    public uint Field9C { get; set; }
    public string FileName { get; set; }
    public uint[] FieldDC { get; set; } // Item 3 could be RGBA in bytes
    public float[] AnimData { get; set; }

    public DvElementEffect() { }
    public DvElementEffect(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        var mtx = reader.Read<Matrix4x4>();
        Quaternion tempRot;
        Vector3 tempPos;
        Vector3 tempSca;
        Matrix4x4.Decompose(mtx, out tempSca, out tempRot, out tempPos);
        Rotation = Utils.ToEulerAngles(tempRot);
        Position = tempPos;
        Scale = tempSca;
        Field9C = reader.Read<uint>();
        FileName = reader.ReadString(StringBinaryFormat.FixedLength, 64);
        FieldDC = reader.ReadArray<uint>(8);
        AnimData = reader.ReadArray<float>(128);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.ComposeMatrix(Position, Scale, Utils.ToQuaternion(Rotation)));
        writer.Write(Field9C);
        writer.WriteString(StringBinaryFormat.FixedLength, FileName, 64);
        writer.WriteArray(FieldDC);
        writer.WriteArray(AnimData);
    }
}
