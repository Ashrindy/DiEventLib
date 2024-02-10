using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementCameraShake : DvNodeObject
{
    public uint Field_00 { get; set; } // Could be in which way the camera shakes? As in, roll, yaw, pitch etc.
    public float Intensity { get; set; }
    public float Frequency { get; set; }

    public DvElementCameraShake() { }
    public DvElementCameraShake(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        reader.Skip(4);
        Field_00 = reader.Read<uint>();
        Intensity = reader.Read<float>();
        Frequency = reader.Read<float>();
        reader.Skip(16);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Skip(4);
        writer.Write(Field_00);
        writer.Write(Intensity);
        writer.Write(Frequency);
        writer.Skip(16);
    }
}
