using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib
{
    #region elementStuff
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

    #region miscElementStuff
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
    #endregion
    #endregion
}
