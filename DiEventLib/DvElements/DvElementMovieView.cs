using Amicitia.IO.Binary;

namespace DiEventLib;

public class DvElementMovieView : DvNodeElement
{
    public DvElementMovieView() { ElementID = DvElementID.MovieView; }
    public DvElementMovieView(BinaryObjectReader reader) : base(DvElementID.MovieView) => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        //ElementRead(reader);
    }

    public void Write(BinaryObjectWriter writer)
    {
    }
}
