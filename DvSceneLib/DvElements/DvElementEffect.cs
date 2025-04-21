using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Drawing;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Effect", "Adds a particle effect a specific position, rotation and scale")]
public class DvElementEffect : DvNodeElement
{
    public enum EffectTransType : int
    {
        Node,
        NodeAndFrame,
        NodePosition
    }

    public Vector3 Position = new(0, 0, 0);
    public Vector3 Rotation = new(0, 0, 0);
    public Vector3 Scale = new(1, 1, 1);
    public bool UnkFlag0 = false;
    public bool QuaternionFlag = false;
    public bool UnkFlag1 = false;
    public bool ModelSpaceNodeFlag = false;
    public string FileName = "";
    public bool UnkFlag2 = false;
    public bool UnkFlag3 = false;
    public bool Persistent = false;
    public RGBA8 EffectColor = new();
    public int Unk0 = 0;
    public float Unk1 = 0;
    public EffectTransType TransType = EffectTransType.Node;
    public int Unk2 = 0;
    public int Unk3 = 0;
    public float[] CurveData = new float[128];

    public DvElementEffect() : base(DvElementID.Effect) { }
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
        var Flags0 = reader.Read<uint>();
        UnkFlag0 = (Flags0 & 1) != 0;
        QuaternionFlag = (Flags0 & 2) != 0;
        UnkFlag1 = (Flags0 & 4) != 0;
        ModelSpaceNodeFlag = (Flags0 & 8) != 0;
        FileName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        UnkFlag2 = reader.Read<bool>();
        reader.Align(4);
        var Flags1 = reader.Read<uint>();
        UnkFlag3 = (Flags1 & 1) != 0;
        Persistent = (Flags1 & 2) != 0;
        EffectColor.B = reader.Read<byte>();
        EffectColor.G = reader.Read<byte>();
        EffectColor.R = reader.Read<byte>();
        EffectColor.A = reader.Read<byte>();
        Unk0 = reader.Read<int>();
        Unk1 = reader.Read<float>();
        TransType = reader.Read<EffectTransType>();
        Unk2 = reader.Read<int>();
        Unk3 = reader.Read<int>();
        CurveData = reader.ReadArray<float>(128);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.ComposeMatrix(Position, Scale, Utils.ToQuaternion(Rotation)));
        var Flags0 = 0;
        if (UnkFlag0) Flags0 |= 1;
        if (QuaternionFlag) Flags0 |= 2;
        if (UnkFlag1) Flags0 |= 4;
        if (ModelSpaceNodeFlag) Flags0 |= 8;
        writer.Write(Flags0);
        writer.WriteDvString(FileName, Utils.StringEncoding.ShiftJIS);
        writer.Write(UnkFlag2);
        writer.Align(4);
        var Flags1 = 0;
        if (UnkFlag3) Flags1 |= 1;
        if (Persistent) Flags1 |= 2;
        writer.Write(EffectColor.B);
        writer.Write(EffectColor.G);
        writer.Write(EffectColor.R);
        writer.Write(EffectColor.A);
        writer.Write(Unk0);
        writer.Write(Unk1);
        writer.Write(TransType);
        writer.Write(Unk2);
        writer.Write(Unk3);
        writer.WriteArray(CurveData);
    }
}
