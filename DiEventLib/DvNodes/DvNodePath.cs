using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvNodePath : DvNodeObject
{
    public Vector3 Position;
    public Quaternion Rotation;
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
        // Necessary random Quaternion math
        Rotation = tempRot;
        Flags = reader.Read<uint>();
        reader.Skip(12);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        var mtxPos = Matrix4x4.CreateTranslation(Position);
        var mtxRot = Matrix4x4.CreateFromQuaternion(Rotation);
        var mtxSca = Matrix4x4.CreateScale(Scale);
        writer.Write(mtxPos+mtxRot+mtxSca);
        writer.Write(Flags);
        writer.Skip(12);
    }

}