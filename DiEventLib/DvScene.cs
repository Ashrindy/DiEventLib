using Amicitia.IO.Binary;
using DiEventLib.IO.Template;
using System.Text;

namespace DiEventLib;

public class DvScene
{
    public enum DvSceneVersion
    {
        rangers,
        miller
    }

    public DvScene() { }

    public DvScene(string filename) => Open(filename);

    public DvCommon Common = new DvCommon();
    public DvResource Resource = new DvResource();

    public static DvSceneVersion Version = DvSceneVersion.rangers;

    public void Open(string filename, DiEventDataBase db = null) => Read(new(filename, Endianness.Little, Encoding.UTF8), db);
    public void Save(string filename, DiEventDataBase db = null) => Write(new(filename, Endianness.Little, Encoding.UTF8), db);

    public void Read(BinaryObjectReader reader, DiEventDataBase db)
    {
        reader.OffsetBinaryFormat = OffsetBinaryFormat.U32;
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => Common.Read(reader, db));
        reader.ReadAtOffset(reader.Read<uint>() + 0x20, () => Resource.Read(reader));
        reader.Skip(0x18);
    }

    public void Write(BinaryObjectWriter writer, DiEventDataBase db = null)
    {
        writer.OffsetBinaryFormat = OffsetBinaryFormat.U32;
        long commonPointerPos = writer.Position;
        long resourcePointerPos = writer.Position + 4;
        writer.WriteNulls(0x20);
        {
            long commonPointer = writer.Position;
            writer.Seek(commonPointerPos, SeekOrigin.Begin);
            writer.Write((uint)commonPointer - 0x20);
            writer.Seek(commonPointer, SeekOrigin.Begin);
            Common.Write(writer, db);
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

