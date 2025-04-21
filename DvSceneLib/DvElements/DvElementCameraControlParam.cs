using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera Control", "Edits the camera parameters")]
public class DvElementCameraControlParam : DvNodeElement
{
    public int unk1 = 0;
    public float[] Field_48;
    public float[] Field_80;

    public DvElementCameraControlParam() : base(DvElementID.CameraControlParam)
    {
        Field_48 = new float[7];
        for (int i = 0; i < 7; i++)
        {
            Field_48[i] = 0;
        }
        Field_80 = new float[32];
        for (int i = 0; i < 7; i++)
        {
            Field_80[i] = 0;
        }
    }
    public DvElementCameraControlParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        unk1 = reader.Read<int>();
        Field_48 = reader.ReadArray<float>(7);
        Field_80 = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(unk1);
        writer.WriteArray(Field_48);
        writer.WriteArray(Field_80);
    }
}
