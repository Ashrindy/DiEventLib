using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Movie View", "Plays a .usm with the same name as the .dvscene")]
public class DvElementMovieView : DvNodeElement
{
    public DvElementMovieView() { ElementID = DvElementID.MovieView; }
    public DvElementMovieView(BinaryObjectReader reader) : base(DvElementID.MovieView) => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        //ElementRead(reader);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
    }
}
