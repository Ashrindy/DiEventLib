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
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M11);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M12);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M13);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M14);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M21);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M22);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M23);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M24);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M31);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M32);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M33);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M34);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M41);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M42);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M43);
                        Writer.Write(((rootPathInfo)childNode.info).matrix.M44);
                        Writer.Write(((rootPathInfo)childNode.info).flag);

                        foreach(var i in ((rootPathInfo)childNode.info).padding)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (nodeCategory)2:
                        break;

                    case (nodeCategory)3:
                        Writer.Write(((cameraInfo)childNode.info).flag);
                        Writer.Write(((cameraInfo)childNode.info).frameProgressionCount);
                        Writer.Write(((cameraInfo)childNode.info).captionCount);
                        Writer.AddOffset("02", 0x04);
                        foreach(var i in ((cameraInfo)childNode.info).frameProgression)
                        {
                            Writer.Write(i);
                        }
                        foreach (var i in ((cameraInfo)childNode.info).frameProgressionSpeed)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (nodeCategory)4:
                        Writer.Write(((cameraMotionInfo)childNode.info).flag);
                        Writer.Write(((cameraMotionInfo)childNode.info).frameStart);
                        Writer.Write(((cameraMotionInfo)childNode.info).frameEnd);
                        Writer.AddOffset("03", 0x04);
                        break;

                    case (nodeCategory)5:
                        Writer.Write(((characterInfo)childNode.info).field_00);
                        WriteDvString(Writer, ((characterInfo)childNode.info).name1);
                        WriteDvString(Writer, ((characterInfo)childNode.info).name2);
                        WriteDvString(Writer, ((characterInfo)childNode.info).name3);
                        foreach(var i in ((characterInfo)childNode.info).unk)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (nodeCategory)6:
                        Writer.Write(((characterMotionInfo)childNode.info).flag);
                        Writer.Write(((characterMotionInfo)childNode.info).frameStart);
                        Writer.Write(((characterMotionInfo)childNode.info).frameEnd);
                        Writer.AddOffset("04", 0x04);
                        foreach(var i in ((characterMotionInfo)childNode.info).asmStateName)
                        {
                            Writer.Write(i);
                        }
                        Writer.Write(((characterMotionInfo)childNode.info).field_50);
                        Writer.Write(((characterMotionInfo)childNode.info).field_54);
                        foreach (var i in ((characterMotionInfo)childNode.info).field_58)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (nodeCategory)7:
                        break;

                    case (nodeCategory)8:
                        Writer.Write(((modelCustomInfo)childNode.info).field_00);
                        WriteDvString(Writer, ((modelCustomInfo)childNode.info).name1);
                        WriteDvString(Writer, ((modelCustomInfo)childNode.info).name2);
                        WriteDvString(Writer, ((modelCustomInfo)childNode.info).name3);
                        foreach (var i in ((modelCustomInfo)childNode.info).unk)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (nodeCategory)9:
                        break;

                    case (nodeCategory)10:
                        Writer.AddOffset("05", 0x04);
                        Writer.Write(((motionModelInfo)childNode.info).frameStart);
                        Writer.Write(((motionModelInfo)childNode.info).frameEnd);
                        Writer.AddOffset("06", 0x04);
                        foreach (var i in ((motionModelInfo)childNode.info).asmStateName)
                        {
                            Writer.Write(i);
                        }
                        Writer.Write(((motionModelInfo)childNode.info).field_50);
                        Writer.Write(((motionModelInfo)childNode.info).field_54);
                        foreach (var i in ((motionModelInfo)childNode.info).field_58)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (nodeCategory)11:
                        Writer.Write(((modelNodeInfo)childNode.info).field_00);
                        WriteDvString(Writer, ((modelNodeInfo)childNode.info).name1);
                        foreach(var i in ((modelNodeInfo)childNode.info).padding)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (nodeCategory)12:
                        Writer.Write((uint)((elementProperties)childNode.info).elementID);
                        Writer.Write(((elementProperties)childNode.info).frameStart);
                        Writer.Write(((elementProperties)childNode.info).frameEnd);
                        Writer.Write(((elementProperties)childNode.info).version);
                        Writer.Write(((elementProperties)childNode.info).flag);
                        Writer.Write((uint)((elementProperties)childNode.info).playType);
                        Writer.Write((uint)((elementProperties)childNode.info).updateTiming);
                        Writer.Write(((elementProperties)childNode.info).padding);

                        elementProperties prop = (elementProperties)childNode.info;

                        switch (prop.elementID)
                        {
                            case (elementID)1:
                                Writer.AddOffset("07", 0x468);
                                break;

                            case (elementID)3:
                                foreach(var i in ((drawOff)prop.info).field_00)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)5:
                                Writer.Write(((pathAdjustment)prop.info).matrix.M11);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M12);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M13);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M14);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M21);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M22);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M23);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M24);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M31);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M32);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M33);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M34);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M41);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M42);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M43);
                                Writer.Write(((pathAdjustment)prop.info).matrix.M44);
                                foreach(var i in ((pathAdjustment)prop.info).field_40)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)6:
                                Writer.AddOffset("08", 32);
                                break;

                            case (elementID)7:
                                Writer.Write(((cameraShakeLoop)prop.info).field_60);
                                Writer.Write(((cameraShakeLoop)prop.info).field_64);
                                foreach(var i in ((cameraShakeLoop)prop.info).field_68)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in ((cameraShakeLoop)prop.info).field_curveData)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)8:
                                Writer.Write(((effect)prop.info).matrix.M11);
                                Writer.Write(((effect)prop.info).matrix.M12);
                                Writer.Write(((effect)prop.info).matrix.M13);
                                Writer.Write(((effect)prop.info).matrix.M14);
                                Writer.Write(((effect)prop.info).matrix.M21);
                                Writer.Write(((effect)prop.info).matrix.M22);
                                Writer.Write(((effect)prop.info).matrix.M23);
                                Writer.Write(((effect)prop.info).matrix.M24);
                                Writer.Write(((effect)prop.info).matrix.M31);
                                Writer.Write(((effect)prop.info).matrix.M32);
                                Writer.Write(((effect)prop.info).matrix.M33);
                                Writer.Write(((effect)prop.info).matrix.M34);
                                Writer.Write(((effect)prop.info).matrix.M41);
                                Writer.Write(((effect)prop.info).matrix.M42);
                                Writer.Write(((effect)prop.info).matrix.M43);
                                Writer.Write(((effect)prop.info).matrix.M44);
                                Writer.Write(((effect)prop.info).field_9c);
                                WriteDvString(Writer, ((effect)prop.info).filename);
                                foreach(var i in ((effect)prop.info).field_dc)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in ((effect)prop.info).animData)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)10:
                                pathInterpolation elementPathInterpolation = (pathInterpolation)prop.info;
                                foreach(var i in elementPathInterpolation.data)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)11:
                                break;

                            case (elementID)12:
                                nearFarSetting elementNearFarSetting = (nearFarSetting)prop.info;
                                Writer.Write(elementNearFarSetting.field_00);
                                Writer.Write(elementNearFarSetting.near);
                                Writer.Write(elementNearFarSetting.far);
                                foreach(var i in elementNearFarSetting.field_10)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)13:
                                uvAnim elementUVAnim = (uvAnim)prop.info;
                                Writer.Write(elementUVAnim.field_00);
                                WriteDvString(Writer, elementUVAnim.filename);
                                Writer.Write(elementUVAnim.Field44);
                                Writer.Write(elementUVAnim.Field48);
                                Writer.Write(elementUVAnim.Field4C);
                                Writer.Write(elementUVAnim.Field50);
                                break;

                            case (elementID)14:
                                visAnim elementVisAnim = (visAnim)prop.info;
                                Writer.Write(elementVisAnim.field_40);
                                WriteDvString(Writer, elementVisAnim.filename);
                                foreach (var i in elementVisAnim.data1)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)15:
                                matAnim elementMatAnim = (matAnim)prop.info;
                                Writer.Write(elementMatAnim.field_00);
                                WriteDvString(Writer, elementMatAnim.filename);
                                Writer.Write(elementMatAnim.Field44);
                                Writer.Write(elementMatAnim.Field48);
                                Writer.Write(elementMatAnim.Field4C);
                                Writer.Write(elementMatAnim.Field50);
                                break;

                            case (elementID)16:
                                compAnim elementCompAnim = (compAnim)prop.info;
                                Writer.Write(elementCompAnim.field_60);
                                foreach(var i in elementCompAnim.data)
                                {
                                    Writer.Write(i);
                                }
                                Writer.Write(elementCompAnim.field_6c);
                                foreach(var i in elementCompAnim.animations)
                                {
                                    Writer.Write((uint)i.animType);
                                    WriteDvString(Writer, i.filename);
                                }
                                Writer.Write(elementCompAnim.field_03);
                                break;

                            case (elementID)17:
                                cameraOffset elementCameraOffset = (cameraOffset)prop.info;
                                foreach(var i in elementCameraOffset.data)
                                {
                                    Writer.Write(i);
                                }

                                foreach(var i in elementCameraOffset.animData)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)20:
                                sonicCam elementSonicCam = (sonicCam)prop.info;
                                foreach(var i in elementSonicCam.field_4c)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)21:
                                gameCam elementGameCam = (gameCam)prop.info;
                                foreach (var i in elementGameCam.field_4c)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1001:
                                dof elementDOF = (dof)prop.info;
                                Writer.Write(elementDOF.field_60);
                                Writer.Write(elementDOF.field_64);
                                Writer.Write(elementDOF.field_68);
                                Writer.Write(elementDOF.field_6c);
                                Writer.Write(elementDOF.far1);
                                Writer.Write(elementDOF.field_74);
                                Writer.Write(elementDOF.field_78);
                                Writer.Write(elementDOF.field_7c);
                                Writer.Write(elementDOF.far2);
                                Writer.Write(elementDOF.field_84);
                                Writer.Write(elementDOF.field_88);
                                Writer.Write(elementDOF.field_8c);
                                Writer.Write(elementDOF.field_90);
                                Writer.Write(elementDOF.field_94);
                                Writer.Write(elementDOF.field_98);
                                Writer.Write(elementDOF.field_9c);
                                Writer.Write(elementDOF.field_a0);
                                Writer.Write(elementDOF.field_a4);
                                Writer.Write(elementDOF.field_a8);
                                Writer.Write(elementDOF.field_ac);
                                foreach (var i in elementDOF.animData)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1002:
                                Writer.AddOffset("11", 0xA0);
                                break;

                            case (elementID)1003:
                                camExposure elementCamExposure = (camExposure)prop.info;
                                Writer.Write(elementCamExposure.unk1);
                                elementCamExposure.field_48 = new float[7];
                                foreach (var i in elementCamExposure.field_48)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in elementCamExposure.field_80)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1004:
                                shadowRes elementShadowRes = (shadowRes)prop.info;
                                Writer.Write(elementShadowRes.shadowRes1);
                                Writer.Write(elementShadowRes.shadowRes2);
                                break;

                            case (elementID)1007:
                                fog elementFog = (fog)prop.info;
                                foreach (var i in elementFog.data)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1008:
                                chromaticAberration elementChromatic = (chromaticAberration)prop.info;
                                foreach (var i in elementChromatic.data)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in elementChromatic.data1)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1009:
                                vignette elementVignette = (vignette)prop.info;
                                foreach (var i in elementVignette.data1)
                                {
                                    Writer.Write(i);
                                }
                                Writer.Write(elementVignette.data2);
                                foreach (var i in elementVignette.data3)
                                {
                                    Writer.Write(i);
                                }
                                Writer.Write(elementVignette.data4);
                                foreach (var i in elementVignette.data5)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in elementVignette.data6)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1010:
                                fade elementFade = (fade)prop.info;
                                Writer.Write(elementFade.color.A);
                                Writer.Write(elementFade.color.B);
                                Writer.Write(elementFade.color.G);
                                Writer.Write(elementFade.color.R);
                                foreach (var i in elementFade.curveData)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1011:
                                letterBox elementLetterBox = (letterBox)prop.info;
                                foreach (var i in elementLetterBox.curveData)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1012:
                                modelClipping elementModelClipping = (modelClipping)prop.info;
                                foreach (var i in elementModelClipping.data)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1014:
                                bossName elementBossName = (bossName)prop.info;
                                Writer.Write(elementBossName.field_00);
                                Writer.Write((uint)elementBossName.bossID);
                                break;

                            case (elementID)1015:
                                caption elementCaption = (caption)prop.info;
                                foreach (var i in elementCaption.captionName)
                                {
                                    Writer.Write(i);
                                }
                                Writer.Write((uint)elementCaption.languageType);
                                Writer.Write(elementCaption.padding);
                                break;

                            case (elementID)1016:
                                sound elementSound = (sound)prop.info;
                                WriteDvString(Writer, elementSound.cueName);
                                Writer.Write(elementSound.field_a0);
                                Writer.Write(elementSound.field_a4);
                                break;

                            case (elementID)1017:
                                time elementTime = (time)prop.info;
                                foreach (var i in elementTime.data)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1018:
                                sun elementSun = (sun)prop.info;
                                Writer.Write(elementSun.field_00);
                                Writer.Write(elementSun.unkFloat);
                                elementSun.field_01 = new uint[5];
                                foreach (var i in elementSun.field_01)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in elementSun.animData)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1019:
                                lookAtIK elementLookAtIK = (lookAtIK)prop.info;
                                Writer.Write(elementLookAtIK.field_60);
                                Writer.Write(elementLookAtIK.field_64);
                                WriteGUID(Writer, elementLookAtIK.guid);
                                foreach (var i in elementLookAtIK.field_78)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in elementLookAtIK.field_80)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1020:
                                camBlur elementCamBlur = (camBlur)prop.info;
                                Writer.Write(elementCamBlur.flag);
                                Writer.Write(elementCamBlur.blurAmount);
                                foreach (var i in elementCamBlur.curveData)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1021:
                                generalTrigger elementGeneralTrigger = (generalTrigger)prop.info;
                                Writer.Write(elementGeneralTrigger.field_00);
                                WriteDvString(Writer, elementGeneralTrigger.triggerName);
                                break;

                            case (elementID)1023:
                                ditherParam elementDither = (ditherParam)prop.info;
                                Writer.Write(elementDither.param1);
                                Writer.Write(elementDither.param2);
                                break;

                            case (elementID)1024:
                                QTE elementQTE = (QTE)prop.info;
                                Writer.Write((uint)elementQTE.qteType);
                                Writer.Write((uint)elementQTE.qteButton);
                                Writer.Write(elementQTE.redCircleSize);
                                Writer.Write(elementQTE.redCircleThickness);
                                Writer.Write(elementQTE.whiteLineThickness);
                                Writer.Write(elementQTE.whiteLineSpeed);
                                Writer.Write(elementQTE.multiplier);
                                Writer.Write(elementQTE.redCircleOutlineThickness);
                                Writer.Write(elementQTE.whiteLineOutlineThickness);
                                Writer.Write(elementQTE.failCount);
                                Writer.Write(elementQTE.field_88);
                                foreach (var i in elementQTE.field_8c)
                                {
                                    Writer.Write(i);
                                }
                                Writer.Write(elementQTE.field_cc);
                                Writer.Write(elementQTE.field_d0);
                                Writer.Write(elementQTE.field_d4);
                                foreach (var i in elementQTE.field_d8)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1026:
                                break;

                            case (elementID)1027:
                                aura elementAura = (aura)prop.info;
                                foreach (var i in elementAura.data)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1028:
                                break;

                            case (elementID)1029:
                                cyberSpaceNoise elementCyberSpaceNoise = (cyberSpaceNoise)prop.info;
                                Writer.Write(elementCyberSpaceNoise.field_4f);
                                foreach (var i in elementCyberSpaceNoise.data)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1031:
                                auraRoad elementAuraRoad = (auraRoad)prop.info;
                                Writer.Write(elementAuraRoad.field_00);
                                foreach (var i in elementAuraRoad.animData)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1032:
                                break;

                            case (elementID)1034:
                                weather elementWeather = (weather)prop.info;
                                foreach (var i in elementWeather.field_40)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1036:
                                variablePointLight elementPointLight = (variablePointLight)prop.info;
                                foreach (var i in elementPointLight.unk1)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in elementPointLight.unk2)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in elementPointLight.unk3)
                                {
                                    Writer.Write(i);
                                }
                                Writer.Write(elementPointLight.unk4);
                                foreach (var i in elementPointLight.unk5)
                                {
                                    Writer.Write(i);
                                }
                                foreach (var i in elementPointLight.data1)
                                {
                                    Writer.Write(i);
                                }
                                break;

                            case (elementID)1037:
                                break;

                            case (elementID)1042:
                                Writer.AddOffset("10", 0x1008);
                                break;

                            case (elementID)1043:
                                break;
                        }
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
