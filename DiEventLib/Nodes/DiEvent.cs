using DiEventLib.Nodes.NodeTypes;

namespace DiEventLib.Nodes
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

        public node GetNodeByGUID(Guid guid) 
        {
            node returnValue = null;
            if (common.nodes.Find(i => i.guid == guid) == null)
            {
                throw new Exception();
            } else
            {
                returnValue = common.nodes.Find(i => i.guid == guid);
            }

            return returnValue;
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
}
