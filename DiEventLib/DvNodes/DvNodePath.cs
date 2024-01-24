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
        Matrix4x4.Decompose(mtx, out Scale, out Rotation, out Position);
        Flags = reader.Read<uint>();
        reader.Skip(12);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }

}