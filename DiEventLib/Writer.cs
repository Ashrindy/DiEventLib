using DiEventLib.Nodes;
using DiEventLib.Nodes.Elements;
using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System.Text;

namespace DiEventLib
{
    public class Writer
    {
        public void WriteDvScene(string filename, DiEvent diEvent, bool debugStuff = false)
        {

            if (debugStuff ) 
            {
                Console.InputEncoding = Encoding.Unicode;
                Console.OutputEncoding = Encoding.Unicode;
            }

            void WriteGUID(ExtendedBinaryWriter Writer, Guid guid)
            {
                int index = 0;
                foreach (var i in guid.ToByteArray())
                {
                    Writer.Write(i);
                    index++;
                }
            }

            void WriteDvString(ExtendedBinaryWriter Writer, string String)
            {
                foreach (var i in Encoding.Unicode.GetBytes(String))
                {
                    Writer.Write(i);
                }
            }

            ExtendedBinaryWriter Writer = new(File.Create(filename), encoding: Encoding.Unicode);

            Writer.Write(diEvent.sceneHeader.commonPointer - 32);
            Writer.Write(diEvent.sceneHeader.resourcePointer - 32);

            Writer.AddOffset("commonPointer", diEvent.sceneHeader.commonPointer);

            Writer.Write(diEvent.common.frameStart);
            Writer.Write(diEvent.common.frameEnd);
            Writer.Write(diEvent.common.drawNodeNum);
            Writer.Write(diEvent.common.cutInfoPointer - 32);
            Writer.Write(diEvent.common.authPagePointer - 32);
            Writer.Write(diEvent.common.disableFrameInfoPointer - 32);
            Writer.Write(diEvent.common.resourceCutInfoPointer - 32);
            Writer.Write(diEvent.common.soundCutInfoPointer - 32);
            Writer.Write(diEvent.common.nodeInfoPointer - 32);
            Writer.Write(diEvent.common.chainCameraIn);
            Writer.Write(diEvent.common.chainCameraOut);
            Writer.Write(diEvent.common.type);
            Writer.Write(diEvent.common.skipPointTick);

            Writer.AddOffset("1", 0x04);

            Writer.Write(diEvent.common.cutInfo.cutInfoObject.count);
            Writer.Write(diEvent.common.cutInfo.cutInfoObject.size);

            if(diEvent.common.cutInfo.cutInfoObject.count != 0)
            {
                Writer.AddOffset("2", 0x08);

                foreach (var i in diEvent.common.cutInfo.cutFrame)
                {
                    Writer.Write(i);
                }
            }

            Writer.Write(diEvent.common.authPage.authPageObject.count);
            Writer.Write(diEvent.common.authPage.authPageObject.size);

            Writer.AddOffset("3", 0x08);

            if (diEvent.common.authPage.authPageObject.count != 0)
            {
                foreach (var i in diEvent.common.authPage.authPage)
                {
                    Writer.Write(i);
                }
            }

            Writer.Write(diEvent.common.disableFrame.disableFrameInfoObject.count);
            Writer.Write(diEvent.common.disableFrame.disableFrameInfoObject.size);

            Writer.AddOffset("4", 0x08);

            if (diEvent.common.disableFrame.disableFrameInfoObject.count != 0)
            {
                foreach (var i in diEvent.common.disableFrame.disableFrame)
                {
                    Writer.Write(i);
                }
            }

            Writer.Write(diEvent.common.resourceCut.resourceCutInfoObject.count);
            Writer.Write(diEvent.common.resourceCut.resourceCutInfoObject.size);

            Writer.AddOffset("5", 0x08);

            if (diEvent.common.resourceCut.resourceCutInfoObject.count != 0)
            {
                foreach (var i in diEvent.common.resourceCut.resourceCutFrame)
                {
                    Writer.Write(i);
                }
            }

            Writer.Write(diEvent.common.soundCut.soundInfoObject.count);
            Writer.Write(diEvent.common.soundCut.soundInfoObject.size);

            Writer.AddOffset("6", 0x08);

            if (diEvent.common.soundCut.soundInfoObject.count != 0)
            {
                foreach (var i in diEvent.common.soundCut.soundFrame)
                {
                    Writer.Write(i);
                }
            }

            foreach (var child in diEvent.common.nodes) { saveFirstNodeChild(child); }
            

            void saveFirstNodeChild(node childNode)
            {
                WriteGUID(Writer, childNode.guid);
                Writer.Write((uint)childNode.category);
                Writer.Write(childNode.nodeSize);
                Writer.Write(childNode.childCount);
                Writer.Write(childNode.flags);
                Writer.Write(childNode.priority);
                foreach(var i in childNode.padding)
                {
                    Writer.Write(i);
                }
                WriteDvString(Writer, childNode.name);

                switch (childNode.category)
                {
                    case (nodeCategory)1:
                        DvPath path = new DvPath(childNode, writer: Writer);
                        break;

                    case (nodeCategory)2:
                        break;

                    case (nodeCategory)3:
                        DvCamera camera = new DvCamera(childNode, writer: Writer);
                        break;

                    case (nodeCategory)4:
                        DvCameraMotion cameraMotion = new DvCameraMotion(childNode, writer: Writer);
                        break;

                    case (nodeCategory)5:
                        DvCharacter character = new DvCharacter(childNode, writer: Writer);
                        break;

                    case (nodeCategory)6:
                        DvCharacterMotion characterMotion = new DvCharacterMotion(childNode, writer: Writer);
                        break;

                    case (nodeCategory)7:
                        break;

                    case (nodeCategory)8:
                        DvModelCustom modelCustom = new DvModelCustom(childNode, writer: Writer);
                        break;

                    case (nodeCategory)9:
                        break;

                    case (nodeCategory)10:
                        DvMotionModel motionModel = new DvMotionModel(childNode, writer: Writer);
                        break;

                    case (nodeCategory)11:
                        DvModelNode modelNode = new DvModelNode(childNode, writer: Writer);
                        break;

                    case (nodeCategory)12:
                        DvElement element = new DvElement(childNode, writer: Writer);
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
            }

            if (diEvent.resources.resourceObject.count != 0)
            {
                Writer.Write(diEvent.resources.resourceObject.count);
                Writer.Write(diEvent.resources.resourceObject.size);

                Writer.AddOffset("75", 0x08);

                foreach(var tempResource in diEvent.resources.resources)
                {
                    WriteGUID(Writer, tempResource.guid);
                    Writer.Write((uint)tempResource.resourceType);
                    Writer.Write(tempResource.flags);
                    Writer.Write(tempResource.field_18);
                    WriteDvString(Writer, tempResource.filename);
                    foreach(var i in tempResource.data)
                    {
                        Writer.Write(i);
                    }
                }
            }

            Writer.Close();
        }
    }
}
