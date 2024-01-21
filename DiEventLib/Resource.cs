using DiEventLib.Nodes;

namespace DiEventLib
{
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
}
