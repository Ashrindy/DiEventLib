using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Weather", "Changes the weather")]
public class DvElementWeather : DvNodeElement
{
    public uint Field_00 = 0; // could be an enum of some sort, like "sunny" "cloudy" etc.
    public float[] CurveData = new float[32];

    public DvElementWeather() : base(DvElementID.Weather) { }
    public DvElementWeather(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        writer.WriteArray(CurveData);
    }
}
