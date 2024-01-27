using Amicitia.IO.Binary;
using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime;
using System.Reflection.PortableExecutable;

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
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => Common.Read(reader));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => Resource.Read(reader));
        reader.Skip(0x18);
    }
    public void Write(string filename)
    {
        BinaryObjectWriter writer = new(filename, Endianness.Little, Encoding.UTF8);
        writer.OffsetBinaryFormat = OffsetBinaryFormat.U32;
        long commonPointerPos = writer.Position;
        long resourcePointerPos = writer.Position+4;
        writer.WriteNulls(0x20);
        {
            long commonPointer = writer.Position;
            writer.Seek(commonPointerPos, SeekOrigin.Begin);
            writer.Write((uint)commonPointer - 0x20);
            writer.Seek(commonPointer, SeekOrigin.Begin);
            Common.Write(writer);
        }

        {
            long resourcePointer = writer.Position;
            writer.Seek(resourcePointerPos, SeekOrigin.Begin);
            writer.Write((uint)resourcePointer - 0x20);
            writer.Seek(resourcePointer, SeekOrigin.Begin);
            Resource.Write(writer);
        }
        
        writer.Dispose();
    }
}

