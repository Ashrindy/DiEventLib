using Amicitia.IO.Binary;
using System;
using System.Text;
using System.Collections.Generic;
namespace DiEventLib;


public class DvScene
{
    public DvScene() { }

    public DvScene(string filename) => Read(filename);

    public DvCommon Common = new DvCommon();
    public DvResource Resource = new DvResource();

    public void Read(string filename)
    {
        BinaryObjectReader reader = new(filename, Endianness.Little, Encoding.UTF8);
        reader.OffsetBinaryFormat = OffsetBinaryFormat.U32;
        Common.Pointer = reader.Read<uint>();
        Resource.Pointer = reader.Read<uint>();
        reader.ReadAtOffset(Common.Pointer + 0x20, () => Common.Read(reader));
        reader.ReadAtOffset(Resource.Pointer + 0x20, () => Resource.Read(reader));
    }
    public void Write(string filename)
    {
        BinaryObjectWriter writer = new(filename, Endianness.Little, Encoding.UTF8);
        writer.OffsetBinaryFormat = OffsetBinaryFormat.U32;
        writer.Write(Common.Pointer);
        writer.Write(Resource.Pointer);
        writer.Seek(Common.Pointer + 0x20, SeekOrigin.Begin);
        Common.Write(writer);
        writer.Seek(Resource.Pointer + 0x20, SeekOrigin.Begin);
        Resource.Write(writer);
    }
}

