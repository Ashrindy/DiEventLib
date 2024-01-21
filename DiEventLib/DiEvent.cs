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
        public nodeCategory category;
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
        public byte[] padding;
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
        public byte[] unk;
    }

    public struct characterMotionInfo
    {
        public uint flag;
        public uint frameStart;
        public uint frameEnd;
        public uint field_0c;
        public byte[] asmStateName;
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
        public byte[] unk;
    }

    public struct motionModelInfo
    {
        public uint field_00;
        public uint frameStart;
        public uint frameEnd;
        public uint field_0c;
        public byte[] asmStateName;
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

    public enum nodeCategory
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
}
