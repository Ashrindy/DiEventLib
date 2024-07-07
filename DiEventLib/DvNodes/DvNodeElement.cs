using Amicitia.IO.Binary;
namespace DiEventLib;

public class DvNodeElement : DvNode
{
    //public IDvNode dvNode { get; set; }
    public DvElementID ElementID { get; set; }
    public float Start { get; set; } = 0f;
    public float End { get; set; } = 0f;
    public int Version { get; set; } = 0;
    public uint Flags { get; set; } = 0;
    public ElementPlayType PlayType { get; set; } = ElementPlayType.Normal;
    public ElementUpdateTiming UpdateTiming { get; set; } = ElementUpdateTiming.OnExecPath;
    //public DvNodeObject Element { get; set; }

    public DvNodeElement() : base(DvNodeCategory.Element)
    {
        NodeName = nameof(DvNodePath);
        Priority = 0;
        Flags = 0;
        Guid = Guid.NewGuid();
    }

    public DvNodeElement(DvElementID elementId) : base(DvNodeCategory.Element)
    {
        ElementID = elementId;
        NodeName = nameof(DvNodePath);
        Priority = 0;
        Flags = 0;
        Guid = Guid.NewGuid();
    }

    public DvNodeElement(string name) : base(DvNodeCategory.Path, name)
    {
        NodeName = name;
        Priority = 0;
        Flags = 0;
        Guid = Guid.NewGuid();
    }

    public DvNodeElement(BinaryObjectReader reader)
    {
        Read(reader);
    }

    public void Read(BinaryObjectReader reader)
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
            case DvElementID.Caption: new DvElementCaption(reader); break; 

            case DvElementID.Fade: new DvElementFade(reader); break;
            case DvElementID.GameCamera: new DvElementGameCamera(reader); break;
            case DvElementID.MovieView: new DvElementMovieView(reader); break;
            case DvElementID.LetterBox: new DvElementLetterBox(reader); break;
        }
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(ElementID);
        writer.Write(Start);
        writer.Write(End);
        writer.Write(Version);
        writer.Write(Flags);
        writer.Write(PlayType);
        writer.Write(UpdateTiming);
        writer.WriteNulls(4);
        //if (unkElementData != null)
        //    writer.WriteArray(unkElementData);
        //else
         //   Element.Write(writer);
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
    ColorContrast = 1002,
    CameraExposure = 1003,
    ShadowResolution = 1004,
    // 1005
    // 1006
    AtmosphereHeightFogParam = 1007,
    ChromaticAberrationFilter = 1008,
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