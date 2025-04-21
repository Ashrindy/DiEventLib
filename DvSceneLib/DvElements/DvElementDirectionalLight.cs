using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Directional Light", "Creates a directional light")]
public class DvElementDirectionalLight : DvNodeElement
{
    public int Unk0 = 0;
    public Vector3 Direction = new();
    public int[] Unk1 = new int[8];

    public DvElementDirectionalLight() : base(DvElementID.DirectionalLight) { }
    public DvElementDirectionalLight(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Unk0 = reader.Read<int>();
        Direction = reader.Read<Vector3>();
        Unk1 = reader.ReadArray<int>(8);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Unk0);
        writer.Write(Direction);
        writer.WriteArray(Unk1);
    }
}
