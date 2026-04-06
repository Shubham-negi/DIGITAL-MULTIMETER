using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSource;

    [Header("Voice Overs")]


    [Header("Scene 0 Voice Overs")]

    public AudioClip moduleIntroVO;
    public AudioClip beginButtonClick;

    [Header("Scene 1 Voice Overs")]
    public AudioClip eachComponentClick;
    public AudioClip digitalMultimeter;
    public AudioClip batteryVO;
    public AudioClip blackProbeVO;
    public AudioClip redProbeVO;
    public AudioClip acSocketBoardVO;

    public AudioClip exploreTheMultimeterVO;


    public AudioClip continuitySymbolVO;
    public AudioClip acDcSwitchVO;
    public AudioClip volategIntroVO;

    public AudioClip selectorDialVO;

    public AudioClip blackProbeComConnectVO;
    public AudioClip nowConnectRedProbeVO;
    public AudioClip redProbeComConnectVO;
    public AudioClip probesConnectedVO;
    public AudioClip connectRedAndBlackProbesVO;
    public AudioClip checkingMultimeterWorkingVO;
    public AudioClip turnToContinuityVO;
    public AudioClip touchBothProbesVO;
    public AudioClip probeTouchBeepVO;

    public AudioClip multimeterWorkingVO;

    public AudioClip scene3IntroVO;
    public AudioClip turnTheDialToDCVO;
    public AudioClip turnThedialTo20;

    public AudioClip dCFlowsInOneDirectionVO;
    public AudioClip placeTheProbesOnbatteryVO;

    public AudioClip redAndBlackIsConnectedVO;

    public AudioClip observeTheVoltageReadingVO;

    public AudioClip voltageis9inbatteryVO;

    public AudioClip voltageisReversedinbatteryVO;

    public AudioClip scene4IntroVO;

    public AudioClip moveNearToWallSocketVO;

    public AudioClip safteyTipsVO;

    public AudioClip turnThedialTo200;


    public AudioClip acVoltageChnagesDirectionVO;

    public AudioClip placeProbesOnACSocketVO;

    public AudioClip acVoltageReadingFluctuatesVO;

    public AudioClip aCVoltageForHomeVO;

    public AudioClip reverseTheProbeOnACVO;
    public AudioClip acVoltageNatureVO;

    public AudioClip switchToDC;
    public AudioClip switchToAC;

    public AudioClip scene5IntroVO;
    public AudioClip conclusionVO;



    [Header("Sounds")]
    public AudioClip beep;
    public AudioClip slideSwitch;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ensure AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // =========================
    // 🔊 Base Play Method
    // =========================
    void Play(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.Stop();          // Stop any current audio
        audioSource.clip = clip;     // Assign new clip
        audioSource.Play();          // Play
    }

    // =========================
    // 🎯 State Check (IMPORTANT)
    // =========================
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    // =========================
    // ⏳ Play + Wait (Coroutine)
    // =========================
    public IEnumerator PlayAndWait(AudioClip clip)
    {
        if (clip == null) yield break;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);
    }

    // =========================
    // 🎤 Voice Overs
    // =========================
    public void PlayScene0Intro() => Play(moduleIntroVO);
    public void PlayClickBegin() => Play(beginButtonClick);
    public void PlayClickEachComponent() => Play(eachComponentClick);
    public void PlayDigitalMultimeter() => Play(digitalMultimeter);
    public void PlayBlackProbe() => Play(blackProbeVO);
    public void PlayRedProbe() => Play(redProbeVO);
    public void PlayContinuitySymbol() => Play(continuitySymbolVO);
    public void PlaySelectorDial() => Play(selectorDialVO);
    public void PlayProbesConnected() => Play(probesConnectedVO);
    public void PlayBatteryVO() => Play(batteryVO);
    public void PlayACSocketBoardVO() => Play(acSocketBoardVO);
    public void PlayConnectRedAndBlackProbes() => Play(connectRedAndBlackProbesVO);
    public void PlayProbesConnectedVO() => Play(probesConnectedVO);
    public void PlayCheckingMultimeterWorkingVO() => Play(checkingMultimeterWorkingVO);
    public void PlayTurnToContinuityVO() => Play(turnToContinuityVO);

    public void PlayTouchBothProbesVO() => Play(touchBothProbesVO);

    public void PlayProbeTouchBeepVO() => Play(probeTouchBeepVO);

    public void PlayMultimeterWorkingVO() => Play(multimeterWorkingVO);

    public void PlayScene3IntroVO() => Play(scene3IntroVO);

    public void PlayTurnTheDialToDCVO() => Play(turnTheDialToDCVO);
    public void PlayDCFlowsInOneDirectionVO() => Play(dCFlowsInOneDirectionVO);
    public void PlayPlaceTheProbesOnBatteryVO() => Play(placeTheProbesOnbatteryVO);
    public void PlayRedAndBlackIsConnectedVO() => Play(redAndBlackIsConnectedVO);
    public void PlayObserveTheVoltageReadingVO() => Play(observeTheVoltageReadingVO);
    public void PlayVoltageIs9InBatteryVO() => Play(voltageis9inbatteryVO);
    public void PlayVoltageIsReversedInBatteryVO() => Play(voltageisReversedinbatteryVO);

    public void PlayScene4IntroVO() => Play(scene4IntroVO);
    public void PlayMoveNearToWallSocketVO() => Play(moveNearToWallSocketVO);
    public void PlaySafteyTipsVO() => Play(safteyTipsVO);
    public void PlaySwitchTOAC() => Play(switchToAC);
    public void PlaySwitchTODC() => Play(switchToDC);

    public void PlayACVoltageChangesDirectionVO() => Play(acVoltageChnagesDirectionVO);
    public void PlayPlaceProbesOnACSocketVO() => Play(placeProbesOnACSocketVO);
    public void PlayACVoltageReadingFluctuatesVO() => Play(acVoltageReadingFluctuatesVO);
    public void PlayACVoltageForHomeVO() => Play(aCVoltageForHomeVO);

    public void PlayReverseTheProbeOnACVO() => Play(reverseTheProbeOnACVO);
    public void PlayACVoltageNatureVO() => Play(acVoltageNatureVO);

    public void PlayScene5IntroVO() => Play(scene5IntroVO);
    public void PlayConclusionVO() => Play(conclusionVO);

    public void PlayTurnTheDialTo20() => Play(turnThedialTo20);
    public void PlayTurnTheDialTo200() => Play(turnThedialTo200);
    public void PlayAcDcSwitchVO() => Play(acDcSwitchVO);
    public void PlayVoltageIntroVO() => Play(volategIntroVO);

    public void PlayBlackProbeComConnectVO() => Play(blackProbeComConnectVO);
    public void PlayNowConnectRedProbeVO() => Play(nowConnectRedProbeVO);   

    public void PlayRedProbeComConnectVO() => Play(redProbeComConnectVO);


    // =========================
    // 🖱 UI Sounds
    // =========================
    public void PlayBeep() => Play(beep);
    public void PlaySlideSwitch() => Play(slideSwitch);

    public void PlayExploreTheMultimeterVO() => Play(exploreTheMultimeterVO);
}