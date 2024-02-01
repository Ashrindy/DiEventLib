using Amicitia.IO.Binary;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DiEventLib;

public class DvElementCompositeAnimation : DvNodeObject
{
    public uint Field_60 { get; set; }
    public string StateName { get; set; }
    public uint Field_6c { get; set; }
    public Animation[] Animations { get; set; } = new Animation[16];
    //public uint ActiveAnimCount { get; set; }
    public DvElementCompositeAnimation() { }
    public DvElementCompositeAnimation(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_60 = reader.Read<uint>();
        StateName = reader.ReadString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, 8);
        Field_6c = reader.Read<uint>();
        for(int i = 0; i < 16; i++)
        {
            Animations[i] = new();
            Animations[i].Type = reader.Read<AnimationType>();
            Animations[i].FileName = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, 64);
        }
        var activeAnimCount = reader.Read<uint>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_60);
        writer.WriteString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, StateName, 8);
        writer.Write(Field_6c);
        uint activeAnimCount = 0;
        foreach (var anim in Animations)
        {
            writer.Write(anim.Type);
            writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, anim.FileName, 64);

            // TODO: Find alternative
            if (anim.FileName.Length > 0)
                activeAnimCount++;
        }
        writer.Write(activeAnimCount);
    }
}

public enum AnimationType : uint
{
    SkeletalAnimation = 1,
    UVAnimation,
    MaterialAnimation = 4
}

public struct Animation
{
    public AnimationType Type;
    public string FileName;
}
