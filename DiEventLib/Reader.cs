using HedgeLib.IO;
using System.Text;
using DiEventLib.Nodes.Elements;
using DiEventLib.Nodes;
using DiEventLib.Nodes.NodeTypes;

namespace DiEventLib
{
    public class Reader
    {
        public string filename;

        public DiEvent ReadDvScene(string filename, bool debugStuff = false)
        {
            this.filename = filename;

            string ReadDVString(ExtendedBinaryReader reader)
            {
                byte[] nameBytes = new byte[64];

                for (int x = 0; x < 64; x++)
                {
                    nameBytes[x] = reader.ReadByte();
                }

                return Encoding.Unicode.GetString(nameBytes);
            }

            Guid ReadGUID(ExtendedBinaryReader reader)
            {
                byte[] guidBytes = new byte[16];

                for (int x = 0; x < 16; x++)
                {
                    guidBytes[x] = reader.ReadByte();
                }
                return new Guid(guidBytes);
            }

            ExtendedBinaryReader reader = new(File.OpenRead(filename));

            uint commonPointer = reader.ReadUInt32() + 32;
            uint resourcePointer = reader.ReadUInt32() + 32;

            DvSceneHeader sceneHeader = new DvSceneHeader();
            Common common = new Common();
            Resources resources = new Resources();

            DiEvent diEvent = new DiEvent(sceneHeader, common, resources);

            sceneHeader.commonPointer = commonPointer;
            sceneHeader.resourcePointer = resourcePointer;

            reader.JumpTo(commonPointer);
            reader.JumpAhead(0x08);

            common.frameStart = reader.ReadSingle();
            common.frameEnd = reader.ReadSingle();
            common.drawNodeNum = reader.ReadUInt32();
            common.cutInfoPointer = reader.ReadUInt32() + 32;
            common.authPagePointer = reader.ReadUInt32() + 32;
            common.disableFrameInfoPointer = reader.ReadUInt32() + 32;
            common.resourceCutInfoPointer = reader.ReadUInt32() + 32;
            common.soundCutInfoPointer = reader.ReadUInt32() + 32;
            common.nodeInfoPointer = reader.ReadUInt32() + 32;
            common.chainCameraIn = reader.ReadSingle();
            common.chainCameraOut = reader.ReadSingle();
            common.type = reader.ReadInt32();
            common.skipPointTick = reader.ReadInt32();

            reader.JumpTo(common.cutInfoPointer);

            common.cutInfo.cutInfoObject.count = reader.ReadUInt32();
            common.cutInfo.cutInfoObject.size = reader.ReadUInt32();

            common.cutInfo.cutFrame = new List<float>();

            if(common.cutInfo.cutInfoObject.count != 0)
            {
                reader.JumpAhead(0x08);

                for (int i = 0; i < common.cutInfo.cutInfoObject.count; i++)
                {
                    common.cutInfo.cutFrame.Add(reader.ReadSingle());
                }
            }

            reader.JumpTo(common.authPagePointer);

            common.authPage.authPageObject.count = reader.ReadUInt32();
            common.authPage.authPageObject.size = reader.ReadUInt32();

            common.authPage.authPage = new List<float>();

            if (common.authPage.authPageObject.count != 0)
            {
                reader.JumpAhead(0x08);

                for (int i = 0; i < common.authPage.authPageObject.count; i++)
                {
                    common.authPage.authPage.Add(reader.ReadSingle());
                }
            }

            reader.JumpTo(common.disableFrameInfoPointer);

            common.disableFrame.disableFrameInfoObject.count = reader.ReadUInt32();
            common.disableFrame.disableFrameInfoObject.size = reader.ReadUInt32();

            common.disableFrame.disableFrame = new List<float>();

            if (common.disableFrame.disableFrameInfoObject.count != 0)
            {
                reader.JumpAhead(0x08);

                for (int i = 0; i < common.disableFrame.disableFrameInfoObject.count; i++)
                {
                    common.disableFrame.disableFrame.Add(reader.ReadSingle());
                }
            }

            reader.JumpTo(common.resourceCutInfoPointer);

            common.resourceCut.resourceCutInfoObject.count = reader.ReadUInt32();
            common.resourceCut.resourceCutInfoObject.size = reader.ReadUInt32();

            common.resourceCut.resourceCutFrame = new List<float>();

            if (common.resourceCut.resourceCutInfoObject.count != 0)
            {
                reader.JumpAhead(0x08);

                for (int i = 0; i < common.resourceCut.resourceCutInfoObject.count; i++)
                {
                    common.resourceCut.resourceCutFrame.Add(reader.ReadSingle());
                }
            }

            reader.JumpTo(common.soundCutInfoPointer);

            common.soundCut.soundInfoObject.count = reader.ReadUInt32();
            common.soundCut.soundInfoObject.size = reader.ReadUInt32();

            common.soundCut.soundFrame = new List<float>();

            if (common.soundCut.soundInfoObject.count != 0)
            {
                reader.JumpAhead(0x08);

                for (int i = 0; i < common.soundCut.soundInfoObject.count; i++)
                {
                    common.soundCut.soundFrame.Add(reader.ReadSingle());
                }
            }

            reader.JumpTo(common.nodeInfoPointer);

            common.nodes = new List<node>();

            loadFirstNodeChild();

            void loadFirstNodeChild(node parentNode = null)
            {
                node childNode = new node();

                childNode.guid = ReadGUID(reader);
                childNode.category = (nodeCategory)reader.ReadUInt32();
                childNode.nodeSize = reader.ReadInt32();
                childNode.childCount = reader.ReadInt32();
                childNode.flags = reader.ReadUInt32();
                childNode.priority = reader.ReadInt32();
                childNode.padding = new byte[12];
                for (int x = 0; (x < 12); x++)
                {
                    childNode.padding[x] = reader.ReadByte();
                }
                childNode.name = ReadDVString(reader);

                switch (childNode.category)
                {
                    case (nodeCategory)1:
                        childNode.info = new DvPath(reader: reader);
                        break;

                    case (nodeCategory)2:
                        break;

                    case (nodeCategory)3:
                        childNode.info = new DvCamera(reader: reader);
                        break;

                    case (nodeCategory)4:
                        childNode.info = new DvCameraMotion(reader: reader);
                        break;

                    case (nodeCategory)5:
                        childNode.info = new DvCharacter(reader: reader);
                        break;

                    case (nodeCategory)6:
                        childNode.info = new DvCharacterMotion(reader: reader);
                        break;

                    case (nodeCategory)7:
                        break;

                    case (nodeCategory)8:
                        childNode.info = new DvModelCustom(reader: reader);
                        break;

                    case (nodeCategory)9:
                        break;
                        
                    case (nodeCategory)10:
                        childNode.info = new DvMotionModel(reader: reader);
                        break;

                    case (nodeCategory)11:
                        childNode.info = new DvModelNode(reader: reader);
                        break;

                    case (nodeCategory)12:
                        childNode.info = new DvElement(reader: reader);
                        break;

                    case (nodeCategory)13:
                        break;

                    case (nodeCategory)14:
                        break;

                    case (nodeCategory)15:
                        break;

                    case (nodeCategory)16:
                        break;

                    case (nodeCategory)17:
                        break;

                    case (nodeCategory)18:
                        break;
                }

                if(debugStuff) { Console.WriteLine(childNode.category + childNode.name + childNode.childCount); }

                if (parentNode != null) { parentNode.children.Add(childNode); }
                common.nodes.Add(childNode);

                if (childNode.childCount > 0)
                {
                    childNode.children = new List<node>();
                    for(int i = 0; i < childNode.childCount; i++) { loadFirstNodeChild(childNode); }
                }
            }

            reader.JumpTo(sceneHeader.resourcePointer);

            resources.resourceObject.count = reader.ReadUInt32();
            resources.resourceObject.size = reader.ReadUInt32();

            resources.resources = new List<resource>();

            if (resources.resourceObject.count != 0)
            {
                reader.JumpAhead(0x08);

                for(int i = 0;i < resources.resourceObject.count; i++)
                {
                    resource tempResource = new resource();

                    tempResource.guid = ReadGUID(reader);
                    tempResource.resourceType = (resourceKind)reader.ReadUInt32();
                    tempResource.flags = reader.ReadUInt32();
                    tempResource.field_18 = reader.ReadUInt32();

                    byte[] nameBytes = new byte[192];

                    for (int x = 0; x < 192; x++)
                    {
                        nameBytes[x] = reader.ReadByte();
                    }

                    tempResource.filename = Encoding.Unicode.GetString(nameBytes);

                    tempResource.data = new List<byte>();

                    for(int y = 0; y < 596; y++)
                    {
                        tempResource.data.Add(reader.ReadByte());
                    }

                    resources.resources.Add(tempResource);
                }
            }

            reader.Close();
            return diEvent;
        }
    }
}
