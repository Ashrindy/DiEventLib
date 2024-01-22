using DiEventLib.Nodes.NodeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes
{
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

        public DvNodeObject GetInfoByType<DvNodeObject>()
        {
            return (DvNodeObject)info;
        }
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
