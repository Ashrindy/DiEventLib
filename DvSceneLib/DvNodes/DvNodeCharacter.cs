using Amicitia.IO.Binary;

namespace DvSceneLib;

public class DvNodeCharacter : DvNodeBaseAnimationModel
{
    public DvNodeCharacter() { }
    public DvNodeCharacter(BinaryObjectReader reader)
        => Read(reader);
}