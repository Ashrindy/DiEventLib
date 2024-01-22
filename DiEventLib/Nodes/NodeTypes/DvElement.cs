using DiEventLib.Nodes.Elements;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.Nodes.NodeTypes
{
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
        atmosphericHeightFog = 1007,
        chromaticAberration = 1008,
        vignetteParam = 1009,
        fade = 1010,
        letterBox = 1011,
        modelClipping = 1012,
        bossName = 1014,
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

    public class DvElement : DvNodeObject
    {
        public elementProperties elementInfo;

        public DvElement(node Node = null, ExtendedBinaryReader reader = null, ExtendedBinaryWriter writer = null)
        {
            if (reader != null) { Read(reader); } else if (writer != null) { Write(writer, Node); }
        }

        public override void Read(ExtendedBinaryReader reader)
        {
            elementInfo.elementID = (elementID)reader.ReadUInt32();
            elementInfo.frameStart = reader.ReadSingle();
            elementInfo.frameEnd = reader.ReadSingle();
            elementInfo.version = reader.ReadInt32();
            elementInfo.flag = reader.ReadUInt32();
            elementInfo.playType = (playType)reader.ReadUInt32();
            elementInfo.updateTiming = (updateTiming)reader.ReadUInt32();
            elementInfo.padding = reader.ReadUInt32();

            switch (elementInfo.elementID)
            {
                case (elementID)1:
                    reader.JumpAhead(0x468);
                    break;

                case (elementID)3:
                    elementInfo.info = new DvElementDrawOff(reader: reader);
                    break;

                case (elementID)5:
                    elementInfo.info = new DvElementPathAdjustment(reader: reader);
                    break;

                case (elementID)6:
                    reader.JumpAhead(32);
                    break;

                case (elementID)7:
                    elementInfo.info = new DvElementCameraShakeLoop(reader: reader);
                    break;

                case (elementID)8:
                    elementInfo.info = new DvElementEffect(reader: reader);
                    break;

                case (elementID)10:
                    elementInfo.info = new DvElementPathInterpolation(reader: reader);
                    break;

                case (elementID)11:
                    elementInfo.info = new culling();
                    break;

                case (elementID)12:
                    elementInfo.info = new DvElementNearFarSetting(reader: reader);
                    break;

                case (elementID)13:
                    elementInfo.info = new DvElementUVAnimation(reader: reader);
                    break;

                case (elementID)14:
                    elementInfo.info = new DvElementVisibilityAnimation(reader: reader);
                    break;

                case (elementID)15:
                    elementInfo.info = new DvElementMaterialAnimation(reader: reader);
                    break;

                case (elementID)16:
                    elementInfo.info = new DvElementComplexAnimation(reader: reader);
                    break;

                case (elementID)17:
                    elementInfo.info = new DvElementCameraOffset(reader: reader);
                    break;

                case (elementID)20:
                    elementInfo.info = new DvElementSonicCamera(reader: reader);
                    break;

                case (elementID)21:
                    elementInfo.info = new DvElementGameCamera(reader: reader);
                    break;

                case (elementID)1001:
                    elementInfo.info = new DvElementDOF(reader: reader);
                    break;

                case (elementID)1002:
                    reader.JumpAhead(0xA0);
                    break;

                case (elementID)1003:
                    elementInfo.info = new DvElementCameraExposure(reader: reader);
                    break;

                case (elementID)1004:
                    elementInfo.info = new DvElementShadowResolution(reader: reader);
                    break;

                case (elementID)1007:
                    elementInfo.info = new DvElementFog(reader: reader);
                    break;

                case (elementID)1008:
                    elementInfo.info = new DvElementChromaticAberration(reader: reader);
                    break;

                case (elementID)1009:
                    elementInfo.info = new DvElementVignette(reader: reader);
                    break;

                case (elementID)1010:
                    elementInfo.info = new DvElementFade(reader: reader);
                    break;

                case (elementID)1011:
                    elementInfo.info = new DvElementLetterBox(reader: reader);
                    break;

                case (elementID)1012:
                    elementInfo.info = new DvElementModelClipping(reader: reader);
                    break;

                case (elementID)1014:
                    elementInfo.info = new DvElementBossName(reader: reader);
                    break;

                case (elementID)1015:
                    elementInfo.info = new DvElementCaption(reader: reader);
                    break;

                case (elementID)1016:
                    elementInfo.info = new DvElementSound(reader: reader);
                    break;

                case (elementID)1017:
                    elementInfo.info = new DvElementTime(reader: reader);
                    break;

                case (elementID)1018:
                    elementInfo.info = new DvElementSun(reader: reader);
                    break;

                case (elementID)1019:
                    elementInfo.info = new DvElementLookAtIK(reader: reader);
                    break;

                case (elementID)1020:
                    elementInfo.info = new DvElementCameraBlur(reader: reader);
                    break;

                case (elementID)1021:
                    elementInfo.info = new DvElementGeneralTrigger(reader: reader);
                    break;

                case (elementID)1023:
                    elementInfo.info = new DvElementDither(reader: reader);
                    break;

                case (elementID)1024:
                    elementInfo.info = new DvElementQTE(reader: reader);
                    break;

                case (elementID)1026:
                    elementInfo.info = new overrideASM();
                    break;

                case (elementID)1027:
                    elementInfo.info = new DvElementAura(reader: reader);
                    break;

                case (elementID)1028:
                    elementInfo.info = new changeTimeScale();
                    break;

                case (elementID)1029:
                    elementInfo.info = new DvElementCyberSpaceNoise(reader: reader);
                    break;

                case (elementID)1031:
                    elementInfo.info = new DvElementAuraRoad(reader: reader);
                    break;

                case (elementID)1032:
                    elementInfo.info = new movieView();
                    break;

                case (elementID)1034:
                    elementInfo.info = new DvElementWeather(reader: reader);
                    break;

                case (elementID)1036:
                    elementInfo.info = new DvElementVariablePointLight(reader: reader);
                    break;

                case (elementID)1037:
                    elementInfo.info = new openingLogo();
                    break;

                case (elementID)1042:
                    elementInfo.info = new theEndCableObject();
                    reader.JumpAhead(0x1008);
                    break;

                case (elementID)1043:
                    elementInfo.info = new rifleBeastLighting();
                    break;
            }
        }

        public override void Write(ExtendedBinaryWriter Writer, node Node)
        {
            Writer.Write((uint)((DvElement)Node.info).elementInfo.elementID);
            Writer.Write(((DvElement)Node.info).elementInfo.frameStart);
            Writer.Write(((DvElement)Node.info).elementInfo.frameEnd);
            Writer.Write(((DvElement)Node.info).elementInfo.version);
            Writer.Write(((DvElement)Node.info).elementInfo.flag);
            Writer.Write((uint)((DvElement)Node.info).elementInfo.playType);
            Writer.Write((uint)((DvElement)Node.info).elementInfo.updateTiming);
            Writer.Write(((DvElement)Node.info).elementInfo.padding);

            elementProperties prop = ((DvElement)Node.info).elementInfo;

            if(prop.info != null) {
                switch (prop.elementID)
                {
                    case (elementID)1:
                        Writer.AddOffset("07", 0x468);
                        break;

                    case (elementID)3:
                        foreach (var i in ((drawOff)prop.info).field_00)
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
                        foreach (var i in ((pathAdjustment)prop.info).field_40)
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
                        foreach (var i in ((cameraShakeLoop)prop.info).field_68)
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
                        Helper.WriteDvString(Writer, ((effect)prop.info).filename);
                        foreach (var i in ((effect)prop.info).field_dc)
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
                        foreach (var i in elementPathInterpolation.data)
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
                        foreach (var i in elementNearFarSetting.field_10)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (elementID)13:
                        uvAnim elementUVAnim = (uvAnim)prop.info;
                        Writer.Write(elementUVAnim.field_00);
                        Helper.WriteDvString(Writer, elementUVAnim.filename);
                        Writer.Write(elementUVAnim.Field44);
                        Writer.Write(elementUVAnim.Field48);
                        Writer.Write(elementUVAnim.Field4C);
                        Writer.Write(elementUVAnim.Field50);
                        break;

                    case (elementID)14:
                        visAnim elementVisAnim = (visAnim)prop.info;
                        Writer.Write(elementVisAnim.field_40);
                        Helper.WriteDvString(Writer, elementVisAnim.filename);
                        foreach (var i in elementVisAnim.data1)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (elementID)15:
                        matAnim elementMatAnim = (matAnim)prop.info;
                        Writer.Write(elementMatAnim.field_00);
                        Helper.WriteDvString(Writer, elementMatAnim.filename);
                        Writer.Write(elementMatAnim.Field44);
                        Writer.Write(elementMatAnim.Field48);
                        Writer.Write(elementMatAnim.Field4C);
                        Writer.Write(elementMatAnim.Field50);
                        break;

                    case (elementID)16:
                        compAnim elementCompAnim = (compAnim)prop.info;
                        Writer.Write(elementCompAnim.field_60);
                        foreach (var i in elementCompAnim.data)
                        {
                            Writer.Write(i);
                        }
                        Writer.Write(elementCompAnim.field_6c);
                        foreach (var i in elementCompAnim.animations)
                        {
                            Writer.Write((uint)i.animType);
                            Helper.WriteDvString(Writer, i.filename);
                        }
                        Writer.Write(elementCompAnim.field_03);
                        break;

                    case (elementID)17:
                        cameraOffset elementCameraOffset = (cameraOffset)prop.info;
                        foreach (var i in elementCameraOffset.data)
                        {
                            Writer.Write(i);
                        }

                        foreach (var i in elementCameraOffset.animData)
                        {
                            Writer.Write(i);
                        }
                        break;

                    case (elementID)20:
                        sonicCam elementSonicCam = (sonicCam)prop.info;
                        foreach (var i in elementSonicCam.field_4c)
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
                        Helper.WriteDvString(Writer, elementSound.cueName);
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
                        Helper.WriteGUID(Writer, elementLookAtIK.guid);
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
                        Helper.WriteDvString(Writer, elementGeneralTrigger.triggerName);
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
            }
        }
    }
}
