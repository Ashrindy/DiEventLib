using Amicitia.IO.Binary;

namespace DvSceneLib;

public class DvNodeModelMotion : DvNodeCharacterMotion
{
    public DvNodeModelMotion() { }
    public DvNodeModelMotion(BinaryObjectReader reader)
        => Read(reader);
}