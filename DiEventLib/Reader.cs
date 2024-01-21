using HedgeLib.IO;
using System.Text;

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
                        rootPathInfo childNodeRootPath = new rootPathInfo();
                        childNodeRootPath.matrix.M11 = reader.ReadSingle();
                        childNodeRootPath.matrix.M12 = reader.ReadSingle();
                        childNodeRootPath.matrix.M13 = reader.ReadSingle();
                        childNodeRootPath.matrix.M14 = reader.ReadSingle();
                        childNodeRootPath.matrix.M21 = reader.ReadSingle();
                        childNodeRootPath.matrix.M22 = reader.ReadSingle();
                        childNodeRootPath.matrix.M23 = reader.ReadSingle();
                        childNodeRootPath.matrix.M24 = reader.ReadSingle();
                        childNodeRootPath.matrix.M31 = reader.ReadSingle();
                        childNodeRootPath.matrix.M32 = reader.ReadSingle();
                        childNodeRootPath.matrix.M33 = reader.ReadSingle();
                        childNodeRootPath.matrix.M34 = reader.ReadSingle();
                        childNodeRootPath.matrix.M41 = reader.ReadSingle();
                        childNodeRootPath.matrix.M42 = reader.ReadSingle();
                        childNodeRootPath.matrix.M43 = reader.ReadSingle();
                        childNodeRootPath.matrix.M44 = reader.ReadSingle();
                        childNodeRootPath.flag = reader.ReadUInt32();
                        childNodeRootPath.padding = new byte[12];
                        for (int x = 0; (x < 12); x++)
                        {
                            childNodeRootPath.padding[x] = reader.ReadByte();
                        }

                        childNode.info = childNodeRootPath;
                        break;

                    case (nodeCategory)2:
                        break;

                    case (nodeCategory)3:
                        cameraInfo childNodeCameraInfo = new cameraInfo();
                        childNodeCameraInfo.flag = reader.ReadUInt32();
                        childNodeCameraInfo.frameProgressionCount = reader.ReadUInt32();
                        childNodeCameraInfo.captionCount = reader.ReadUInt32();
                        reader.JumpAhead(0x04);
                        childNodeCameraInfo.frameProgression = new float[childNodeCameraInfo.frameProgressionCount];
                        childNodeCameraInfo.frameProgressionSpeed = new float[childNodeCameraInfo.frameProgressionCount];
                        for(int i = 0; i < childNodeCameraInfo.frameProgressionCount; i++)
                        {
                            childNodeCameraInfo.frameProgression[i] = reader.ReadSingle();
                        }
                        for (int i = 0; i < childNodeCameraInfo.frameProgressionCount; i++)
                        {
                            childNodeCameraInfo.frameProgressionSpeed[i] = reader.ReadSingle();
                        }

                        childNode.info = childNodeCameraInfo;
                        break;

                    case (nodeCategory)4:
                        cameraMotionInfo childNodeCameraMotionInfo = new cameraMotionInfo();
                        childNodeCameraMotionInfo.flag = reader.ReadUInt32();
                        childNodeCameraMotionInfo.frameStart = reader.ReadUInt32();
                        childNodeCameraMotionInfo.frameEnd = reader.ReadUInt32();
                        reader.JumpAhead(0x04);

                        childNode.info = childNodeCameraMotionInfo;
                        break;

                    case (nodeCategory)5:
                        characterInfo childNodeCharacterInfo = new characterInfo();
                        childNodeCharacterInfo.field_00 = reader.ReadUInt32();
                        childNodeCharacterInfo.name1 = ReadDVString(reader);
                        Console.WriteLine(childNodeCharacterInfo.name1);
                        childNodeCharacterInfo.name2 = ReadDVString(reader);
                        childNodeCharacterInfo.name3 = ReadDVString(reader);
                        childNodeCharacterInfo.unk = new byte[76];
                        for (int x = 0; x < 76; x++)
                        {
                            childNodeCharacterInfo.unk[x] = reader.ReadByte();
                        }

                        childNode.info = childNodeCharacterInfo;
                        break;

                    case (nodeCategory)6:
                        characterMotionInfo childNodeCharacterMotionInfo = new characterMotionInfo();
                        childNodeCharacterMotionInfo.flag = reader.ReadUInt32();
                        childNodeCharacterMotionInfo.frameStart = reader.ReadUInt32();
                        childNodeCharacterMotionInfo.frameEnd = reader.ReadUInt32();
                        reader.JumpAhead(0x04);
                        childNodeCharacterMotionInfo.asmStateName = new byte[8];
                        for(int x = 0; x < 8; x++)
                        {
                            childNodeCharacterMotionInfo.asmStateName[x] = reader.ReadByte();
                        }
                        childNodeCharacterMotionInfo.field_50 = reader.ReadUInt32();
                        childNodeCharacterMotionInfo.field_54 = reader.ReadUInt32();
                        childNodeCharacterMotionInfo.field_58 = new uint[4];
                        for(int x = 0; x < 4; x++)
                        {
                            childNodeCharacterMotionInfo.field_58[x] = reader.ReadUInt32();
                        }

                        childNode.info = childNodeCharacterMotionInfo;
                        break;

                    case (nodeCategory)7:
                        break;

                    case (nodeCategory)8:
                        modelCustomInfo childNodeModelCustomInfo = new modelCustomInfo();
                        childNodeModelCustomInfo.field_00 = reader.ReadUInt32();
                        childNodeModelCustomInfo.name1 = ReadDVString(reader);
                        childNodeModelCustomInfo.name2 = ReadDVString(reader);
                        childNodeModelCustomInfo.name3 = ReadDVString(reader);
                        childNodeModelCustomInfo.unk = new byte[76];
                        for (int x = 0; x < 76; x++)
                        {
                            childNodeModelCustomInfo.unk[x] = reader.ReadByte();
                        }

                        childNode.info = childNodeModelCustomInfo;
                        break;

                    case (nodeCategory)9:
                        break;
                        
                    case (nodeCategory)10:
                        reader.JumpAhead(0x04);
                        motionModelInfo childNodemotionModelInfo = new motionModelInfo();
                        childNodemotionModelInfo.frameStart = reader.ReadUInt32();
                        childNodemotionModelInfo.frameEnd = reader.ReadUInt32();
                        reader.JumpAhead(0x04);
                        childNodemotionModelInfo.asmStateName = new byte[8];
                        for (int x = 0; x < 8; x++)
                        {
                            childNodemotionModelInfo.asmStateName[x] = reader.ReadByte();
                        }
                        childNodemotionModelInfo.field_50 = reader.ReadUInt32();
                        childNodemotionModelInfo.field_54 = reader.ReadUInt32();
                        childNodemotionModelInfo.field_58 = new uint[4];
                        for (int x = 0; x < 4; x++)
                        {
                            childNodemotionModelInfo.field_58[x] = reader.ReadUInt32();
                        }

                        childNode.info = childNodemotionModelInfo;
                        break;

                    case (nodeCategory)11:
                        modelNodeInfo childNodeNodeModelInfo = new modelNodeInfo();
                        childNodeNodeModelInfo.field_00 = reader.ReadUInt32();
                        childNodeNodeModelInfo.name1 = ReadDVString(reader);
                        childNodeNodeModelInfo.padding = new byte[12];
                        for (int x = 0; (x < 12); x++)
                        {
                            childNodeNodeModelInfo.padding[x] = reader.ReadByte();
                        }

                        childNode.info = childNodeNodeModelInfo;
                        break;

                    case (nodeCategory)12:
                        elementProperties childNodeElementInfo = new elementProperties();

                        childNodeElementInfo.elementID = (elementID)reader.ReadUInt32();
                        childNodeElementInfo.frameStart = reader.ReadSingle();
                        childNodeElementInfo.frameEnd = reader.ReadSingle();
                        childNodeElementInfo.version = reader.ReadInt32();
                        childNodeElementInfo.flag = reader.ReadUInt32();
                        childNodeElementInfo.playType = (playType)reader.ReadUInt32();
                        childNodeElementInfo.updateTiming = (updateTiming)reader.ReadUInt32();
                        childNodeElementInfo.padding = reader.ReadUInt32();

                        switch (childNodeElementInfo.elementID)
                        {
                            case (elementID)1:
                                reader.JumpAhead(0x468);
                                break;

                            case (elementID)3:
                                drawOff elementDrawOff = new drawOff();
                                elementDrawOff.field_00 = new uint[4];
                                for (int i = 0; i < 4; i++)
                                {
                                    elementDrawOff.field_00[i] = reader.ReadUInt32();
                                }

                                childNodeElementInfo.info = elementDrawOff;
                                break;

                            case (elementID)5:
                                pathAdjustment elementPathAdjustment = new pathAdjustment();
                                elementPathAdjustment.matrix.M11 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M12 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M13 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M14 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M21 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M22 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M23 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M24 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M31 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M32 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M33 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M34 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M41 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M42 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M43 = reader.ReadSingle();
                                elementPathAdjustment.matrix.M44 = reader.ReadSingle();
                                elementPathAdjustment.field_40 = new uint[4];
                                for (int i = 0; i < 4; i++)
                                {
                                    elementPathAdjustment.field_40[i] = reader.ReadUInt32();
                                }

                                childNodeElementInfo.info = elementPathAdjustment;
                                break;

                            case (elementID)6:
                                reader.JumpAhead(32);
                                break;

                            case (elementID)7:
                                cameraShakeLoop elementCamShakeLoop = new cameraShakeLoop();
                                elementCamShakeLoop.field_60 = reader.ReadUInt32();
                                elementCamShakeLoop.field_64 = reader.ReadUInt32();
                                elementCamShakeLoop.field_68 = new float[6];
                                for (int i = 0; i < 6; i++)
                                {
                                    elementCamShakeLoop.field_68[i] = reader.ReadSingle();
                                }
                                elementCamShakeLoop.field_curveData = new float[64];
                                for (int i = 0; i < 64; i++)
                                {
                                    elementCamShakeLoop.field_curveData[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementCamShakeLoop;
                                break;

                            case (elementID)8:
                                effect elementEffect = new effect();
                                elementEffect.matrix.M11 = reader.ReadSingle();
                                elementEffect.matrix.M12 = reader.ReadSingle();
                                elementEffect.matrix.M13 = reader.ReadSingle();
                                elementEffect.matrix.M14 = reader.ReadSingle();
                                elementEffect.matrix.M21 = reader.ReadSingle();
                                elementEffect.matrix.M22 = reader.ReadSingle();
                                elementEffect.matrix.M23 = reader.ReadSingle();
                                elementEffect.matrix.M24 = reader.ReadSingle();
                                elementEffect.matrix.M31 = reader.ReadSingle();
                                elementEffect.matrix.M32 = reader.ReadSingle();
                                elementEffect.matrix.M33 = reader.ReadSingle();
                                elementEffect.matrix.M34 = reader.ReadSingle();
                                elementEffect.matrix.M41 = reader.ReadSingle();
                                elementEffect.matrix.M42 = reader.ReadSingle();
                                elementEffect.matrix.M43 = reader.ReadSingle();
                                elementEffect.matrix.M44 = reader.ReadSingle();
                                elementEffect.field_9c = reader.ReadUInt32();
                                elementEffect.filename = ReadDVString(reader);
                                elementEffect.field_dc = new uint[8];
                                for (int i = 0; i < 8; i++)
                                {
                                    elementEffect.field_dc[i] = reader.ReadUInt32();
                                }
                                elementEffect.animData = new float[128];
                                for (int i = 0; i < 128; i++)
                                {
                                    elementEffect.animData[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementEffect;
                                break;

                            case (elementID)10:
                                pathInterpolation elementPathInterpolation = new pathInterpolation();
                                elementPathInterpolation.data = new byte[592];
                                for (int i = 0; i < 592; i++)
                                {
                                    elementPathInterpolation.data[i] = reader.ReadByte();
                                }

                                childNodeElementInfo.info = elementPathInterpolation;
                                break;

                            case (elementID)11:
                                childNodeElementInfo.info = new culling();
                                break;

                            case (elementID)12:
                                nearFarSetting elementNearFarSetting = new nearFarSetting();
                                elementNearFarSetting.field_00 = reader.ReadUInt32();
                                elementNearFarSetting.near = reader.ReadSingle();
                                elementNearFarSetting.far = reader.ReadSingle();
                                elementNearFarSetting.field_10 = new uint[5];
                                for(int i = 0; i < 5; i++)
                                {
                                    elementNearFarSetting.field_10[i] = reader.ReadUInt32();
                                }

                                childNodeElementInfo.info = elementNearFarSetting;
                                break;

                            case (elementID)13:
                                uvAnim elementUVAnim = new uvAnim();
                                elementUVAnim.field_00 = reader.ReadUInt32();
                                elementUVAnim.filename = ReadDVString(reader);
                                elementUVAnim.Field44 = reader.ReadUInt32();
                                elementUVAnim.Field48 = reader.ReadSingle();
                                elementUVAnim.Field4C = reader.ReadUInt32();
                                elementUVAnim.Field50 = reader.ReadUInt32();

                                childNodeElementInfo.info = elementUVAnim;
                                break;

                            case (elementID)14:
                                visAnim elementVisAnim = new visAnim();
                                elementVisAnim.field_40 = reader.ReadUInt32();
                                elementVisAnim.filename = ReadDVString(reader);
                                elementVisAnim.data1 = new byte[16];
                                for (int i = 0; i < 16; i++)
                                {
                                    elementVisAnim.data1[i] = reader.ReadByte();
                                }

                                childNodeElementInfo.info = elementVisAnim;
                                break;

                            case (elementID)15:
                                matAnim elementMatAnim = new matAnim();
                                elementMatAnim.field_00 = reader.ReadUInt32();
                                elementMatAnim.filename = ReadDVString(reader);
                                elementMatAnim.Field44 = reader.ReadUInt32();
                                elementMatAnim.Field48 = reader.ReadSingle();
                                elementMatAnim.Field4C = reader.ReadUInt32();
                                elementMatAnim.Field50 = reader.ReadUInt32();

                                childNodeElementInfo.info = elementMatAnim;
                                break;

                            case (elementID)16:
                                compAnim elementCompAnim = new compAnim();
                                elementCompAnim.field_60 = reader.ReadUInt32();
                                elementCompAnim.data = new byte[8];
                                for(int i = 0; i < 8; i++)
                                {
                                    elementCompAnim.data[i] = reader.ReadByte();
                                }
                                elementCompAnim.field_6c = reader.ReadUInt32();
                                elementCompAnim.animations = new anim[16];
                                for(int i = 0;i < 16; i++)
                                {
                                    elementCompAnim.animations[i] = new anim();
                                    elementCompAnim.animations[i].animType = (animType)reader.ReadUInt32();
                                    elementCompAnim.animations[i].filename = ReadDVString(reader);
                                }
                                elementCompAnim.field_03 = reader.ReadUInt32();

                                childNodeElementInfo.info = elementCompAnim;
                                break;

                            case (elementID)17:
                                cameraOffset elementCameraOffset = new cameraOffset();
                                elementCameraOffset.data = new float[12];
                                for (int i = 0; i < 12; i++)
                                {
                                    elementCameraOffset.data[i] = reader.ReadSingle();
                                }

                                elementCameraOffset.animData = new float[256];
                                for (int i = 0; i < 256; i++)
                                {
                                    elementCameraOffset.animData[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementCameraOffset;
                                break;

                            case (elementID)20:
                                sonicCam elementSonicCam = new sonicCam();
                                elementSonicCam.field_4c = new float[80];
                                for (int i = 0; i < 80; i++)
                                {
                                    elementSonicCam.field_4c[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementSonicCam;
                                break;

                            case (elementID)21:
                                gameCam elementGameCam = new gameCam();
                                elementGameCam.field_4c = new float[26];
                                for (int i = 0; i < 26; i++)
                                {
                                    elementGameCam.field_4c[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementGameCam;
                                break;

                            case (elementID)1001:
                                dof elementDOF = new dof();
                                elementDOF.field_60 = reader.ReadUInt32();
                                elementDOF.field_64 = reader.ReadSingle();
                                elementDOF.field_68 = reader.ReadSingle();
                                elementDOF.field_6c = reader.ReadSingle();
                                elementDOF.far1 = reader.ReadSingle();
                                elementDOF.field_74 = reader.ReadSingle();
                                elementDOF.field_78 = reader.ReadSingle();
                                elementDOF.field_7c = reader.ReadSingle();
                                elementDOF.far2 = reader.ReadSingle();
                                elementDOF.field_84 = reader.ReadSingle();
                                elementDOF.field_88 = reader.ReadSingle();
                                elementDOF.field_8c = reader.ReadUInt32();
                                elementDOF.field_90 = reader.ReadUInt32();
                                elementDOF.field_94 = reader.ReadSingle();
                                elementDOF.field_98 = reader.ReadSingle();
                                elementDOF.field_9c = reader.ReadSingle();
                                elementDOF.field_a0 = reader.ReadSingle();
                                elementDOF.field_a4 = reader.ReadSingle();
                                elementDOF.field_a8 = reader.ReadSingle();
                                elementDOF.field_ac = reader.ReadSingle();
                                elementDOF.animData = new float[32];
                                for (int i = 0; i < 32; i++)
                                {
                                    elementDOF.animData[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementDOF;
                                break;

                            case (elementID)1002:
                                reader.JumpAhead(0xA0);
                                break;

                            case (elementID)1003:
                                camExposure elementCamExposure = new camExposure();
                                elementCamExposure.unk1 = reader.ReadInt32();
                                elementCamExposure.field_48 = new float[7];
                                for (int i = 0; i < 7; i++)
                                {
                                    elementCamExposure.field_48[i] = reader.ReadSingle();
                                }
                                elementCamExposure.field_80 = new float[32];
                                for (int i = 0; i < 32; i++)
                                {
                                    elementCamExposure.field_80[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementCamExposure;
                                break;

                            case (elementID)1004:
                                shadowRes elementShadowRes = new shadowRes();
                                elementShadowRes.shadowRes1 = reader.ReadUInt32();
                                elementShadowRes.shadowRes2 = reader.ReadUInt32();

                                childNodeElementInfo.info = elementShadowRes;
                                break;

                            case (elementID)1007:
                                fog elementFog = new fog();
                                elementFog.data = new byte[300];
                                for(int i =0; i <300; i++)
                                {
                                    elementFog.data[i] = reader.ReadByte();
                                }

                                childNodeElementInfo.info = elementFog;
                                break;

                            case (elementID)1008:
                                chromaticAberration elementChromatic = new chromaticAberration();
                                elementChromatic.data = new float[17];
                                for (int i = 0; i < 17; i++)
                                {
                                    elementChromatic.data[i] = reader.ReadSingle();
                                }
                                elementChromatic.data1 = new float[32];
                                for (int i = 0; i < 32; i++)
                                {
                                    elementChromatic.data1[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementChromatic;
                                break;

                            case (elementID)1009:
                                vignette elementVignette = new vignette();
                                elementVignette.data1 = new float[9];
                                for (int i = 0; i < 9; i++)
                                {
                                    elementVignette.data1[i] = reader.ReadSingle();
                                }
                                elementVignette.data2 = reader.ReadInt32();
                                elementVignette.data3 = new float[24];
                                for (int i = 0; i < 24; i++)
                                {
                                    elementVignette.data3[i] = reader.ReadSingle();
                                }
                                elementVignette.data4 = reader.ReadInt32();
                                elementVignette.data5 = new float[15];
                                for (int i = 0; i < 15; i++)
                                {
                                    elementVignette.data5[i] = reader.ReadSingle();
                                }
                                elementVignette.data6 = new float[32];
                                for (int i = 0; i < 32; i++)
                                {
                                    elementVignette.data6[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementVignette;
                                break;

                            case (elementID)1010:
                                fade elementFade = new fade();
                                elementFade.color.A = reader.ReadUInt32();
                                elementFade.color.B = reader.ReadUInt32();
                                elementFade.color.G = reader.ReadUInt32();
                                elementFade.color.R = reader.ReadUInt32();
                                elementFade.curveData = new float[32];
                                for (int i = 0; i < 32; i++)
                                {
                                    elementFade.curveData[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementFade;
                                break;

                            case (elementID)1011:
                                letterBox elementLetterBox = new letterBox();
                                elementLetterBox.curveData = new float[32];
                                for (int i = 0; i < 32; i++)
                                {
                                    elementLetterBox.curveData[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementLetterBox;
                                break;

                            case (elementID)1012:
                                modelClipping elementModelClipping = new modelClipping();
                                elementModelClipping.data = new byte[20];
                                for (int i = 0; i < 20; i++)
                                {
                                    elementModelClipping.data[i] = reader.ReadByte();
                                }

                                childNodeElementInfo.info = elementModelClipping;
                                break;

                            case (elementID)1014:
                                bossName elementBossName = new bossName();
                                elementBossName.field_00 = reader.ReadUInt32();
                                elementBossName.bossID = (bossID)reader.ReadUInt32();

                                childNodeElementInfo.info = elementBossName;
                                break;

                            case (elementID)1015:
                                caption elementCaption = new caption();
                                elementCaption.captionName = new byte[16];
                                for (int i = 0; i < 16; i++)
                                {
                                    elementCaption.captionName[i] = reader.ReadByte();
                                }
                                elementCaption.languageType = (languageType)reader.ReadUInt32();
                                elementCaption.padding = reader.ReadUInt32();

                                childNodeElementInfo.info = elementCaption;
                                break;

                            case (elementID)1016:
                                sound elementSound = new sound();
                                elementSound.cueName = ReadDVString(reader);
                                elementSound.field_a0 = reader.ReadUInt32();
                                elementSound.field_a4 = reader.ReadUInt32();
                                break;

                            case (elementID)1017:
                                time elementTime = new time();
                                elementTime.data = new byte[164];
                                for (int i = 0; i < 164; i++)
                                {
                                    elementTime.data[i] = reader.ReadByte();
                                }

                                childNodeElementInfo.info = elementTime;
                                break;

                            case (elementID)1018:
                                sun elementSun = new sun();
                                elementSun.field_00 = reader.ReadUInt32();
                                elementSun.unkFloat = reader.ReadSingle();
                                elementSun.field_01 = new uint[5];
                                for (int i = 0; i < 5; i++)
                                {
                                    elementSun.field_01[i] = reader.ReadUInt32();
                                }
                                elementSun.animData = new uint[32];
                                for (int i = 0; i < 32; i++)
                                {
                                    elementSun.animData[i] = reader.ReadUInt32();
                                }

                                childNodeElementInfo.info = elementSun;
                                break;

                            case (elementID)1019:
                                lookAtIK elementLookAtIK = new lookAtIK();
                                elementLookAtIK.field_60 = reader.ReadUInt32();
                                elementLookAtIK.field_64 = reader.ReadUInt32();
                                elementLookAtIK.guid = ReadGUID(reader);
                                elementLookAtIK.field_78 = new uint[11];
                                for (int i = 0; i < 11; i++)
                                {
                                    elementLookAtIK.field_78[i] = reader.ReadUInt32();
                                }
                                elementLookAtIK.field_80 = new float[64];
                                for (int i = 0; i < 64; i++)
                                {
                                    elementLookAtIK.field_80[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementLookAtIK;
                                break;

                            case (elementID)1020:
                                camBlur elementCamBlur = new camBlur();
                                elementCamBlur.flag = reader.ReadUInt32();
                                elementCamBlur.blurAmount = reader.ReadUInt32();
                                elementCamBlur.curveData = new float[34];
                                for(int i  = 0; i < 34; i++)
                                {
                                    elementCamBlur.curveData[i] = reader.ReadSingle();
                                }

                                childNodeElementInfo.info = elementCamBlur;
                                break;

                            case (elementID)1021:
                                generalTrigger elementGeneralTrigger = new generalTrigger();
                                elementGeneralTrigger.field_00 = reader.ReadUInt32();
                                elementGeneralTrigger.triggerName = ReadDVString(reader);

                                childNodeElementInfo.info = elementGeneralTrigger;
                                break;

                            case (elementID)1023:
                                ditherParam elementDither = new ditherParam();
                                elementDither.param1 = reader.ReadSingle();
                                elementDither.param2 = reader.ReadSingle();

                                childNodeElementInfo.info = elementDither;
                                break;

                            case (elementID)1024:
                                QTE elementQTE = new QTE();
                                elementQTE.qteType = (QTEType)reader.ReadUInt32();
                                elementQTE.qteButton = (QTEButton)reader.ReadUInt32();
                                elementQTE.redCircleSize = reader.ReadSingle();
                                elementQTE.redCircleThickness = reader.ReadSingle();
                                elementQTE.whiteLineThickness = reader.ReadSingle();
                                elementQTE.whiteLineSpeed = reader.ReadSingle();
                                elementQTE.multiplier = reader.ReadSingle();
                                elementQTE.redCircleOutlineThickness = reader.ReadSingle();
                                elementQTE.whiteLineOutlineThickness = reader.ReadSingle();
                                elementQTE.failCount = reader.ReadUInt32();
                                elementQTE.field_88 = reader.ReadUInt32();
                                elementQTE.field_8c = new byte[64];
                                for(int i = 0; i < 64; i++)
                                {
                                    elementQTE.field_8c[i] = reader.ReadByte();
                                }
                                elementQTE.field_cc = reader.ReadSingle();
                                elementQTE.field_d0 = reader.ReadSingle();
                                elementQTE.field_d4 = reader.ReadSingle();
                                elementQTE.field_d8 = new byte[264];
                                for(int i = 0; i < 264; i++)
                                {
                                    elementQTE.field_d8[i] = reader.ReadByte();
                                }

                                childNodeElementInfo.info = elementQTE;
                                break;

                            case (elementID)1026:
                                childNodeElementInfo.info = new overrideASM();
                                break;

                            case (elementID)1027:
                                aura elementAura = new aura();
                                elementAura.data = new byte[204];
                                for(int i = 0; i < 204; i++)
                                {
                                    elementAura.data[i] = reader.ReadByte();
                                }

                                childNodeElementInfo.info = elementAura;
                                break;

                            case (elementID)1028:
                                childNodeElementInfo.info = new changeTimeScale();
                                break;

                            case (elementID)1029:
                                cyberSpaceNoise elementCyberSpaceNoise = new cyberSpaceNoise();
                                elementCyberSpaceNoise.field_4f = reader.ReadUInt32();
                                elementCyberSpaceNoise.data = new float[32];
                                for(int i = 0; i < 32; i++)
                                {
                                    elementCyberSpaceNoise.data[i] = reader.ReadUInt32();
                                }

                                childNodeElementInfo.info = elementCyberSpaceNoise;
                                break;

                            case (elementID)1031:
                                auraRoad elementAuraRoad = new auraRoad();
                                elementAuraRoad.field_00 = reader.ReadUInt32();
                                elementAuraRoad.animData = new float[64];
                                for(int i = 0; i < 64; i++)
                                {
                                    elementAuraRoad.animData[i] = reader.ReadUInt32();
                                }

                                childNodeElementInfo.info = elementAuraRoad;
                                break;

                            case (elementID)1032:
                                childNodeElementInfo.info = new movieView();
                                break;

                            case (elementID)1034:
                                weather elementWeather = new weather();
                                elementWeather.field_40 = new uint[33];
                                for(int i = 0; i < 33; i++)
                                {
                                    elementWeather.field_40[i] = reader.ReadUInt32();
                                }

                                childNodeElementInfo.info = elementWeather;
                                break;

                            case (elementID)1036:
                                variablePointLight elementPointLight = new variablePointLight();
                                elementPointLight.unk1 = new float[7];
                                for(int i = 0; i < 7; i++)
                                {
                                    elementPointLight.unk1[i] = reader.ReadUInt32();
                                }
                                elementPointLight.unk2 = new int[6];
                                for(int i = 0; i < 6; i++)
                                {
                                    elementPointLight.unk2[i] = reader.ReadInt32();
                                }
                                elementPointLight.unk3 = new float[8];
                                for(int i = 0;i < 8; i++)
                                {
                                    elementPointLight.unk3[i] = reader.ReadUInt32();
                                }
                                elementPointLight.unk4 = reader.ReadInt32();
                                elementPointLight.unk5 = new int[10];
                                for(int i = 0; i < 10; i++)
                                {
                                    elementPointLight.unk5[i] = reader.ReadInt32();
                                }
                                elementPointLight.data1 = new float[128];
                                for(int i = 0;i < 128; i++)
                                {
                                    elementPointLight.data1[i] = reader.ReadSingle();
                                }
                                childNodeElementInfo.info = elementPointLight;
                                break;

                            case (elementID)1037:
                                childNodeElementInfo.info = new openingLogo();
                                break;

                            case (elementID)1042:
                                childNodeElementInfo.info = new theEndCableObject();
                                reader.JumpAhead(0x1008);
                                break;

                            case (elementID)1043:
                                childNodeElementInfo.info = new rifleBeastLighting();
                                break;
                        }

                        childNode.info = childNodeElementInfo;
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
