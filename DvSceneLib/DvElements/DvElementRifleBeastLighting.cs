using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Rifle Beast Lighting", "Adds the lighting from the Supreme/The End boss fight")]
public class DvElementRifleBeastLighting : DvNodeElement
{
    public DvElementRifleBeastLighting() { }
    public DvElementRifleBeastLighting(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
    }

    public void Write(BinaryObjectWriter writer)
    {
    }
}
