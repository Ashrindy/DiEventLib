using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Shadow Map Param", "Edits the shadow map parameters")]
public class DvElementShadowMapParam : DvNodeElement
{
    public enum ShadowFilter : int
    {
        Point,
        PCF,
        PCSS,
        ESM,
        MSM,
        VSMPoint,
        VSMLinear,
        VSMAniso2,
        VSMAniso4,
        VSMAniso8,
        VSMAniso16,
        VSMFirst = 12,
        VSMLast
    }

    public enum ShadowRangeType : int
    {
        CameraLookAt,
        PositionManual,
        FullManual
    }

    public enum FitProjection : int
    {
        ToCascades,
        ToScene,
        ToRotateCascades
    }

    public enum FitNearFar : int
    {
        ZeroOne,
        AABB,
        SceneAABB
    }

    public enum PartitionType : int
    {
        PSSM,
        Manual
    }

    public ShadowFilter Filter = ShadowFilter.Point;
    public ShadowRangeType RangeType = ShadowRangeType.CameraLookAt;
    public FitProjection FitProj = FitProjection.ToCascades;
    public FitNearFar FitNearFarClip = FitNearFar.ZeroOne;
    public float SceneRange = 0;
    public Vector3 SceneCenter = new(0, 0, 0);
    public Vector3 ManualLightPosition = new(0, 0, 0);
    public float PSSMLambda = 0;
    public float CascadeOffset = 0;
    public int CascadeLevel = 0;
    public float[] CascadeSplits = new float[4];
    public float[] CascadeBias = new float[4];
    public float Bias = 0;
    public float Offset = 0;
    public float NormalBias = 0;
    public int BlurQuality = 0;
    public int BlurSize = 0;
    public float FadeOutDistance = 0;
    public float CascadeTransitionFadeDistance = 0;
    public bool BackFaceShadow = false;
    public bool ShadowCamera = false;
    public bool DrawSceneAABB = false;
    public bool DrawShadowFrustrum = false;
    public bool DrawCascade = false;
    public bool DrawCameraFrustrum = false;
    public bool PauseCamera = false;

    public DvElementShadowMapParam() : base(DvElementID.ShadowMapParam) { }
    public DvElementShadowMapParam(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Filter = reader.Read<ShadowFilter>();
        RangeType = reader.Read<ShadowRangeType>();
        FitProj = reader.Read<FitProjection>();
        FitNearFarClip = reader.Read<FitNearFar>();
        SceneRange = reader.Read<float>();
        SceneCenter = reader.Read<Vector3>();
        ManualLightPosition = reader.Read<Vector3>();
        PSSMLambda = reader.Read<float>();
        CascadeOffset = reader.Read<float>();
        CascadeLevel = reader.Read<int>();
        CascadeSplits = reader.ReadArray<float>(4);
        CascadeBias = reader.ReadArray<float>(4);
        Bias = reader.Read<float>();
        Offset = reader.Read<float>();
        NormalBias = reader.Read<float>();
        reader.Skip(8);
        BlurQuality = reader.Read<int>();
        BlurSize = reader.Read<int>();
        FadeOutDistance = reader.Read<float>();
        CascadeTransitionFadeDistance = reader.Read<float>();
        var Flags = reader.Read<uint>();
        BackFaceShadow = (Flags & 1) != 0;
        ShadowCamera = (Flags & 2) != 0;
        DrawSceneAABB = (Flags & 4) != 0;
        DrawShadowFrustrum = (Flags & 8) != 0;
        DrawCascade = (Flags & 16) != 0;
        DrawCameraFrustrum = (Flags & 32) != 0;
        PauseCamera = (Flags & 64) != 0;
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(Filter);
        writer.Write(RangeType);
        writer.Write(FitProj);
        writer.Write(FitNearFarClip);
        writer.Write(SceneRange);
        writer.Write(SceneCenter);
        writer.Write(ManualLightPosition);
        writer.Write(PSSMLambda);
        writer.Write(CascadeOffset);
        writer.Write(CascadeLevel);
        writer.WriteArray(CascadeSplits);
        writer.WriteArray(CascadeBias);
        writer.Write(Bias);
        writer.Write(Offset);
        writer.Write(NormalBias);
        writer.WriteNulls(8);
        writer.Write(BlurQuality);
        writer.Write(BlurSize);
        writer.Write(FadeOutDistance);
        writer.Write(CascadeTransitionFadeDistance);
        var Flags = 0;
        if (BackFaceShadow) Flags |= 1;
        if (ShadowCamera) Flags |= 2;
        if (DrawSceneAABB) Flags |= 4;
        if (DrawShadowFrustrum) Flags |= 8;
        if (DrawCascade) Flags |= 16;
        if (DrawCameraFrustrum) Flags |= 32;
        if (PauseCamera) Flags |= 64;
        writer.Write(Flags);
    }
}
