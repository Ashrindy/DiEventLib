using Amicitia.IO.Binary;
using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
namespace DiEventLib;

public class DvNodeElement : DvNodeObject
{
    public DvElementID ElementID { get; set; }
    public float Start { get; set; }
    public float End { get; set; }
    public int Version { get; set; }
    public uint Flags { get; set; }
    public ElementPlayType PlayType { get; set; }
    public ElementUpdateTiming UpdateTiming { get; set; }
    public DvNodeObject Element { get; set; }

    // TODO: Remove when all elements will be researched
    public int NodeSize { get; set; }

    public DvNodeElement() { }

    // TODO: Remove contructor with node size when all elements will be researched
    public DvNodeElement(BinaryObjectReader reader, int size)
    {
        NodeSize = size - 32;
        Read(reader);
    }
    public override void Read(BinaryObjectReader reader)
    {
        ElementID = reader.Read<DvElementID>();
        Start = reader.Read<float>();
        End = reader.Read<float>();
        Version = reader.Read<int>();
        Flags = reader.Read<uint>();
        PlayType = reader.Read<ElementPlayType>();
        UpdateTiming = reader.Read<ElementUpdateTiming>();
        reader.Skip(4);
        switch (ElementID)
        {
            case DvElementID.GameCamera:
                Element = new DvElementGameCamera(reader);
                break;
            case DvElementID.Fade:
                Element = new DvElementFade(reader);
                break;
            case DvElementID.LetterBox:
                Element = new DvElementLetterBox(reader);
                break;
            case DvElementID.Caption:
                Element = new DvElementCaption(reader);
                break;
            case DvElementID.DOF:
                Element = new DvElementDOF(reader);
                break;
            case DvElementID.PathAdjustment:
                Element = new DvElementPathAdjustment(reader);
                break;
            case DvElementID.PathInterpolation:
                Element = new DvElementPathInterpolation(reader);
                break;
            case DvElementID.SonicCamera:
                Element = new DvElementSonicCamera(reader);
                break;
            case DvElementID.Sound:
                Element = new DvElementSound(reader);
                break;
            case DvElementID.Time:
                Element = new DvElementTime(reader);
                break;
            case DvElementID.VignetteParam:
                Element = new DvElementVignette(reader);
                break;
            case DvElementID.Weather:
                Element = new DvElementWeather(reader);
                break;
            case DvElementID.ShadowResolution:
                Element = new DvElementShadowResolution(reader);
                break;
            case DvElementID.RifleBeastLighting:
                Element = new DvElementRifleBeastLighting(reader);
                break;
            case DvElementID.DrawOff:
                Element = new DvElementDrawOff(reader);
                break;
            case DvElementID.NearFarSetting:
                Element = new DvElementNearFarSetting(reader);
                break;
            case DvElementID.CameraBlurParam:
                Element = new DvElementCameraBlur(reader);
                break;
            case DvElementID.CameraExposure:
                Element = new DvElementCameraExposure(reader);
                break;
            case DvElementID.CameraOffset:
                Element = new DvElementCameraOffset(reader);
                break;
            case DvElementID.CameraShake:
                Element = new DvElementCameraShake(reader);
                break;
            case DvElementID.CameraShakeLoop:
                Element = new DvElementCameraShakeLoop(reader);
                break;
            case DvElementID.Aura:
                Element = new DvElementAura(reader);
                break;
            case DvElementID.AuraRoad:
                Element = new DvElementAuraRoad(reader);
                break;
            case DvElementID.BossName:
                Element = new DvElementBossName(reader);
                break;
            case DvElementID.ChangeTimeScale:
                Element = new DvElementChangeTimeScale(reader);
                break;
            case DvElementID.Culling:
                Element = new DvElementCulling(reader);
                break;
            case DvElementID.Effect:
                Element = new DvElementEffect(reader);
                break;
            case DvElementID.QTE:
                Element = new DvElementQTE(reader);
                break;
            case DvElementID.ChromaticAberration:
                Element = new DvElementChromaticAberration(reader);
                break;
            case DvElementID.ColorCorrection:
                Element = new DvElementColorCorrection(reader);
                break;
            case DvElementID.CompositeAnimation:
                Element = new DvElementCompositeAnimation(reader);
                break;
            case DvElementID.CyberSpaceNoise:
                Element = new DvElementCyberSpaceNoise(reader);
                break;
            case DvElementID.DitherParam:
                Element = new DvElementDither(reader);
                break;
            case DvElementID.AtmosphereHeightFogParam:
                Element = new DvElementAtmosphereHeightFogParam(reader);
                break;
            case DvElementID.GeneralTrigger:
                Element = new DvElementGeneralTrigger(reader);
                break;
            case DvElementID.LookAtIK:
                Element = new DvElementLookAtIK(reader);
                break;
            case DvElementID.MaterialAnimation:
                Element = new DvElementMaterialAnimation(reader);
                break;
            case DvElementID.ModelClipping:
                Element = new DvElementModelClipping(reader);
                break;
            case DvElementID.OverrideASM:
                Element = new DvElementOverrideASM(reader);
                break;
            case DvElementID.Sun:
                Element = new DvElementSun(reader);
                break;
            case DvElementID.TheEndCableObject:
                Element = new DvElementTheEndCableObject(reader);
                break;
            case DvElementID.VariablePointLight:
                Element = new DvElementVariablePointLight(reader);
                break;
            case DvElementID.VisibilityAnimation:
                Element = new DvElementVisibilityAnimation(reader);
                break;
            case DvElementID.MovieView:
                Element = new DvElementMovieView(reader);
                break;
            case DvElementID.OpeningLogo:
                Element = new DvElementOpeningLogo(reader);
                break;
            case DvElementID.UVAnimation:
                Element = new DvElementUVAnimation(reader);
                break;
            case DvElementID.Spotlight:
                Element = new DvElementSpotlight(reader);
                break;
            case DvElementID.ModelFade:
                Element = new DvElementModelFade(reader);
                break;
            case DvElementID.AdditionRange:
                Element = new DvElementAdditionRange(reader);
                break;
            case DvElementID.Bloom:
                Element = new DvElementBloom(reader);
                break;
            case DvElementID.ShadowMapParam:
                Element = new DvElementShadowMapParam(reader);
                break;
            case DvElementID.VertexAnimation:
                Element = new DvElementVertexAnimation(reader);
                break;
            case DvElementID.LipAnimation:
                Element = new DvElementLipAnimation(reader);
                break;
            case DvElementID.CrossFade:
                Element = new DvElementCrossFade(reader);
                break;

            default:
                reader.Skip(NodeSize);
                break;
        }
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(ElementID);
        writer.Write(Start);
        writer.Write(End);
        writer.Write(Version);
        writer.Write(Flags);
        writer.Write(PlayType);
        writer.Write(UpdateTiming);
        writer.WriteNulls(4);
        Element.Write(writer);
    }
}

// TODO: Need to find rest
public enum DvElementID : uint
{
    // ParameterSpecifiedCamera = 1,
    // 2
    DrawOff = 3,
    // 4
    PathAdjustment = 5,
    CameraShake = 6,
    CameraShakeLoop = 7,
    Effect = 8,
    // 9
    PathInterpolation = 10,
    Culling = 11,
    NearFarSetting = 12,
    UVAnimation = 13,
    VisibilityAnimation = 14,
    MaterialAnimation = 15,
    CompositeAnimation = 16,
    CameraOffset = 17,
    ModelFade = 18,
    // 19
    SonicCamera = 20,
    GameCamera = 21,
    // 22
    VertexAnimation = 23,
    Spotlight = 24,
    // 25
    SpotlightModel = 26,

    Bloom = 1000,
    DOF = 1001,
    ColorCorrection = 1002,
    CameraExposure = 1003,
    ShadowResolution = 1004,
    // 1005
    // 1006
    AtmosphereHeightFogParam = 1007,
    ChromaticAberration = 1008,
    VignetteParam = 1009,
    Fade = 1010,
    LetterBox = 1011,
    ModelClipping = 1012,
    // 1013
    BossName = 1014,
    Caption = 1015,
    Sound = 1016,
    Time = 1017,
    Sun = 1018,
    LookAtIK = 1019,
    CameraBlurParam = 1020,
    GeneralTrigger = 1021,
    // 1022
    DitherParam = 1023,
    QTE = 1024,
    LipAnimation = 1025,
    OverrideASM = 1026,
    Aura = 1027,
    ChangeTimeScale = 1028,
    CyberSpaceNoise = 1029,
    AuraRoad = 1031,
    MovieView = 1032,
    CrossFade = 1033,
    Weather = 1034,
    ShadowMapParam = 1035,
    VariablePointLight = 1036,
    OpeningLogo = 1037,
    AdditionRange = 1038, // i have no clue what this is
    // 1039
    // 1040
    // 1041
    TheEndCableObject = 1042,
    RifleBeastLighting = 1043
};

public enum ElementPlayType : uint
{
    Normal = 0x0,
    Oneshot = 0x1,
    Always = 0x2,
};

public enum ElementUpdateTiming : uint
{
    OnExecPath = 0,
    OnPreUpdate = 1,
    CharacterFixPosture = 2,
    OnPostUpdateCharacter = 3,
    OnUpdatePos = 4,
    OnFixBonePosture = 5,
    OnEvaluateDetailMotion = 6,
    CharacterJobUpdate = 7,
    ModifyPoseAfter = 8,
    JobRegister = 9,
    MotionUpdate = 10,
    Normal = 2
};