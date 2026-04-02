using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject beginUI;
    public GameObject componentsUI;

    public GameObject clickEachComponentText;

    public GameObject userActionOnRAndBProbes;

    public GameObject turnToContinuityUI;

    public GameObject touchProbesTogetherUI;

    public GameObject turnTheDialToDCUI;
    public GameObject turnThedialTo20UI;

    public GameObject connectRedAndBlackProbesUI;

    public GameObject observeTheVoltageReadingUI;

    public GameObject voltageIsReversedInBatteryUI;

    // public GameObject scene4IntroUI;
    public GameObject safetyTipsUI;
    public GameObject turnDialToACUI;
        public GameObject turnThedialTo200UI;
    public GameObject insertProbesInACSocketUI;
    public GameObject observeACVoltageFluctuationsUI;
    public GameObject reverseProbesInACSocketUI;

    public GameObject ConclusionUI;
        public GameObject exitRestartUI;




    [Header("HINT UI")]
    public GameObject randBProbeHintButtonUI;


    [Header("Highlight Indicator UI")]
    public GameObject comPortIndicatorUI;
    public GameObject VΩPortIndicatorUI;
    public GameObject continuitySymbolIndicatorUI;
    public GameObject acDCSwitchIndicatorUI;

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
    }
    public void BeginUI(bool active)
    {
        beginUI.SetActive(active);
    }
    public void ClickEachComponentTextUI(bool active)
    {
        clickEachComponentText.SetActive(active);
    }
    public void UserActionOnRAndBProbesUI(bool active)
    {
        userActionOnRAndBProbes.SetActive(active);
    }

    public void ShowComponentsUI(bool active)
    {
        componentsUI.SetActive(active);
    }


    public void TurnToContinuityUI(bool active)
    {
        turnToContinuityUI.SetActive(active);
    }

    public void TouchProbesTogetherUI(bool active)
    {
        touchProbesTogetherUI.SetActive(active);
    }

    public void TurnTheDialToDCUI(bool active)
    {
        turnTheDialToDCUI.SetActive(active);
    }

    public void ConnectRedAndBlackProbesUI(bool active)
    {
        connectRedAndBlackProbesUI.SetActive(active);
    }
    public void ObserveTheVoltageReadingUI(bool active)
    {
        observeTheVoltageReadingUI.SetActive(active);
    }


    public void VoltageIsReversedInBatteryUI(bool active)
    {
        voltageIsReversedInBatteryUI.SetActive(active);
    }
    public void SafetyTipsUI(bool active)
    {
        safetyTipsUI.SetActive(active);
    }

    public void TurnDialToACUI(bool active)
    {
        turnDialToACUI.SetActive(active);
    }

    public void InsertProbesInACSocketUI(bool active)
    {
        insertProbesInACSocketUI.SetActive(active);
    }

    public void ObserveACVoltageFluctuationsUI(bool active)
    {
        observeACVoltageFluctuationsUI.SetActive(active);
    }

    public void ReverseProbesInACSocketUI(bool active)
    {
        reverseProbesInACSocketUI.SetActive(active);
    }

    public void ConclusionUIActive(bool active)
    {
        ConclusionUI.SetActive(active);
    }

    public void RandBProbeHintButtonUI(bool active)
    {
        randBProbeHintButtonUI.SetActive(active);
    }
    public void RotateDialTo20UI(bool active)
    {
        turnThedialTo20UI.SetActive(active);
    }
    public void RotateDialTo200UI(bool active)
    {
        turnThedialTo200UI.SetActive(active);
    }

    public void ExitRestartUI(bool active)
    {
        exitRestartUI.SetActive(active);
    }

}
