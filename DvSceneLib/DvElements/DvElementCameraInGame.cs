using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Camera InGame", "")]
public class DvElementCameraInGame : DvNodeElement
{
    public float[] Field_4c = new float[26];

    public DvElementCameraInGame() : base(DvElementID.CameraInGame) { }
    public DvElementCameraInGame(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_4c = reader.ReadArray<float>(26);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteArray(Field_4c);
    }
}
