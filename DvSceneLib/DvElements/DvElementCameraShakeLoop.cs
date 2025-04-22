using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera Shake Loop", "Shakes the camera indefinitely")]
public class DvElementCameraShakeLoop : DvNodeElement
{
    public bool Pattern = false;
    public bool Enabled = false;
    public float Unk0 = 0;
    public float Unk1 = 0;
    public float[] CurveData = new float[64];

    public DvElementCameraShakeLoop() : base(DvElementID.CameraShakeLoop) { }
    public DvElementCameraShakeLoop(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Pattern = reader.Read<bool>();
        reader.Align(4);
        Enabled = reader.Read<bool>();
        reader.Align(4);
        Unk0 = reader.Read<float>();
        Unk1 = reader.Read<float>();
        reader.Skip(16);
        CurveData = reader.ReadArray<float>(64);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Pattern);
        writer.Align(4);
        writer.Write(Enabled);
        writer.Align(4);
        writer.Write(Unk0);
        writer.Write(Unk1);
        writer.WriteNulls(16);
        writer.WriteArray(CurveData);
    }
}
