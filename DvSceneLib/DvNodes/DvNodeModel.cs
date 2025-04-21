using Amicitia.IO.Binary;

namespace DvSceneLib;

public class DvNodeModel : DvNodeBaseAnimationModel
{
    public DvNodeModel() { }
    public DvNodeModel(BinaryObjectReader reader)
        => Read(reader);
}