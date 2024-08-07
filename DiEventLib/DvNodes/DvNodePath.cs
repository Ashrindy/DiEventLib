using Amicitia.IO.Binary;
using System.Numerics;
using System.Xml.Linq;

namespace DiEventLib;

public class DvNodePath : DvNode
{
    public Vector3 Position = new(0, 0, 0);
    public Vector3 Rotation = new(0, 0, 0);
    public Vector3 Scale = new(1, 1, 1);
    public uint Flags = 0;
    public DvNodePath()
    {
        NodeName = nameof(DvNodePath);
        Priority = 0;
        Flags = 0;
        Guid = Guid.NewGuid();
    }

    public DvNodePath(string name) : base(DvNodeCategory.Path, name)
    {
        //NodeName = name;
        Priority = 0;
        Flags = 0;
        Guid = Guid.NewGuid();
    }

    public DvNodePath(BinaryObjectReader reader)
    {
        Read(reader);
    }

    public void Read(BinaryObjectReader reader)
    {
        //base.Read(reader);
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

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.ComposeMatrix(Position, Scale, Utils.ToQuaternion(Rotation)));
        writer.Write(Flags);
        writer.WriteNulls(12);
    }

}