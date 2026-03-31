using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ComponentsManager : MonoBehaviour
{
    public GameObject battery;
    public GameObject blackProbe;
    public GameObject redProbe;
      public GameObject blackProbeConnector;
    public GameObject redProbeConnector;
    public GameObject multimeter;
    public GameObject acSocket;

    public GameObject comPort;
    public GameObject VΩPort;
    

    public GameObject selectorDial;
    public GameObject continuitySymbol;

    private int actionCount = 0;

    // =========================
    // 🔧 Helpers
    // =========================

    void EnableInteraction(GameObject obj)
    {
        obj.GetComponent<HighlightEffect>().enabled = true;
        obj.GetComponent<BoxCollider>().enabled = true;
    }

    void DisableInteraction(GameObject obj)
    {
        obj.GetComponent<HighlightEffect>().enabled = false;
        obj.GetComponent<BoxCollider>().enabled = false;

    }
    
    IEnumerator WaitForAudio()
    {
        yield return new WaitUntil(() => !SoundManager.Instance.IsPlaying());
    }

    // =========================
    // 🎬 Flow
    // =========================

    public void Scene1_ComponentLearning()
    {
        SoundManager.Instance.PlayClickEachComponent();
        EnableInteraction(multimeter);
    }

    public void OnMultimeterClick()
    {
        StartCoroutine(OnMultimeterClickRoutine());
    }

    IEnumerator OnMultimeterClickRoutine()
    {
        DisableInteraction(multimeter);
        SoundManager.Instance.PlayDigitalMultimeter();

        yield return WaitForAudio();

        EnableInteraction(comPort);
    }

    public void OnComPortClick()
    {
        StartCoroutine(OnComPortClickRoutine());
    }

    IEnumerator OnComPortClickRoutine()
    {
        DisableInteraction(comPort);
        SoundManager.Instance.PlayBlackProbe();

        yield return WaitForAudio();

        EnableInteraction(VΩPort);
    }

    public void OnVΩPortClick()
    {
        StartCoroutine(OnVΩPortClickRoutine());
    }

    IEnumerator OnVΩPortClickRoutine()
    {
        DisableInteraction(VΩPort);
        SoundManager.Instance.PlayRedProbe();

        yield return WaitForAudio();

        EnableInteraction(continuitySymbol);
    }

    public void OnContinuitySymbolClick()
    {
        StartCoroutine(OnContinuitySymbolClickRoutine());
    }

    IEnumerator OnContinuitySymbolClickRoutine()
    {
        DisableInteraction(continuitySymbol);
        SoundManager.Instance.PlayContinuitySymbol();

        yield return WaitForAudio();

        EnableInteraction(selectorDial);
    }

    public void OnSelectorDialClick()
    {
        StartCoroutine(OnSelectorDialClickRoutine());
    }

    IEnumerator OnSelectorDialClickRoutine()
    {
        DisableInteraction(selectorDial);
        SoundManager.Instance.PlaySelectorDial();

        yield return WaitForAudio();

        UserAction();

    }

    // =========================
    // 👤 User Action
    // =========================

    public void UserAction()
    {
        SoundManager.Instance.PlayConnectRedAndBlackProbes();

        EnableInteraction(VΩPort);
        EnableInteraction(comPort);
        EnableInteraction(blackProbeConnector);
        EnableInteraction(redProbeConnector);
         

        var vSocket = VΩPort.GetComponent<XRSocketInteractor>();
        var cSocket = comPort.GetComponent<XRSocketInteractor>();

        vSocket.enabled = true;
        cSocket.enabled = true;

        var vCol = VΩPort.GetComponent<BoxCollider>();
        var cCol = comPort.GetComponent<BoxCollider>();

        vCol.isTrigger = true;
        cCol.isTrigger = true;

        UIManager.Instance.ClickEachComponentTextUI(false);
        UIManager.Instance.ShowComponentsUI(false);
        UIManager.Instance.UserActionOnRAndBProbesUI(true);
    }

    // =========================
    // ✅ Progress Tracking
    // =========================

    public void MarkActionComplete()
    {
        actionCount++;
    }

    public bool AllComponentsLearned()
    {

        
        return actionCount >= 2;
    }
}