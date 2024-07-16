using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementGameCamera : DvNodeElement
{
    public float[] Field_4c { get; set; }

    public DvElementGameCamera() : base(DvElementID.GameCamera)
    { 
        Field_4c = new float[26];
        for(int i = 0; i < 26; i++)
        {
            Field_4c[i] = 0;
        }
    }
    public DvElementGameCamera(BinaryObjectReader reader)
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
