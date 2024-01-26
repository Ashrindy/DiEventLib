using Amicitia.IO.Binary;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DiEventLib;

public class DvElementCompositeAnimation : DvNodeObject
{
    public uint Field_60 { get; set; }
    public byte[] Data { get; set; }
    public uint Field_6c { get; set; }
    public Anim[] Animations { get; set; }
    public uint Field_03 { get; set; }
    public DvElementCompositeAnimation() { }
    public DvElementCompositeAnimation(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_60 = reader.Read<uint>();
        Data = reader.ReadArray<byte>(8);
        Field_6c = reader.Read<uint>();
        Animations = new Anim[16];
        for(int i = 0; i < 16; i++)
        {
            Animations[i] = new();
            Animations[i].AnimType = reader.Read<AnimType>();
            Animations[i].FileName = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 64);
        }
        Field_03 = reader.Read<uint>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_60);
        writer.WriteArray(Data);
        writer.Write(Field_6c);
        foreach(var anim in Animations)
        {
            writer.Write(anim.AnimType);
            writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, anim.FileName, 64);
        }
        writer.Write(Field_03);
    }
}

public enum AnimType : uint
{
    SkeletalAnim = 1,
    UVAnim,
    MatAnim = 4
}

public struct Anim
{
    public AnimType AnimType;
    public string FileName;
}
