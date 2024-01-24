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
            reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => Common.Read(reader));
            reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => Resource.Read(reader));
            Console.WriteLine();
        }
        public void Write()
        {
            throw new NotImplementedException();
        }
    }

