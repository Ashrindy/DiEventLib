using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvNodePath : DvNodeObject
{
    public Vector3 Position { get; set; } = new(0,0,0);
    public Vector3 Rotation { get; set; } = new(0, 0, 0);
    public Vector3 Scale { get; set; } = new(0, 0, 0);
    public uint Flags { get; set; } = 0;
    public DvNodePath() { }
    public DvNodePath(BinaryObjectReader reader)
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
        Flags = reader.Read<uint>();
        reader.Skip(12);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.ComposeMatrix(Position, Scale, Utils.ToQuaternion(Rotation)));
        writer.Write(Flags);
        writer.WriteNulls(12);
    }

}