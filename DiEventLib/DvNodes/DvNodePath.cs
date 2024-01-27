using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvNodePath : DvNodeObject
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public uint Flags { get; set; }
    public DvNodePath() { }
    public DvNodePath(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        var mtx = reader.Read<Matrix4x4>();
        Quaternion tempRot;
        Matrix4x4.Decompose(mtx, out Scale, out tempRot, out Position);
        Rotation = Utils.ToEulerAngles(tempRot);
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