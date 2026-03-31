using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ComponentsManager : MonoBehaviour
{
    public GameObject battery;
    public GameObject blackProbe;
    public GameObject redProbe;
    public GameObject multimeter;
    public GameObject acSocket;

    public GameObject comPort;
    public GameObject VΩPort;

    public GameObject selectorDial;

    public GameObject continuitySymbol;


    public void Scene1_ComponentLearning()
    {
        SoundManager.Instance.PlayClickEachComponent();
        multimeter.GetComponent<HighlightEffect>().enabled = true;
        multimeter.GetComponent<BoxCollider>().enabled = true;
        Invoke("OnMultimeterClick", 3f);
    }
    public void OnMultimeterClick()
    {
        multimeter.GetComponent<HighlightEffect>().enabled = false;
        multimeter.GetComponent<BoxCollider>().enabled = false;
        SoundManager.Instance.PlayDigitalMultimeter();
        comPort.GetComponent<HighlightEffect>().enabled = true;
        comPort.GetComponent<BoxCollider>().enabled = true;
        Invoke("OnComPortClick", 3f);

    }
    public void OnComPortClick()
    {
        comPort.GetComponent<HighlightEffect>().enabled = false;
        comPort.GetComponent<BoxCollider>().enabled = false;
        SoundManager.Instance.PlayBlackProbe();
        VΩPort.GetComponent<HighlightEffect>().enabled = true;
        VΩPort.GetComponent<BoxCollider>().enabled = true;

        Invoke("OnVΩPortClick", 3f);

    }
    public void OnVΩPortClick()
    {
        VΩPort.GetComponent<HighlightEffect>().enabled = false;
        VΩPort.GetComponent<BoxCollider>().enabled = false;
        SoundManager.Instance.PlayRedProbe();
        continuitySymbol.GetComponent<HighlightEffect>().enabled = true;
        continuitySymbol.GetComponent<BoxCollider>().enabled = true;
        Invoke("OnContinuitySymbolClick", 3f);

    }

    public void OnContinuitySymbolClick()
    {
        continuitySymbol.GetComponent<HighlightEffect>().enabled = false;
        continuitySymbol.GetComponent<BoxCollider>().enabled = false;
        SoundManager.Instance.PlayContinuitySymbol();
        selectorDial.GetComponent<HighlightEffect>().enabled = true;
        selectorDial.GetComponent<BoxCollider>().enabled = true;
        Invoke("OnSelectorDialClick", 3f);
    }

    public void OnSelectorDialClick()
    {
        selectorDial.GetComponent<HighlightEffect>().enabled = false;
        selectorDial.GetComponent<BoxCollider>().enabled = false;
        SoundManager.Instance.PlaySelectorDial();
        battery.GetComponent<HighlightEffect>().enabled = true;
        battery.GetComponent<BoxCollider>().enabled = true;

        Invoke(nameof(USERACTION), 10f);
    }

    public void USERACTION()
    {
        SoundManager.Instance.PlayConnectRedAndBlackProbes();
        VΩPort.GetComponent<HighlightEffect>().enabled = true;
        comPort.GetComponent<HighlightEffect>().enabled = true;
        VΩPort.GetComponent<XRSocketInteractor>().enabled = true;
        comPort.GetComponent<XRSocketInteractor>().enabled = true;
        VΩPort.GetComponent<BoxCollider>().enabled = true;
        comPort.GetComponent<BoxCollider>().enabled = true;
        VΩPort.GetComponent<BoxCollider>().isTrigger = true;
        comPort.GetComponent<BoxCollider>().isTrigger = true;
        UIManager.Instance.ClickEachComponentTextUI(false);
        UIManager.Instance.ShowComponentsUI(false);
        UIManager.Instance.UserActionOnRAndBProbesUI(true);
    }


    private int actionCount = 0;

    public void MarkActionComplete()
    {
        actionCount++;
    }

    public bool AllComponentsLearned()
    {
        return actionCount >= 2;
    }




}
