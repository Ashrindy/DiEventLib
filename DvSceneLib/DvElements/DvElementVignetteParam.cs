using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Vignette", "Adds a smooth black border around the screen")]
public class DvElementVignetteParam : DvNodeElement
{
    public enum GradationType : int
    {
        Circle,
        Line
    }

    public enum BlendMode : int
    {
        AlphaBlend,
        Add,
        Mul,
        Screen,
        Overlay
    }

    public struct VignetteParam
    {
        public Vector2 Position;
        public Vector2 Size;
        public float Scale;
        public Vector2 LineDirection;
        public int Opacity;
        public RGB32 Color;
        public float PenumbraScale;
        public float Intensity;
        public float Rotation;
    }

    public struct DepthParam
    {
        public float MinPenumbraScale;
        public float MaxPenumbraScale;
        public float BokehScale;
        public float MinDOFOpacityScale;
        public float MaxDOFOpacityScale;
        public float MinOpacityScale;
        public float MaxOpacityScale;
        public float MinOpacityDist;
        public float MaxOpacityDist;
    }

    public bool DepthEnabled = false;
    public bool CurveEnabled = false;
    public GradationType GradType = GradationType.Circle;
    public VignetteParam Params = new();
    public BlendMode BlendType = BlendMode.AlphaBlend;
    public DepthParam DepthParams = new();
    public VignetteParam FinishParams = new();
    public DepthParam FinishDepthParams = new();
    public float[] CurveData = new float[32];

    public DvElementVignetteParam() : base(DvElementID.VignetteParam) { }
    public DvElementVignetteParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        DepthEnabled = (Flags & 1) != 0;
        CurveEnabled = (Flags & 2) != 0;
        GradType = reader.Read<GradationType>();
        Params = reader.Read<VignetteParam>();
        reader.Skip(4);
        BlendType = reader.Read<BlendMode>();
        DepthParams = reader.Read<DepthParam>();
        FinishParams = reader.Read<VignetteParam>();
        FinishDepthParams = reader.Read<DepthParam>();
        CurveData = reader.ReadArray<float>(32);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (DepthEnabled) Flags |= 1;
        if (CurveEnabled) Flags |= 2;
        writer.Write(Flags);
        writer.Write(GradType);
        writer.Write(Params);
        writer.WriteNulls(4);
        writer.Write(BlendType);
        writer.Write(DepthParams);
        writer.Write(FinishParams);
        writer.Write(FinishDepthParams);
        writer.WriteArray(CurveData);
    }
}
