using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using TMPro;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Activity2Manager : MonoBehaviour
{

    [Header("Activity 2 ")]

    public Transform acDCSwitchACTVTY2;
    public GameObject lightBulb;
    public ComponentsManager componentManager;

    public GameObject selectorDial;

    public GameObject blackProb;


    public GameObject redProb;

    public TextMeshProUGUI screenText;

    public GameObject faultySwitch;
    public GameObject newSwitch;

    public GameObject switchSocket;









    public bool isfaultySwitch = true;
    public bool isDcOn20V;
    public bool isDCMode = false;

    public void Activity2DestinationReached()
    {
        componentManager.activity2HighlightEffect.enabled = false;
        componentManager.destinationMarker.SetActive(false);
        SoundManager.Instance.PlayActivity2IntroVO();
        Invoke(nameof(Activity2Start), 8f);
    }


    [ContextMenu("start Activity 2 ")]
    public void Activity2Start()
    {
        SoundManager.Instance.PlayTurnOnSwitchVO();
        UIManager.Instance.turnOnSwitchIndicatorUI.SetActive(true);
    }

    bool activity2SwitchCalled = false;
    public void Activity2Switch(float val)
    {
        if (val >= .9 && val <= 1)
        {
            SoundManager.Instance.PlaySlideSwitch();
            if (isfaultySwitch == true && activity2SwitchCalled == false)
            {
                activity2SwitchCalled = true;
                FaultySwitchON();
            }
            if (isfaultySwitch == false && activity2SwitchCalled == true)
            {
                lightBulb.SetActive(true);
                activity2SwitchCalled = false;
                SoundManager.Instance.PlayCircuitComplete();
                UIManager.Instance.turnOnTheSwitch.SetActive(true);
                Invoke(nameof(ModuleCompleted), 5f);
            }
        }
    }

    public void FaultySwitchON()
    {
        StartCoroutine(faultySwitchON());
    }

    private IEnumerator faultySwitchON()

    {
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.PlayNoLightVO();
        yield return WaitForAudio();
        SoundManager.Instance.PlaySwitchTODC();
        yield return WaitForAudio();
        UIManager.Instance.turnOnSwitchIndicatorUI.SetActive(false);
        UIManager.Instance.switchToDCUI.SetActive(true);
        acDCSwitchACTVTY2.GetComponent<HighlightEffect>().enabled = true;
    }


    public void ToggleACDCSwitchACTVTY2()
    {
        isDCMode = !isDCMode;
        Vector3 localPos = acDCSwitchACTVTY2.localPosition;
        localPos.z = !isDCMode ? -0.01804f : -0.02151f;
        acDCSwitchACTVTY2.localPosition = localPos;
        Activity2AC_DCSwitch();
    }
    public void Activity2AC_DCSwitch()
    {
        if (isDCMode == true)
        {
            ActivityAC_DCSwitchON();
        }
    }


    public void ActivityAC_DCSwitchON()
    {
        StartCoroutine(activityAC_DCSwitchON());
    }

    private IEnumerator activityAC_DCSwitchON()

    {
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.PlayTurnTheDialTo20();
        UIManager.Instance.switchToDCUI.SetActive(false);
        UIManager.Instance.turnDialTo20UI.SetActive(true);
        selectorDial.GetComponent<XRKnob>().enabled = true;
        selectorDial.GetComponent<BoxCollider>().enabled = true;

    }

    public void UpdateDialValue(float value)
    {

        // Reset all states first (important)
        isDcOn20V = false;

        // 🔹 Continuity Mode
        if (value >= 0.61f && value <= 0.64f)
        {
            isDcOn20V = true;
            UIManager.Instance.turnDialTo20UI.SetActive(false);
            selectorDial.GetComponent<XRKnob>().enabled = false;
            selectorDial.GetComponent<BoxCollider>().enabled = false;

            ConnectBlackProb();

        }

    }

    public void ConnectBlackProb()
    {
        StartCoroutine(connectBlackProb());
    }


    private IEnumerator connectBlackProb()

    {
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.PlayConnectBlackProbTobattery();
        UIManager.Instance.connectBProbTON.SetActive(true);
        yield return WaitForAudio();
        blackProb.GetComponent<XRGrabInteractable>().enabled = true;

    }

    public void PlugInToBattery()
    {
        StartCoroutine(plugInToBattery());
    }


    private IEnumerator plugInToBattery()

    {
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.PlayBlackProbConnected();
        UIManager.Instance.connectBProbTON.SetActive(false);
        yield return WaitForAudio();
        SoundManager.Instance.PlayTouchRedProb();
        UIManager.Instance.connectRProbTOPos.SetActive(true);
        redProb.GetComponent<XRGrabInteractable>().enabled = true;
    }


    public void CheckPowerSource()
    {
        StartCoroutine(checkPowerSource());
    }


    private IEnumerator checkPowerSource()

    {
        screenText.text = "8.9V";

        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayBatteryIsWorking();
        UIManager.Instance.connectRProbTOPos.SetActive(false);
        yield return WaitForAudio();
        SoundManager.Instance.PlayMoveToSwitchInput();
        UIManager.Instance.moveToSwitchInput.SetActive(true);
        yield return WaitForAudio();
        SoundManager.Instance.PlayThisIsswitchInput();
    }
    public void FollowTheFlow()
    {
        StartCoroutine(followTheFlow());
    }
    private IEnumerator followTheFlow()

    {
        screenText.text = "8.9V";
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayPowerhasreachedToSwitch();
        yield return WaitForAudio();
        UIManager.Instance.moveToSwitchInput.SetActive(false);
        UIManager.Instance.moveToSwitchOutPut.SetActive(true);

        SoundManager.Instance.PlayMoveToSwitchOutput();
        yield return WaitForAudio();


    }
    public void TheCriticalCheck()
    {
        StartCoroutine(theCriticalCheck());
    }


    private IEnumerator theCriticalCheck()

    {
        UIManager.Instance.moveToSwitchOutPut.SetActive(true);

        screenText.text = "0V";
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayWaitNoVoltage();
        yield return WaitForAudio();

        yield return new WaitForSeconds(3f);


        SoundManager.Instance.PlayFaultySwitchfound();

        UIManager.Instance.moveToSwitchOutPut.SetActive(false);
        yield return WaitForAudio();

        SoundManager.Instance.PlayRemoveFaultySwitch();
        UIManager.Instance.removeTheFaultySwitch.SetActive(true);
        
       // faultySwitch.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
       // faultySwitch.transform.GetChild(1).GetComponent<XRKnob>().enabled = false;

        Destroy(faultySwitch.transform.GetChild(1).GetComponent<BoxCollider>());
       Destroy(faultySwitch.transform.GetChild(1).GetComponent<XRKnob>());
       Destroy(faultySwitch.transform.GetChild(1).GetComponent<Rigidbody>());

        faultySwitch.GetComponent<BoxCollider>().enabled = true;
        faultySwitch.GetComponent<Rigidbody>().useGravity = true;
        faultySwitch.GetComponent<Rigidbody>().isKinematic = false;
        faultySwitch.GetComponent<XRGrabInteractable>().enabled = true;




        // FixTheProblem();
    }

    public void FixTheProblem()
    {
        StartCoroutine(fixtheproblem());
    }


    private IEnumerator fixtheproblem()

    {


        UIManager.Instance.removeTheFaultySwitch.SetActive(false);

        faultySwitch.GetComponent<XRGrabInteractable>().enabled = false;
        newSwitch.SetActive(true);
        SoundManager.Instance.PlayReplaceSwitchWithNew();
        UIManager.Instance.replacewithnewSwitch.SetActive(true);
        yield return WaitForAudio();


        newSwitch.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        newSwitch.transform.GetChild(1).GetComponent<XRKnob>().enabled = false;


        newSwitch.GetComponent<BoxCollider>().enabled = true;
        newSwitch.GetComponent<Rigidbody>().useGravity = true;
        newSwitch.GetComponent<Rigidbody>().isKinematic = false;
        newSwitch.GetComponent<XRGrabInteractable>().enabled = true;

        switchSocket.SetActive(true);


        SoundManager.Instance.PlayFaultyMustbeReplaced();
    }


    public void FaultySwitchReplaced()
    {
        isfaultySwitch = false;





        newSwitch.GetComponent<BoxCollider>().enabled = false;
        newSwitch.GetComponent<Rigidbody>().useGravity = false;
        newSwitch.GetComponent<Rigidbody>().isKinematic = true;
        newSwitch.GetComponent<XRGrabInteractable>().enabled = false;

        newSwitch.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
        newSwitch.transform.GetChild(1).GetComponent<XRKnob>().enabled = true;

        UIManager.Instance.replacewithnewSwitch.SetActive(false);

        UIManager.Instance.turnOnTheSwitch.SetActive(true);

        SoundManager.Instance.PlayTurnOnSwitchForLightBulb();
        Destroy(switchSocket);
       // switchSocket.SetActive(false);



    }



    public void ModuleCompleted()
    {

        UIManager.Instance.exitRestartUI.SetActive(true);
        SoundManager.Instance.PlayEndConclusion();



    }





    ////Activity 2 Swich




    //----------------Sounds Audio Delay--------------------

    IEnumerator WaitForAudio()
    {
        yield return new WaitUntil(() => !SoundManager.Instance.IsPlaying());
    }

}
