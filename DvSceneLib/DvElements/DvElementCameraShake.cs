using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera Shake", "Shakes the camera for a certain amount of time")]
public class DvElementCameraShake : DvNodeElement
{
    public bool Enabled = true;
    public float Intensity = 0;
    public float Frequency = 0;

    public DvElementCameraShake() : base(DvElementID.CameraShake)
    { }
    public DvElementCameraShake(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        reader.Skip(4);
        Enabled = reader.Read<bool>();
        reader.Align(4);
        Intensity = reader.Read<float>();
        Frequency = reader.Read<float>();
        reader.Skip(16);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Skip(4);
        writer.Write(Enabled);
        writer.Align(4);
        writer.Write(Intensity);
        writer.Write(Frequency);
        writer.Skip(16);
    }
}
