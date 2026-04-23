using System;
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
    [Header("AC Socket")]

    public GameObject acSocket;

    public GameObject acSocketACIndicator;
    public GameObject acSocketDCIndicator;



    public GameObject comPort;
    public GameObject VΩPort;
    public GameObject selectorDial;
    public GameObject continuitySymbol;
    public Transform acDCSwitch;


    public HighlightEffect activity2HighlightEffect;
    public GameObject destinationMarker;



    public bool AllComponentsLearned = false;

    [Header("Activity 2 ")]

    public Transform acDCSwitchACTVTY2;



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

    public void ToggleACDCSwitchACTVTY2()
    {
        InteractionManager.Instance.isDCModeACTVTY2 = !InteractionManager.Instance.isDCModeACTVTY2;

        Vector3 localPos = acDCSwitchACTVTY2.localPosition;
        localPos.z = !InteractionManager.Instance.isDCModeACTVTY2 ? -0.01804f : -0.02151f;

        acDCSwitchACTVTY2.localPosition = localPos;
        InteractionManager.Instance.Activity2AC_DCSwitch();




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
        EnableInteraction(battery);

        EnableInteraction(redProbe);

        EnableInteraction(blackProbe);

        EnableInteraction(acSocket.gameObject);

    }

    public void MultimeterComponentIntroduction()
    {
        StartCoroutine(OnMultimeterClickRoutine());
    }

    IEnumerator OnMultimeterClickRoutine()
    {

        yield return new WaitForSeconds(2f);
        SoundManager.Instance.PlayExploreTheMultimeterVO();
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
    {
        UIManager.Instance.continuitySymbolIndicatorUI.SetActive(false);

        DisableInteraction(continuitySymbol);
        SoundManager.Instance.PlayContinuitySymbol();

        yield return WaitForAudio();
        UIManager.Instance.selectorDialIndicatorUI.SetActive(true);

        EnableInteraction(selectorDial);
    }



    public void OnSelectorDialClick()
    {
        StartCoroutine(OnSelectorDialClickRoutine());
    }

    IEnumerator OnSelectorDialClickRoutine()
    {


        UIManager.Instance.selectorDialIndicatorUI.SetActive(false);

        DisableInteraction(selectorDial);
        SoundManager.Instance.PlaySelectorDial();
        yield return WaitForAudio();

        UIManager.Instance.acDCSwitchIndicatorUI.SetActive(true);


        EnableInteraction(acDCSwitch.gameObject);

    }

    public void OnAcDcVolategeSwitchClick()
    {
        StartCoroutine(OnAcDcVoltageSwitchClickRoutine());
    }

    IEnumerator OnAcDcVoltageSwitchClickRoutine()
    {
        SoundManager.Instance.PlayAcDcSwitchVO();
        yield return WaitForAudio();

        yield return new WaitForSeconds(1f);
        acSocketACIndicator.GetComponent<HighlightEffect>().enabled = true;
        acSocketDCIndicator.GetComponent<HighlightEffect>().enabled = true;

        SoundManager.Instance.PlayVoltageIntroVO();
        yield return WaitForAudio();

        yield return new WaitForSeconds(1f);
        acSocketACIndicator.gameObject.SetActive(false);
        acSocketDCIndicator.gameObject.SetActive(false);
        UIManager.Instance.comPortIndicatorUI.SetActive(true);
        UIManager.Instance.VΩPortIndicatorUI.SetActive(true);

        UIManager.Instance.randBProbeHintButtonUI.SetActive(true);
        DisableInteraction(acDCSwitch.gameObject);
        StartCoroutine(StartBlackProbeStep());
    }

    // ==================================
    // 👤 User Action On Black and Red Probe
    // ====================================



    IEnumerator StartBlackProbeStep()
    {

        SoundManager.Instance.PlayBlackProbeComConnectVO();

        yield return WaitForAudio();

        EnableInteraction(comPort);
        EnableInteraction(blackProbeConnector);

        EnableXRGrabbable(blackProbeConnector, true);

        var cSocket = comPort.GetComponent<XRSocketInteractor>();
        cSocket.enabled = true;

        var cCol = comPort.GetComponent<BoxCollider>();
        cCol.isTrigger = true;

        // UI + VO
        UIManager.Instance.ShowComponentsUI(false);
        UIManager.Instance.UserActionOnRAndBProbesUI(true);


    }

    public void OnBlackProbeConnected()
    {
        StartCoroutine(StartRedProbeStep());
    }

    IEnumerator StartRedProbeStep()
    {
        // =========================
        // 🔒 LOCK BLACK PROBE PROPERLY
        // =========================


        yield return new WaitForSeconds(1f);
        EnableXRGrabbable(blackProbeConnector, false);

        var rb = blackProbeConnector.GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;





        // Disable interaction completely
        DisableInteraction(blackProbeConnector);

        // Parent it so it stays fixed
        // blackProbeConnector.transform.SetParent(comPort.transform);


        // =========================
        // 🔊 TRANSITION VO
        // =========================

        SoundManager.Instance.PlayNowConnectRedProbeVO();
        // “Now connect the red probe to the voltage port.”
        yield return WaitForAudio();


        // =========================
        // 🔴 ENABLE RED PROBE STEP
        // =========================


        DisableInteraction(comPort);

        EnableInteraction(VΩPort);

        var cSocket = VΩPort.GetComponent<XRSocketInteractor>();
        cSocket.enabled = true;

        var cCol = VΩPort.GetComponent<BoxCollider>();
        cCol.isTrigger = true;
        EnableInteraction(redProbeConnector);

        EnableXRGrabbable(redProbeConnector, true);


        // =========================
        // 🔊 INSTRUCTION VO
        // =========================

        SoundManager.Instance.PlayRedProbeComConnectVO();
        yield return WaitForAudio();
    }
    // =========================
    // ✅ Progress Tracking
    // =========================



    public void MarkComponentsLearned()
    {
        AllComponentsLearned = true;
    }


    // =========================
    // ✅ Progress Tracking of component click 

    // =========================
    internal int componentActionCount = 0;
    public void MarkComponentComplete()
    {
        componentActionCount++;
    }

    public bool AllComponentsClicked()
    {


        return componentActionCount >= 5;
    }
}