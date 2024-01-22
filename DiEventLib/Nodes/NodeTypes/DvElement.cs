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

        public DvNodeObject GetElementInfoByType<DvNodeObject>()
        {
            object result = null;
            try
            {
                result = (DvNodeObject)elementInfo.info;
            }
            catch
            {
            }
            return (DvNodeObject)result;
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
                        new DvElementDrawOff(Node, writer: Writer);
                        break;

                    case (elementID)5:
                        new DvElementPathAdjustment(Node, writer: Writer);
                        break;

                    case (elementID)6:
                        Writer.AddOffset("08", 32);
                        break;

                    case (elementID)7:
                        new DvElementCameraShakeLoop(Node, writer: Writer);
                        break;

                    case (elementID)8:
                        new DvElementEffect(Node, writer: Writer);
                        break;

                    case (elementID)10:
                        new DvElementPathInterpolation(Node, writer: Writer);
                        break;

                    case (elementID)11:
                        break;

                    case (elementID)12:
                        new DvElementNearFarSetting(Node, writer: Writer);
                        break;

                    case (elementID)13:
                        new DvElementUVAnimation(Node, writer: Writer);
                        break;

                    case (elementID)14:
                        new DvElementVisibilityAnimation(Node, writer: Writer);
                        break;

                    case (elementID)15:
                        new DvElementMaterialAnimation(Node, writer: Writer);
                        break;

                    case (elementID)16:
                        new DvElementComplexAnimation(Node, writer: Writer);
                        break;
             
                    case (elementID)17:
                        new DvElementCameraOffset(Node, writer: Writer);
                        break;

                    case (elementID)20:
                        new DvElementSonicCamera(Node, writer: Writer);
                        break;

                    case (elementID)21:
                        new DvElementGameCamera(Node, writer: Writer);
                        break;

                    case (elementID)1001:
                        new DvElementDOF(Node, writer: Writer);
                        break;

                    case (elementID)1002:
                        Writer.AddOffset("11", 0xA0);
                        break;

                    case (elementID)1003:
                        new DvElementCameraExposure(Node, writer: Writer);
                        break;

                    case (elementID)1004:
                        new DvElementShadowResolution(Node, writer: Writer);
                        break;

                    case (elementID)1007:
                        new DvElementFog(Node, writer: Writer);
                        break;

                    case (elementID)1008:
                        new DvElementChromaticAberration(Node, writer: Writer);
                        break;

                    case (elementID)1009:
                        new DvElementVignette(Node, writer: Writer);
                        break;

                    case (elementID)1010:
                        new DvElementFade(Node, writer: Writer);
                        break;

                    case (elementID)1011:
                        new DvElementLetterBox(Node, writer: Writer);
                        break;

                    case (elementID)1012:
                        new DvElementModelClipping(Node, writer: Writer);
                        break;

                    case (elementID)1014:
                        new DvElementBossName(Node, writer: Writer);
                        break;

                    case (elementID)1015:
                        new DvElementCaption(Node, writer: Writer);
                        break;

                    case (elementID)1016:
                        new DvElementSound(Node, writer: Writer);
                        break;

                    case (elementID)1017:
                        new DvElementTime(Node, writer: Writer);
                        break;

                    case (elementID)1018:
                        new DvElementSun(Node, writer: Writer);
                        break;

                    case (elementID)1019:
                        new DvElementLookAtIK(Node, writer: Writer);
                        break;

                    case (elementID)1020:
                        new DvElementCameraBlur(Node, writer: Writer);
                        break;

                    case (elementID)1021:
                        new DvElementGeneralTrigger(Node, writer: Writer);
                        break;

                    case (elementID)1023:
                        new DvElementDither(Node, writer: Writer);
                        break;

                    case (elementID)1024:
                        new DvElementQTE(Node, writer: Writer);
                        break;

                    case (elementID)1026:
                        break;

                    case (elementID)1027:
                        new DvElementAura(Node, writer: Writer);
                        break;

                    case (elementID)1028:
                        break;

                    case (elementID)1029:
                        new DvElementCyberSpaceNoise(Node, writer: Writer);
                        break;

                    case (elementID)1031:
                        new DvElementAuraRoad(Node, writer: Writer);
                        break;

                    case (elementID)1032:
                        break;

                    case (elementID)1034:
                        new DvElementWeather(Node, writer: Writer);
                        break;

                    case (elementID)1036:
                        new DvElementVariablePointLight(Node, writer: Writer);
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
