namespace DiEventLib.Nodes.NodeTypes
{
    public struct cameraInfo
    {
        public uint flag;
        public uint frameProgressionCount;
        public uint captionCount;
        public byte[] padding;
        public float[] frameProgression;
        public float[] frameProgressionSpeed;
    }
}
