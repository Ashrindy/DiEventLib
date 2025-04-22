using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("CameraParams", "Creates a custom camera")]
public class DvElementCameraParams : DvNodeElement
{
    public struct Camera
    {
        public struct CameraFlags
        {
            public bool EnabledPosition;
            public bool EnabledTargetPosition;
            public bool EnabledFOV;
            public bool EnabledRotation;
        }

        public CameraFlags Flags;
        public Vector3 Position;
        public Vector3 TargetPosition;
        public float FOV;
        public Vector3 Rotation;
    }

    public Camera CameraSetup = new();
    public Camera FinishCameraSetup = new();
    public float[] CurveData = new float[256];

    public DvElementCameraParams() : base(DvElementID.CameraParams) { }
    public DvElementCameraParams(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        CameraSetup.Flags.EnabledPosition = (Flags & 1) != 0;
        CameraSetup.Flags.EnabledTargetPosition = (Flags & 2) != 0;
        CameraSetup.Flags.EnabledFOV = (Flags & 4) != 0;
        CameraSetup.Flags.EnabledRotation = (Flags & 8) != 0;
        FinishCameraSetup.Flags.EnabledPosition = (Flags & 16) != 0;
        FinishCameraSetup.Flags.EnabledTargetPosition = (Flags & 32) != 0;
        FinishCameraSetup.Flags.EnabledFOV = (Flags & 64) != 0;
        FinishCameraSetup.Flags.EnabledRotation = (Flags & 128) != 0;
        CameraSetup.Position = reader.Read<Vector3>();
        CameraSetup.TargetPosition = reader.Read<Vector3>();
        CameraSetup.FOV = reader.Read<float>();
        CameraSetup.Rotation = reader.Read<Vector3>();
        FinishCameraSetup.Position = reader.Read<Vector3>();
        FinishCameraSetup.TargetPosition = reader.Read<Vector3>();
        FinishCameraSetup.FOV = reader.Read<float>();
        FinishCameraSetup.Rotation = reader.Read<Vector3>();
        reader.Skip(20);
        CurveData = reader.ReadArray<float>(256);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (CameraSetup.Flags.EnabledPosition) Flags |= 1;
        if (CameraSetup.Flags.EnabledTargetPosition) Flags |= 2;
        if (CameraSetup.Flags.EnabledFOV) Flags |= 4;
        if (CameraSetup.Flags.EnabledRotation) Flags |= 8;
        if (FinishCameraSetup.Flags.EnabledPosition) Flags |= 16;
        if (FinishCameraSetup.Flags.EnabledTargetPosition) Flags |= 32;
        if (FinishCameraSetup.Flags.EnabledFOV) Flags |= 64;
        if (FinishCameraSetup.Flags.EnabledRotation) Flags |= 128;
        writer.Write(Flags);
        writer.Write(CameraSetup.Position);
        writer.Write(CameraSetup.TargetPosition);
        writer.Write(CameraSetup.FOV);
        writer.Write(CameraSetup.Rotation);
        writer.Write(FinishCameraSetup.Position);
        writer.Write(FinishCameraSetup.TargetPosition);
        writer.Write(FinishCameraSetup.FOV);
        writer.Write(FinishCameraSetup.Rotation);
        writer.WriteNulls(20);
        writer.WriteArray(CurveData);
    }
}
