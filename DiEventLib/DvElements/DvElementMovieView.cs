using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementMovieView : DvNodeElement
{
    public DvElementMovieView() { ElementID = DvElementID.MovieView; }
    public DvElementMovieView(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
    }

    public override void Write(BinaryObjectWriter writer)
    {
    }
}
