using System.Numerics;

namespace DiEventLib
{
    public class DiEvent
    {
        public DvSceneHeader sceneHeader;
        public Common common;
        public Resources resources;

        public DiEvent(DvSceneHeader sceneHeader, Common common, Resources resources)
        {
            this.sceneHeader = sceneHeader;
            this.common = common;
            this.resources = resources;
        }
    }

    public class DvSceneHeader
    {
        public uint commonPointer;
        public uint resourcePointer;
    }

    public class Common
    {
        public float frameStart;
        public float frameEnd;
        public uint drawNodeNum;
        public uint cutInfoPointer;
        public uint authPagePointer;
        public uint disableFrameInfoPointer;
        public uint resourceCutInfoPointer;
        public uint soundCutInfoPointer;
        public uint nodeInfoPointer;
        public float chainCameraIn;
        public float chainCameraOut;
        public int type;
        public int skipPointTick;
        public DvCutInfo cutInfo;
        public DvAuthPage authPage;
        public DvDisableFrameInfo disableFrame;
        public DvResourceCutInfo resourceCut;
        public DvSoundInfo soundCut;
        public List<node> nodes;
    }

    public struct infoObject
    {
        public uint count;
        public uint size;
    }

    public struct DvCutInfo
    {
        public infoObject cutInfoObject;
        public List<float> cutFrame;
    }

    public struct DvAuthPage
    {
        public infoObject authPageObject;
        public List<float> authPage;
    }

    public struct DvDisableFrameInfo
    {
        public infoObject disableFrameInfoObject;
        public List<float> disableFrame;
    }

    public struct DvResourceCutInfo
    {
        public infoObject resourceCutInfoObject;
        public List<float> resourceCutFrame;
    }

    public struct DvSoundInfo
    {
        public infoObject soundInfoObject;
        public List<float> soundFrame;
    }

    public class Resources
    {
        public infoObject resourceObject;
        public List<resource> resources;
    }

    public class resource
    {
        public Guid guid;
        public resourceKind resourceType;
        public uint flags;
        public uint field_18;
        public string filename;
        public List<byte> data;
    }

    public enum resourceKind
    {
        dummy = 0,
        tex = 1,
        character = 2,
        model = 3,
        motionCamera = 4,
        motionPath = 5,
        motionModel = 6,
        motionCharacter = 7,
        equipModel = 8,
        behavior = 9
    }

    public class node
    {
        public Guid guid;
        public elementCategory category;
        public int nodeSize;
        public int childCount;
        public uint flags;
        public int priority;
        public byte[] padding;
        public string name;
        public object info;
        public List<node> children;
    }

    public struct rootPathInfo
    {
        public Matrix4x4 matrix;
        public uint flag;
        public byte[]? padding;
    }

    public struct cameraInfo
    {
        public uint flag;
        public uint frameProgressionCount;
        public uint captionCount;
        public byte[] padding;
        public float[] frameProgression;
        public float[] frameProgressionSpeed;
    }

    public struct cameraMotionInfo
    {
        public uint flag;
        public uint frameStart;
        public uint frameEnd;
        public byte[] padding;
    }

    public struct characterInfo
    {
        public uint field_00;
        public string name1;
        public string name2;
        public string name3;
        public char[] unk;
    }

    public struct characterMotionInfo
    {
        public uint flag;
        public uint frameStart;
        public uint frameEnd;
        public uint field_0c;
        public char[] asmStateName;
        public float field_50;
        public uint field_54;
        public uint[] field_58;
    }

    public struct characterBehaviorInfo
    {
        public byte[] unkBytes;
    }

    public struct modelCustomInfo
    {
        public uint field_00;
        public string name1;
        public string name2;
        public string name3;
        public char[] unk;
    }

    public struct motionModelInfo
    {
        public uint field_00;
        public uint frameStart;
        public uint frameEnd;
        public uint field_0c;
        public char[] asmStateName;
        public float field_50;
        public uint field_54;
        public uint[] field_58;
    }

    public struct modelNodeInfo
    {
        public uint field_00;
        public string name1;
        public byte[] padding;
    }

    #region elementStuff
    public struct elementProperties
    {
        public elementID elementID;
        public float frameStart;
        public float frameEnd;
        public int version;
        public uint flag;
        public playType playType;
        public updateTiming updateTiming;
        public uint padding;
        public object info;
    }

    #region elementInfo
    public struct drawOff
    {
        public uint[] field_00;
    }

    public struct pathAdjustment
    {
        public Matrix4x4 matrix;
        public uint[] field_40;
    }

    public struct cameraShake
    {
        
    }

    public struct cameraShakeLoop
    {
        public uint field_60;
        public uint field_64;
        public float[] field_68;
        public float[] field_curveData;
    }

    public struct effect
    {
        public Matrix4x4 matrix;
        public uint field_9c;
        public string filename;
        public uint[] field_dc;
        public float[] animData;
    }

    public struct pathInterpolation
    {
        public char[] data;
    }

    public struct culling
    {

    }

    public struct uvAnim
    {
        public uint field_00;
        public string filename;
        public uint Field44;
        public float Field48;
        public uint Field4C;
        public uint Field50;
    }

    public struct visAnim
    {
        public uint field_40;
        public string filename;
        public char[] data1;
    }

    public struct matAnim
    {
        public uint field_00;
        public string filename;
        public uint Field44;
        public float Field48;
        public uint Field4C;
        public uint Field50;

    }

    public enum animType
    {
        skeletalAnim = 1,
        UVAnim,
        matAnim = 4
    }

    public struct anim
    {
        public animType animType;
        public string filename;
    }

    public struct compAnim
    {
        public uint field_60;
        public char[] data;
        public uint field_6c;
        public anim[] animations;
        public uint field_03;
    }

    public struct cameraOffset
    {
        public float[] data;
        public float[] animData;
    }

    public struct sonicCam
    {
        public float[] field_4c;
    }

    public struct gameCam
    {
        public float[] field_4c;
    }

    public struct dof
    {
        public uint field_60;
        public float field_64;
        public float field_68;
        public float field_6c;
        public float far1;
        public float field_74;
        public float field_78;
        public float field_7c;
        public float far2;
        public float field_84;
        public float field_88;
        public uint field_8c;
        public uint field_90;
        public float field_94;
        public float field_98;
        public float field_9c;
        public float field_a0;
        public float field_a4;
        public float field_a8;
        public float field_ac;
        public float[] animData;
    }

    public struct camExposure
    {
        public int unk1;
        public float[] field_48;
        public float[] field_80;
    }

    public struct shadowRes
    {
        public uint shadowRes1;
        public uint shadowRes2;
    }

    public struct chromaticAberration
    {
        public float[] data;
        public float[] data1;
    }

    public struct vignette
    {
        public float[] data1;
        public int data2;
        public float[] data3;
        public int data4;
        public float[] data5;
        public float[] data6;
    }

    public struct RGBA32
    {
        public uint A;
        public uint B;
        public uint G;
        public uint R;
    }

    public struct fade
    {
        public RGBA32 color;
        public float[] curveData;
    }

    public struct letterBox
    {
        public float[] curveData;
    }

    public struct modelClipping
    {
        public char[] data;
    }

    public enum languageType
    {
        english = 0,
        french,
        italian,
        german,
        spanish,
        polish,
        portuguese,
        russian,
        japanese,
        chinese,
        chinese_simplified,
        korean
    }

    public struct caption
    {
        public char[] captionName;
        public languageType languageType;
        public uint padding;
    }

    public struct sound
    {
        public string cueName;
        public uint field_a0;
        public uint field_a4;
    }

    public struct time
    {
        public char[] data;
    }

    public struct lookAtIK
    {
        public uint field_60;
        public uint field_64;
        public Guid guid;
        public uint[] field_78;
        public float[] field_80;
    }

    public struct camBlur
    {
        public uint flag;
        public uint blurAmount;
        public float[] curveData;
    }

    public struct generalTrigger
    {
        public uint field_00;
        public string triggerName;
    }

    public struct ditherParam
    {
        public float param1;
        public float param2;
    }

    public enum QTEType
    {
        pressPrompt = 0,
        mash,
        redCircle,
        theEndVariant,
        unknown
    }

    public enum QTEButton
    {
        a = 0,
        b,
        x, 
        y,
        lb_rb,
        lb,
        rb,
        mashA,
        mashB,
        mashX, 
        mashY,
        mashLB,
        mashRB,
        unknown1,
        unknown2,
        unknown3
    }

    public struct QTE
    {
        public QTEType qteType;
        public QTEButton qteButton;
        public float redCircleSize;
        public float redCircleThickness;
        public float whiteLineThickness;
        public float whiteLineSpeed;
        public float multiplier;
        public float redCircleOutlineThickness;
        public float whiteLineOutlineThickness;
        public uint failCount;
        public uint field_88;
        public char[] field_8c;
        public float field_cc;
        public float field_d0;
        public float field_d4;
        public char[] field_d8;
    }

    public struct overrideASM
    {

    }

    public struct aura
    {
        public char[] data;
    }

    public struct changeTimeScale
    {

    }

    public struct cyberSpaceNoise
    {
        public uint field_4f;
        public float[] data;
    }

    public struct auraRoad
    {
        public uint field_00;
        public float[] animData;
    }

    public struct movieView
    {

    }

    public struct weather
    {
        public uint[] field_40;
    }

    public struct variablePointLight
    {
        public float[] unk1;
        public int[] unk2;
        public float[] unk3;
        public int unk4;
        public int[] unk5;
        public float[] data1;
    }

    public struct sun
    {
        public uint field_00;
        public float unkFloat;
        public uint[] field_01;
        public uint[] animData;
    }

    public struct openingLogo
    {

    }

    public struct theEndCableObject
    {
        public byte[] unkData;
    }

    public struct rifleBeastLighting
    {

    }

    public struct nearFarSetting
    {
        public uint field_00;
        public float near;
        public float far;
        public uint[] field_10;
    }

    public struct colorCorrection
    {

    }

    public struct bossCutoff
    {
        public uint field_00;
    }

    #endregion

    #region miscElementStuff
    public enum elementID
    {
        parameterSpecificCamera = 1,
        drawOff = 3,
        pathAdjustment = 5,
        cameraShake = 6,
        cameraShakeLoop = 7,
        effect = 8,
        pathInterpolation = 10,
        culling = 11,
        nearFarSetting = 12,
        UVAnimation = 13,
        visibilityAnimation = 14,
        materialAnimation = 15,
        complexAnimation = 16,
        cameraOffset = 17,
        // modelFade = 18,
        sonicCamera = 20,
        gameCamera = 21,
        // spotLightModel = 26,
        DOF = 1001,
        colorCorrection = 1002,
        cameraExposure = 1003,
        shadowResolution = 1004,
        // heightFog = 1007,
        chromaticAberration = 1008,
        vignetteParam = 1009,
        fade = 1010,
        letterBox = 1011,
        modelClipping = 1012,
        bossCutoff = 1014,
        caption = 1015,
        sound = 1016,
        time = 1017,
        sun = 1018,
        lookAtIK = 1019,
        cameraBlurParam = 1020,
        generalTrigger = 1021,
        ditherParam = 1023,
        QTE = 1024,
        // facialAnimation = 1025,
        overrideASM = 1026,
        aura = 1027,
        changeTimeScale = 1028,
        cyberSpaceNoise = 1029,
        auraRoad = 1031,
        movieView = 1032,
        weather = 1034,
        variablePointLight = 1036,
        openingLogo = 1037,
        theEndCableObject = 1042,
        rifleBeastLighting = 1043
    }

    public enum playType
    {
        playTypeNormal = 0,
        playTypeOneshot = 1,
        playTypeAlways = 2
    }

    public enum updateTiming
    {
        updateTimingOnExecPath = 0,
        updateTimingOnPreUpdate = 1,
        updateTimingCharacterFixPosture = 2,
        updateTimingOnPostUpdateCharacter = 3,
        updateTimingOnUpdatePos = 4,
        updateTimingOnFixBoxPosture = 5,
        updateTimingOnEvaluateDetailMotion = 6,
        updateTimingOnCharacterJobUpdate = 7,
        updateTimingModifyPoseAfter = 8,
        updateTimingJobRegister = 9,
        updateTimingMotionUpdate = 10,
        updateTimingNormal = 2
    }

    #endregion

    public enum elementCategory
    {
        path = 1,
        pathMotion = 2,
        camera = 3,
        cameraMotion = 4,
        character = 5,
        characterMotion = 6,
        characterBehavior = 7,
        modelCustom = 8,
        asset = 9,
        motionModel = 10,
        modelNode = 11,
        element = 12,
        stage = 13,
        stageScenarioFlag = 14,
        instanceMotion = 15,
        instanceMotionData = 16,
        folderCondition = 17,
        characterBehaviorSimpleTalk = 18
    }
    #endregion
}
