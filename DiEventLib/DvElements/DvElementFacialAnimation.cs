using Amicitia.IO.Binary;
using DiEventLib.Misc;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Facial Animation", "3 sets of any animation type")]
public class DvElementFacialAnimation : DvNodeElement
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
    public Animation[] Animations = new Animation[3];
    public float[] AnimData;
    //public uint ActiveAnimCount { get; set; }
    public DvElementFacialAnimation() : base(DvElementID.FacialAnimation)
    { }
    public DvElementFacialAnimation(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        for (int i = 0; i < 3; i++)
        {
            Animations[i] = new();
            Animations[i].Type = reader.Read<AnimationType>();
            Animations[i].FileName = reader.ReadDvString(Utils.StringEncoding.Default);
        }
        var activeAnimCount = reader.Read<uint>();
        AnimData = reader.ReadArray<float>(32);
    }

    public void Write(BinaryObjectWriter writer)
    {
        uint activeAnimCount = 0;
        foreach (var anim in Animations)
        {
            writer.Write(anim.Type);
            writer.WriteDvString(anim.FileName, Utils.StringEncoding.Default);
            activeAnimCount++;
        }
        writer.Write(activeAnimCount);
        writer.WriteArray(AnimData);
    }
}
