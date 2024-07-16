using Amicitia.IO.Binary;
using System.Numerics;

namespace DiEventLib;

public class DvElementEffect : DvNodeElement
{
    public Vector3 Position { get; set; } = new(0, 0, 0);
    public Vector3 Rotation { get; set; } = new(0, 0, 0);
    public Vector3 Scale { get; set; } = new(0, 0, 0);
    public uint Field9C { get; set; } = 0;
    public string FileName { get; set; } = "";
    public uint[] FieldDC { get; set; } // Item 3 could be RGBA in bytes
    public float[] AnimData { get; set; }

    public DvElementEffect() : base(DvElementID.Effect)
    { 
        FieldDC = new uint[8];
        for(int i = 0; i < 8; i++)
        {
            FieldDC[i] = 0;
        }
        AnimData = new float[128];
        for(int i = 0; i < 128; i++)
        {
            AnimData[i] = 1;
        }
    }
    public DvElementEffect(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
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
        FileName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        FieldDC = reader.ReadArray<uint>(8);
        AnimData = reader.ReadArray<float>(128);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.ComposeMatrix(Position, Scale, Utils.ToQuaternion(Rotation)));
        writer.Write(Field9C);
        writer.WriteDvString(FileName, Utils.StringEncoding.ShiftJIS);
        writer.WriteArray(FieldDC);
        writer.WriteArray(AnimData);
    }
}
