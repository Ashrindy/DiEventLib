﻿using Amicitia.IO.Binary;
using System.Text;

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
