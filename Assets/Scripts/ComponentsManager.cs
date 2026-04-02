using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ComponentsManager : MonoBehaviour
{
    [Header("Black Probe")]
    public GameObject battery;
    public GameObject batteryPositive;
    public GameObject batteryNegative;


    [Header("Black Probe")]
    public GameObject blackProbe;
    public GameObject blackProbeConnector;
    public GameObject blackProbeHolder;
    [Header("Red Probe")]
    public GameObject redProbe;
    public GameObject redProbeConnector;
    public GameObject redProbeHolder;

    public GameObject multimeter;
    public GameObject acSocket;
    public GameObject comPort;
    public GameObject VΩPort;
    public GameObject selectorDial;
    public GameObject continuitySymbol;
    public Transform acDCSwitch;
    private int actionCount = 0;







    public void ToggleACSocketChildren(bool state)
    {
        if (acSocket == null)
            return;

        Transform socketTransform = acSocket.transform;

        if (socketTransform.childCount == 0)
            return;

        Transform firstChild = socketTransform.GetChild(0);

        foreach (Transform child in firstChild)
        {
            child.gameObject.SetActive(state);
        }
          UIManager.Instance.acDCSwitchIndicatorUI.SetActive(false);
    }



    // Optional: toggle without passing bool (better for poke)
    public void ToggleACDCSwitch()
    {
        InteractionManager.Instance.isDCMode = !InteractionManager.Instance.isDCMode;

        Vector3 localPos = acDCSwitch.localPosition;
        localPos.z = !InteractionManager.Instance.isDCMode ? -0.01804f : -0.02151f;

        acDCSwitch.localPosition = localPos;

        print("acdc switch local pos is - " + acDCSwitch.localPosition);
    }
    // =========================
    // 🔧 Helpers
    // =========================

    public void EnableInteraction(GameObject obj)
    {
        obj.GetComponent<HighlightEffect>().enabled = true;
        obj.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisableInteraction(GameObject obj)
    {
        obj.GetComponent<HighlightEffect>().enabled = false;
        obj.GetComponent<BoxCollider>().enabled = false;

    }
    public void EnableXRGrabbable(GameObject obj, bool isGrabbable)
    {
        obj.GetComponent<XRGrabInteractable>().enabled = isGrabbable;
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
        UIManager.Instance.comPortIndicatorUI.SetActive(true);
    }

    public void OnComPortClick()
    {
        StartCoroutine(OnComPortClickRoutine());
    }

    IEnumerator OnComPortClickRoutine()
    {
                UIManager.Instance.comPortIndicatorUI.SetActive(false);

        DisableInteraction(comPort);
        SoundManager.Instance.PlayBlackProbe();

        yield return WaitForAudio();

        EnableInteraction(VΩPort);
                UIManager.Instance.VΩPortIndicatorUI.SetActive(true);

    }

    public void OnVΩPortClick()
    {
        StartCoroutine(OnVΩPortClickRoutine());
    }

    IEnumerator OnVΩPortClickRoutine()
    {
                        UIManager.Instance.VΩPortIndicatorUI.SetActive(false);

        DisableInteraction(VΩPort);
        SoundManager.Instance.PlayRedProbe();

        yield return WaitForAudio();

        EnableInteraction(continuitySymbol);
        UIManager.Instance.continuitySymbolIndicatorUI.SetActive(true);
    }

    public void OnContinuitySymbolClick()
    {
        StartCoroutine(OnContinuitySymbolClickRoutine());
    }

    IEnumerator OnContinuitySymbolClickRoutine()
    {        UIManager.Instance.continuitySymbolIndicatorUI.SetActive(false);

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
        
        UIManager.Instance.comPortIndicatorUI.SetActive(true);
        UIManager.Instance.VΩPortIndicatorUI.SetActive(true);
        DisableInteraction(selectorDial);
        SoundManager.Instance.PlaySelectorDial();

        yield return WaitForAudio();
       UIManager.Instance.randBProbeHintButtonUI.SetActive(true);

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
        EnableXRGrabbable(blackProbeConnector, true);
        EnableXRGrabbable(redProbeConnector, true);


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