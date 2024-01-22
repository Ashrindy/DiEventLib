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
                    drawOff elementDrawOff = new drawOff();
                    elementDrawOff.field_00 = new uint[4];
                    for (int i = 0; i < 4; i++)
                    {
                        elementDrawOff.field_00[i] = reader.ReadUInt32();
                    }

                    elementInfo.info = elementDrawOff;
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

                    elementInfo.info = elementPathAdjustment;
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

                    elementInfo.info = elementCamShakeLoop;
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
                    elementEffect.filename = Helper.ReadDVString(reader);
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

                    elementInfo.info = elementEffect;
                    break;

                case (elementID)10:
                    pathInterpolation elementPathInterpolation = new pathInterpolation();
                    elementPathInterpolation.data = new byte[592];
                    for (int i = 0; i < 592; i++)
                    {
                        elementPathInterpolation.data[i] = reader.ReadByte();
                    }

                    elementInfo.info = elementPathInterpolation;
                    break;

                case (elementID)11:
                    elementInfo.info = new culling();
                    break;

                case (elementID)12:
                    nearFarSetting elementNearFarSetting = new nearFarSetting();
                    elementNearFarSetting.field_00 = reader.ReadUInt32();
                    elementNearFarSetting.near = reader.ReadSingle();
                    elementNearFarSetting.far = reader.ReadSingle();
                    elementNearFarSetting.field_10 = new uint[5];
                    for (int i = 0; i < 5; i++)
                    {
                        elementNearFarSetting.field_10[i] = reader.ReadUInt32();
                    }

                    elementInfo.info = elementNearFarSetting;
                    break;

                case (elementID)13:
                    uvAnim elementUVAnim = new uvAnim();
                    elementUVAnim.field_00 = reader.ReadUInt32();
                    elementUVAnim.filename = Helper.ReadDVString(reader);
                    elementUVAnim.Field44 = reader.ReadUInt32();
                    elementUVAnim.Field48 = reader.ReadSingle();
                    elementUVAnim.Field4C = reader.ReadUInt32();
                    elementUVAnim.Field50 = reader.ReadUInt32();

                    elementInfo.info = elementUVAnim;
                    break;

                case (elementID)14:
                    visAnim elementVisAnim = new visAnim();
                    elementVisAnim.field_40 = reader.ReadUInt32();
                    elementVisAnim.filename = Helper.ReadDVString(reader);
                    elementVisAnim.data1 = new byte[16];
                    for (int i = 0; i < 16; i++)
                    {
                        elementVisAnim.data1[i] = reader.ReadByte();
                    }

                    elementInfo.info = elementVisAnim;
                    break;

                case (elementID)15:
                    matAnim elementMatAnim = new matAnim();
                    elementMatAnim.field_00 = reader.ReadUInt32();
                    elementMatAnim.filename = Helper.ReadDVString(reader);
                    elementMatAnim.Field44 = reader.ReadUInt32();
                    elementMatAnim.Field48 = reader.ReadSingle();
                    elementMatAnim.Field4C = reader.ReadUInt32();
                    elementMatAnim.Field50 = reader.ReadUInt32();

                    elementInfo.info = elementMatAnim;
                    break;

                case (elementID)16:
                    compAnim elementCompAnim = new compAnim();
                    elementCompAnim.field_60 = reader.ReadUInt32();
                    elementCompAnim.data = new byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        elementCompAnim.data[i] = reader.ReadByte();
                    }
                    elementCompAnim.field_6c = reader.ReadUInt32();
                    elementCompAnim.animations = new anim[16];
                    for (int i = 0; i < 16; i++)
                    {
                        elementCompAnim.animations[i] = new anim();
                        elementCompAnim.animations[i].animType = (animType)reader.ReadUInt32();
                        elementCompAnim.animations[i].filename = Helper.ReadDVString(reader);
                    }
                    elementCompAnim.field_03 = reader.ReadUInt32();

                    elementInfo.info = elementCompAnim;
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

                    elementInfo.info = elementCameraOffset;
                    break;

                case (elementID)20:
                    sonicCam elementSonicCam = new sonicCam();
                    elementSonicCam.field_4c = new float[80];
                    for (int i = 0; i < 80; i++)
                    {
                        elementSonicCam.field_4c[i] = reader.ReadSingle();
                    }

                    elementInfo.info = elementSonicCam;
                    break;

                case (elementID)21:
                    gameCam elementGameCam = new gameCam();
                    elementGameCam.field_4c = new float[26];
                    for (int i = 0; i < 26; i++)
                    {
                        elementGameCam.field_4c[i] = reader.ReadSingle();
                    }

                    elementInfo.info = elementGameCam;
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

                    elementInfo.info = elementDOF;
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

                    elementInfo.info = elementCamExposure;
                    break;

                case (elementID)1004:
                    shadowRes elementShadowRes = new shadowRes();
                    elementShadowRes.shadowRes1 = reader.ReadUInt32();
                    elementShadowRes.shadowRes2 = reader.ReadUInt32();

                    elementInfo.info = elementShadowRes;
                    break;

                case (elementID)1007:
                    fog elementFog = new fog();
                    elementFog.data = new byte[300];
                    for (int i = 0; i < 300; i++)
                    {
                        elementFog.data[i] = reader.ReadByte();
                    }

                    elementInfo.info = elementFog;
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

                    elementInfo.info = elementChromatic;
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

                    elementInfo.info = elementVignette;
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

                    elementInfo.info = elementFade;
                    break;

                case (elementID)1011:
                    letterBox elementLetterBox = new letterBox();
                    elementLetterBox.curveData = new float[32];
                    for (int i = 0; i < 32; i++)
                    {
                        elementLetterBox.curveData[i] = reader.ReadSingle();
                    }

                    elementInfo.info = elementLetterBox;
                    break;

                case (elementID)1012:
                    modelClipping elementModelClipping = new modelClipping();
                    elementModelClipping.data = new byte[20];
                    for (int i = 0; i < 20; i++)
                    {
                        elementModelClipping.data[i] = reader.ReadByte();
                    }

                    elementInfo.info = elementModelClipping;
                    break;

                case (elementID)1014:
                    bossName elementBossName = new bossName();
                    elementBossName.field_00 = reader.ReadUInt32();
                    elementBossName.bossID = (bossID)reader.ReadUInt32();

                    elementInfo.info = elementBossName;
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

                    elementInfo.info = elementCaption;
                    break;

                case (elementID)1016:
                    sound elementSound = new sound();
                    elementSound.cueName = Helper.ReadDVString(reader);
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

                    elementInfo.info = elementTime;
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

                    elementInfo.info = elementSun;
                    break;

                case (elementID)1019:
                    lookAtIK elementLookAtIK = new lookAtIK();
                    elementLookAtIK.field_60 = reader.ReadUInt32();
                    elementLookAtIK.field_64 = reader.ReadUInt32();
                    elementLookAtIK.guid = Helper.ReadGUID(reader);
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

                    elementInfo.info = elementLookAtIK;
                    break;

                case (elementID)1020:
                    camBlur elementCamBlur = new camBlur();
                    elementCamBlur.flag = reader.ReadUInt32();
                    elementCamBlur.blurAmount = reader.ReadUInt32();
                    elementCamBlur.curveData = new float[34];
                    for (int i = 0; i < 34; i++)
                    {
                        elementCamBlur.curveData[i] = reader.ReadSingle();
                    }

                    elementInfo.info = elementCamBlur;
                    break;

                case (elementID)1021:
                    generalTrigger elementGeneralTrigger = new generalTrigger();
                    elementGeneralTrigger.field_00 = reader.ReadUInt32();
                    elementGeneralTrigger.triggerName = Helper.ReadDVString(reader);

                    elementInfo.info = elementGeneralTrigger;
                    break;

                case (elementID)1023:
                    ditherParam elementDither = new ditherParam();
                    elementDither.param1 = reader.ReadSingle();
                    elementDither.param2 = reader.ReadSingle();

                    elementInfo.info = elementDither;
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
                    for (int i = 0; i < 64; i++)
                    {
                        elementQTE.field_8c[i] = reader.ReadByte();
                    }
                    elementQTE.field_cc = reader.ReadSingle();
                    elementQTE.field_d0 = reader.ReadSingle();
                    elementQTE.field_d4 = reader.ReadSingle();
                    elementQTE.field_d8 = new byte[264];
                    for (int i = 0; i < 264; i++)
                    {
                        elementQTE.field_d8[i] = reader.ReadByte();
                    }

                    elementInfo.info = elementQTE;
                    break;

                case (elementID)1026:
                    elementInfo.info = new overrideASM();
                    break;

                case (elementID)1027:
                    aura elementAura = new aura();
                    elementAura.data = new byte[204];
                    for (int i = 0; i < 204; i++)
                    {
                        elementAura.data[i] = reader.ReadByte();
                    }

                    elementInfo.info = elementAura;
                    break;

                case (elementID)1028:
                    elementInfo.info = new changeTimeScale();
                    break;

                case (elementID)1029:
                    cyberSpaceNoise elementCyberSpaceNoise = new cyberSpaceNoise();
                    elementCyberSpaceNoise.field_4f = reader.ReadUInt32();
                    elementCyberSpaceNoise.data = new float[32];
                    for (int i = 0; i < 32; i++)
                    {
                        elementCyberSpaceNoise.data[i] = reader.ReadUInt32();
                    }

                    elementInfo.info = elementCyberSpaceNoise;
                    break;

                case (elementID)1031:
                    auraRoad elementAuraRoad = new auraRoad();
                    elementAuraRoad.field_00 = reader.ReadUInt32();
                    elementAuraRoad.animData = new float[64];
                    for (int i = 0; i < 64; i++)
                    {
                        elementAuraRoad.animData[i] = reader.ReadUInt32();
                    }

                    elementInfo.info = elementAuraRoad;
                    break;

                case (elementID)1032:
                    elementInfo.info = new movieView();
                    break;

                case (elementID)1034:
                    weather elementWeather = new weather();
                    elementWeather.field_40 = new uint[33];
                    for (int i = 0; i < 33; i++)
                    {
                        elementWeather.field_40[i] = reader.ReadUInt32();
                    }

                    elementInfo.info = elementWeather;
                    break;

                case (elementID)1036:
                    variablePointLight elementPointLight = new variablePointLight();
                    elementPointLight.unk1 = new float[7];
                    for (int i = 0; i < 7; i++)
                    {
                        elementPointLight.unk1[i] = reader.ReadUInt32();
                    }
                    elementPointLight.unk2 = new int[6];
                    for (int i = 0; i < 6; i++)
                    {
                        elementPointLight.unk2[i] = reader.ReadInt32();
                    }
                    elementPointLight.unk3 = new float[8];
                    for (int i = 0; i < 8; i++)
                    {
                        elementPointLight.unk3[i] = reader.ReadUInt32();
                    }
                    elementPointLight.unk4 = reader.ReadInt32();
                    elementPointLight.unk5 = new int[10];
                    for (int i = 0; i < 10; i++)
                    {
                        elementPointLight.unk5[i] = reader.ReadInt32();
                    }
                    elementPointLight.data1 = new float[128];
                    for (int i = 0; i < 128; i++)
                    {
                        elementPointLight.data1[i] = reader.ReadSingle();
                    }
                    elementInfo.info = elementPointLight;
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
