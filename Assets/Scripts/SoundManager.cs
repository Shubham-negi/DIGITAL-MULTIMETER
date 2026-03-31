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

    [Header("Sounds")]
    public AudioClip beep;

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


    // =========================
    // 🖱 UI Sounds
    // =========================
    public void PlayBeep() => Play(beep);
}