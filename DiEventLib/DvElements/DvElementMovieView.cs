using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementMovieView : DvNodeObject
{
    public DvElementMovieView() { }
    public DvElementMovieView(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
    }

    public override void Write(BinaryObjectWriter writer)
    {
    }
}
