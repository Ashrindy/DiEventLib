using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera Shake", "Shakes the camera for a certain amount of time")]
public class DvElementCameraShake : DvNodeElement
{
    public uint Field_00 = 0; // Could be in which way the camera shakes? As in, roll, yaw, pitch etc.
    public float Intensity = 0;
    public float Frequency = 0;

    public DvElementCameraShake() : base(DvElementID.CameraShake)
    { }
    public DvElementCameraShake(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        reader.Skip(4);
        Field_00 = reader.Read<uint>();
        Intensity = reader.Read<float>();
        Frequency = reader.Read<float>();
        reader.Skip(16);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Skip(4);
        writer.Write(Field_00);
        writer.Write(Intensity);
        writer.Write(Frequency);
        writer.Skip(16);
    }
}
