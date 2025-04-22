using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera Hedgehog", "")]
public class DvElementCameraHedgehog : DvNodeElement
{
    public struct Camera
    {
        public float Unk0;
        public float Unk1;
        public float Unk2;
        public Vector3 Position;
        public Vector3 TargetPosition;
        public Vector3 Rotation;
        public float Unk3;
        public float Unk4;
        public float Unk5;
        public float Unk6;
        public float Unk7;
        public float Unk8;
        public float Unk9;
        public float NearClip;
        public float FarClip;
        public float FOV;
    }

    public int Flags = 0;
    public Camera CameraBefore = new();
    public Camera CameraAfter = new();
    public float Unk0 = 0;
    public float Unk1 = 0;
    public float Unk2 = 0;
    public float[] CurveData = new float[32];

    public DvElementCameraHedgehog() : base(DvElementID.CameraHedgehog) { }
    public DvElementCameraHedgehog(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Flags = reader.Read<int>();
        CameraBefore = reader.Read<Camera>();
        CameraAfter = reader.Read<Camera>();
        Unk0 = reader.Read<float>();
        Unk1 = reader.Read<float>();
        Unk2 = reader.Read<float>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Flags);
        writer.Write(CameraBefore);
        writer.Write(CameraAfter);
        writer.Write(Unk0);
        writer.Write(Unk1);
        writer.Write(Unk2);
        writer.WriteArray(CurveData);
    }
}
