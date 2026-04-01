using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSource;

    [Header("Voice Overs")]
    public AudioClip scene1Intro;
    public AudioClip beginButtonClick;
    public AudioClip eachComponentClick;
    public AudioClip digitalMultimeter;
    public AudioClip batteryVO;
    public AudioClip blackProbeVO;
    public AudioClip redProbeVO;
    public AudioClip continuitySymbolVO;
    public AudioClip selectorDialVO;
    public AudioClip probesConnectedVO;
    public AudioClip connectRedAndBlackProbesVO;
    public AudioClip acSocketVO;
    public AudioClip checkingMultimeterWorkingVO;
    public AudioClip turnToContinuityVO;
    public AudioClip touchBothProbesVO;
    public AudioClip probeTouchBeepVO;

    public AudioClip multimeterWorkingVO;

    public AudioClip scene3IntroVO;
    public AudioClip turnTheDialToDCVO;
    public AudioClip dCFlowsInOneDirectionVO;
    public AudioClip placeTheProbesOnbatteryVO;

    public AudioClip redAndBlackIsConnectedVO;

    public AudioClip observeTheVoltageReadingVO;

    public AudioClip voltageis9inbatteryVO;

    public AudioClip voltageisReversedinbatteryVO;

    public AudioClip scene4IntroVO;

    public AudioClip moveNearToWallSocketVO;

    public AudioClip safteyTipsVO;

    public AudioClip turnDialToAC;

    public AudioClip acVoltageChnagesDirectionVO;

public AudioClip placeProbesOnACSocketVO;

public AudioClip acVoltageReadingFluctuatesVO;

public AudioClip aCVoltageForHomeVO;

public AudioClip reverseTheProbeOnACVO;
public AudioClip acVoltageNatureVO;
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
    public void PlayScene1Intro() => Play(scene1Intro);
    public void PlayClickBegin() => Play(beginButtonClick);
    public void PlayClickEachComponent() => Play(eachComponentClick);
    public void PlayDigitalMultimeter() => Play(digitalMultimeter);
    public void PlayBlackProbe() => Play(blackProbeVO);
    public void PlayRedProbe() => Play(redProbeVO);
    public void PlayContinuitySymbol() => Play(continuitySymbolVO);
    public void PlaySelectorDial() => Play(selectorDialVO);
    public void PlayProbesConnected() => Play(probesConnectedVO);
    public void PlayBatteryVO() => Play(batteryVO);
    public void PlayACSocketVO() => Play(acSocketVO);
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
    public void PlayTurnDialToAC() => Play(turnDialToAC);
    public void PlayACVoltageChangesDirectionVO() => Play(acVoltageChnagesDirectionVO);
    public void PlayPlaceProbesOnACSocketVO() => Play(placeProbesOnACSocketVO);
    public void PlayACVoltageReadingFluctuatesVO() => Play(acVoltageReadingFluctuatesVO);
    public void PlayACVoltageForHomeVO() => Play(aCVoltageForHomeVO);

    public void PlayReverseTheProbeOnACVO() => Play(reverseTheProbeOnACVO);
    public void PlayACVoltageNatureVO() => Play(acVoltageNatureVO);

public void PlayScene5IntroVO() => Play(scene5IntroVO);
public void PlayConclusionVO() => Play(conclusionVO);

    // =========================
    // 🖱 UI Sounds
    // =========================
    public void PlayBeep() => Play(beep);
     public void PlaySlideSwitch() => Play(slideSwitch);
}