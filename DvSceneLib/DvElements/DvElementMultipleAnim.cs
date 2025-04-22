using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Text;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Composite Animation", "Can add any type of animation in one singular element")]
public class DvElementMultipleAnim : DvNodeElement
{
    public enum AnimationType : uint
    {
        SkeletalAnimation = 1,
        UVAnimation,
        VisibilityAnimation,
        MaterialAnimation
    }

    public struct Animation
    {
        public AnimationType Type;
        public string FileName;
    }

    public uint Field_60 = 0;
    public string StateName = "DSt0000";
    public uint Field_6c = 0;
    public Animation[] Animations = new Animation[16];
    //public uint ActiveAnimCount { get; set; }
    public DvElementMultipleAnim() : base(DvElementID.MultipleAnim)
    { }
    public DvElementMultipleAnim(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_60 = reader.Read<uint>();
        StateName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS, 8);
        Field_6c = reader.Read<uint>();
        for(int i = 0; i < 16; i++)
        {
            Animations[i] = new();
            Animations[i].Type = reader.Read<AnimationType>();
            Animations[i].FileName = reader.ReadDvString(Utils.StringEncoding.Default, 64);
        }
        var activeAnimCount = reader.Read<uint>();
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Field_60);
        writer.WriteDvString(StateName, Utils.StringEncoding.ShiftJIS, 8);
        writer.Write(Field_6c);
        uint activeAnimCount = 0;
        foreach (var anim in Animations)
        {
            writer.Write(anim.Type);
            writer.WriteDvString(anim.FileName, Utils.StringEncoding.Default);

            // TODO: Find alternative
            if (anim.FileName.Length > 0)
                activeAnimCount++;
        }
        writer.Write(activeAnimCount);
    }
}


