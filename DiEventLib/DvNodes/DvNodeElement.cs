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
    public int NodeSize { get; set; }

    public DvNodeElement() { }
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
            /*
            case DvElementID.QTE:
                Element = new DvElementQTE(reader);
                break;
            */
            case DvElementID.MovieView:
                Element = new DvElementMovieView(reader);
                break;
            case DvElementID.OpeningLogo:
                Element = new DvElementOpeningLogo(reader);
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
        writer.Skip(4);
        // writer.Skip(NodeSize);
        switch (ElementID)
        {
            case DvElementID.GameCamera:
                Element.Write(writer);
                break;
            case DvElementID.Fade:
                Element.Write(writer);
                break;
            case DvElementID.LetterBox:
                Element.Write(writer);
                break;
            case DvElementID.Caption:
                Element.Write(writer);
                break;
            /*
            case DvElementID.QTE:
                Element = new DvElementQTE(reader);
                break;
            */
            case DvElementID.MovieView:
                Element.Write(writer);
                break;
            case DvElementID.OpeningLogo:
                Element.Write(writer);
                break;

            default:
                writer.Skip(NodeSize);
                break;
        }
    }
}

public enum DvElementID : uint
{
    // ParameterSpecifiedCamera = 1,
    DrawOff = 3,
    PathAdjustment = 5,
    CameraShake = 6,
    CameraShakeLoop = 7,
    Effect = 8,
    PathInterpolation = 10,
    Culling = 11,
    NearFarSetting = 12,
    UVAnimation = 13,
    VisibilityAnimation = 14,
    MaterialAnimation = 15,
    CompositeAnimation = 16,
    CameraOffset = 17,
    // ModelFade = 18,
    SonicCamera = 20,
    GameCamera = 21,
    //SpotlightModel = 26,

    DOF = 1001,
    ColorCorrection = 1002,
    CameraExposure = 1003,
    ShadowResolution = 1004,
    AtmosphereHeightFogParam = 1007,
    ChromaticAberration = 1008,
    VignetteParam = 1009,
    Fade = 1010,
    LetterBox = 1011,
    ModelClipping = 1012,
    BossName = 1014,
    Caption = 1015,
    Sound = 1016,
    Time = 1017,
    Sun = 1018,
    LookAtIK = 1019,
    CameraBlurParam = 1020,
    GeneralTrigger = 1021,
    DitherParam = 1023,
    QTE = 1024,
    LipAnimation = 1025,
    OverrideAsm = 1026,
    Aura = 1027,
    ChangeTimeScale = 1028,
    CyberSpaceNoise = 1029,
    AuraRoad = 1031,
    MovieView = 1032,
    Weather = 1034,
    VariablePointLight = 1036,
    OpeningLogo = 1037,
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
    Normal = 0x0,
    Oneshot = 0x1,
    Always = 0x2,
};