using System.Collections;
using HighlightPlus;
using TMPro;
using Unity.VisualScripting;
using Unity.VRTemplate;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ActivityManager : MonoBehaviour
{
    [Header("Managers")]
    public UIManager uiManager;
    public SoundManager soundManager;
    public ComponentsManager componentManager;







    private void Start()
    {
        StartCoroutine(Scene0_Intro());
    }
    public void StartScene1ComponentLearning()
    {
        StartCoroutine(Scene1_ComponentLearning());
    }


    // =========================
    // SCENE 0 — INTRO
    // =========================
    private IEnumerator Scene0_Intro()
    {


        soundManager.PlayScene0Intro();

        yield return WaitForAudio();

        soundManager.PlayClickBegin();
        uiManager.BeginUI(true);
    }

    // =========================
    // SCENE 1 — COMPONENTS
    // =========================
    private IEnumerator Scene1_ComponentLearning()
    {
        uiManager.BeginUI(false);
        uiManager.ClickEachComponentTextUI(true);
        uiManager.ShowComponentsUI(true);
        componentManager.Scene1_ComponentLearning();

        yield return new WaitUntil(() => componentManager.AllComponentsClicked());
        componentManager.MultimeterComponentIntroduction();
        yield return new WaitUntil(() => componentManager.AllComponentsLearned == true);


        UIManager.Instance.randBProbeHintButtonUI.SetActive(false);
        UIManager.Instance.comPortIndicatorUI.SetActive(false);
        UIManager.Instance.VΩPortIndicatorUI.SetActive(false);


        UIManager.Instance.UserActionOnRAndBProbesUI(false);
        soundManager.PlayProbesConnectedVO();

        yield return WaitForAudio();

        componentManager.DisableInteraction(componentManager.VΩPort);
        componentManager.DisableInteraction(componentManager.comPort);
        componentManager.DisableInteraction(componentManager.blackProbeConnector);
        componentManager.DisableInteraction(componentManager.redProbeConnector);





        StartCoroutine(Scene2_CheckingMultimeter());


    }

    // =========================
    // SCENE 2 — COMPONENTS
    // =========================

    private IEnumerator Scene2_CheckingMultimeter()
    {
        yield return new WaitForSeconds(4f);

        uiManager.ShowComponentsUI(false);

        componentManager.EnableselectorDial(true);


        soundManager.PlayTurnToContinuityVO();
        UIManager.Instance.TurnToContinuityUI(true);

        componentManager.continuitySymbol.SetActive(true);
        yield return WaitForAudio();

        yield return new WaitUntil(() => InteractionManager.Instance.isContinuityMode);
                componentManager.EnableselectorDial(false);

        componentManager.continuitySymbol.SetActive(false);

        yield return new WaitForSeconds(2f);

        componentManager.EnableXRGrabbable(componentManager.blackProbeHolder, true);
        componentManager.EnableXRGrabbable(componentManager.redProbeHolder, true);


        soundManager.PlayTouchBothProbesVO();
        UIManager.Instance.TurnToContinuityUI(false);
        UIManager.Instance.TouchProbesTogetherUI(true);
        yield return WaitForAudio();




        yield return new WaitUntil(() => InteractionManager.Instance.AreProbesTouching());
        SoundManager.Instance.PlayBeep();
        yield return WaitForAudio();
        yield return new WaitForSeconds(2f);
        soundManager.PlayProbeTouchBeepVO();
        yield return WaitForAudio();

        soundManager.PlayMultimeterWorkingVO();
        UIManager.Instance.TouchProbesTogetherUI(false);
        StartCoroutine(Scene3_MeasuringDCVoltage());
    }
    // =========================
    // SCENE 3 — Scene3_MeasuringDCVoltage
    // =========================

    private IEnumerator Scene3_MeasuringDCVoltage()
    {
        yield return new WaitForSeconds(5f);
        componentManager.acSocket.SetActive(false);
        componentManager.battery.SetActive(true);
        soundManager.PlayScene3IntroVO();
        yield return WaitForAudio();

        UIManager.Instance.TurnTheDialToDCUI(true);
        componentManager.EnableInteraction(componentManager.acDCSwitch.gameObject);
        componentManager.acDCSwitch.gameObject.GetComponent<XRSimpleInteractable>().enabled = true;
        soundManager.PlayTurnTheDialToDCVO();
               

        yield return WaitForAudio();
        UIManager.Instance.acDCSwitchIndicatorUI.SetActive(true);

        yield return new WaitUntil(() => InteractionManager.Instance.isDCMode);

        componentManager.DisableInteraction(componentManager.acDCSwitch.gameObject);

        UIManager.Instance.TurnTheDialToDCUI(false);
        yield return new WaitForSeconds(2f);
        UIManager.Instance.acDCSwitchIndicatorUI.SetActive(false);
        soundManager.PlayTurnTheDialTo20();
        componentManager.symbol_20.SetActive(true);

        yield return WaitForAudio();
        UIManager.Instance.RotateDialTo20UI(true);

        componentManager.EnableselectorDial(true);


        yield return new WaitUntil(() => InteractionManager.Instance.isDCMode && InteractionManager.Instance.isDcOn20V);
        UIManager.Instance.RotateDialTo20UI(false);
        componentManager.symbol_20.SetActive(false);
                componentManager.EnableselectorDial(false);


        yield return new WaitForSeconds(1f);


        soundManager.PlayConnectRedAndBlackProbes();

        UIManager.Instance.ConnectRedAndBlackProbesUI(true);

        yield return WaitForAudio();

        componentManager.EnableXRGrabbable(componentManager.redProbeHolder, true);
        componentManager.EnableXRGrabbable(componentManager.blackProbeHolder, true);

        // componentManager.EnableInteraction(componentManager.batteryPositive);
        // componentManager.EnableInteraction(componentManager.batteryNegative);

        componentManager.batteryNegative.GetComponent<HighlightEffect>().enabled = true;
        componentManager.batteryPositive.GetComponent<HighlightEffect>().enabled = true;



        yield return new WaitUntil(() => InteractionManager.Instance.redTouch == "BatteryPlus" && InteractionManager.Instance.blackTouch == "BatteryMinus");

        SoundManager.Instance.PlayRedAndBlackIsConnectedVO();
        yield return WaitForAudio();


        yield return new WaitForSeconds(2f);

        UIManager.Instance.ConnectRedAndBlackProbesUI(false);
        UIManager.Instance.ObserveTheVoltageReadingUI(true);

        soundManager.PlayObserveTheVoltageReadingVO();

        yield return WaitForAudio();

        soundManager.PlayVoltageIs9InBatteryVO();

        yield return WaitForAudio();

        componentManager.batteryNegative.GetComponent<HighlightEffect>().innerGlowColor = Color.red;
        componentManager.batteryNegative.GetComponent<HighlightEffect>().overlayColor = Color.red;

        componentManager.batteryPositive.GetComponent<HighlightEffect>().innerGlowColor = Color.black;
        componentManager.batteryPositive.GetComponent<HighlightEffect>().overlayColor = Color.black;

        soundManager.PlayReverseTheProbeOnACVO();
        yield return WaitForAudio();
        yield return new WaitUntil(() => InteractionManager.Instance.redTouch == "BatteryMinus" && InteractionManager.Instance.blackTouch == "BatteryPlus");
        UIManager.Instance.ObserveTheVoltageReadingUI(false);


        yield return new WaitForSeconds(2f);

        soundManager.PlayVoltageIsReversedInBatteryVO();
        yield return WaitForAudio();

        UIManager.Instance.VoltageIsReversedInBatteryUI(true);



        UIManager.Instance.VoltageIsReversedInBatteryUI(false);

        StartCoroutine(Scene4_MeasuringACVoltage());
    }


    // =========================
    // SCENE 4  f— Scene4_MeasuringACVoltage
    // =========================

    private IEnumerator Scene4_MeasuringACVoltage()
    {
        yield return new WaitForSeconds(5f);
        componentManager.acSocket.SetActive(true);
        componentManager.battery.SetActive(false);
        soundManager.PlayScene4IntroVO();
        yield return WaitForAudio();
        


        uiManager.SafetyTipsUI(true);
        soundManager.PlaySafteyTipsVO();
        yield return WaitForAudio();
        uiManager.SafetyTipsUI(false);



        uiManager.TurnDialToACUI(true);

        soundManager.PlaySwitchTOAC();

                 yield return WaitForAudio();


        componentManager.EnableInteraction(componentManager.acDCSwitch.gameObject);
        UIManager.Instance.acDCSwitchIndicatorUI.SetActive(true);

        yield return new WaitUntil(() => !InteractionManager.Instance.isDCMode);
        uiManager.TurnDialToACUI(false);

        yield return new WaitForSeconds(1f);


        UIManager.Instance.RotateDialTo200UI(true);
        soundManager.PlayTurnTheDialTo200();
        componentManager.symbol_200.SetActive(true);

        yield return WaitForAudio();
        UIManager.Instance.acDCSwitchIndicatorUI.SetActive(false);

        componentManager.EnableselectorDial(true);

        yield return new WaitUntil(() => !InteractionManager.Instance.isDCMode && InteractionManager.Instance.isACOn200V);
        UIManager.Instance.RotateDialTo200UI(false);
        componentManager.symbol_200.SetActive(false);

        componentManager.EnableselectorDial(false);

        yield return new WaitForSeconds(2f);

        soundManager.PlayACVoltageChangesDirectionVO();
        yield return WaitForAudio();

        uiManager.InsertProbesInACSocketUI(true);
        componentManager.ToggleACSocketChildren(true);
        soundManager.PlayPlaceProbesOnACSocketVO();
        yield return WaitForAudio();

        yield return new WaitUntil
        (() => (InteractionManager.Instance.redTouch == "SwitchMinus" && InteractionManager.Instance.blackTouch == "SwitchPlus")
         || (InteractionManager.Instance.redTouch == "SwitchPlus" && InteractionManager.Instance.blackTouch == "SwitchMinus"));
        yield return new WaitForSeconds(2f);

        uiManager.InsertProbesInACSocketUI(false);
        soundManager.PlayObserveTheVoltageReadingVO();
        yield return WaitForAudio();
        uiManager.ObserveACVoltageFluctuationsUI(true);

        soundManager.PlayACVoltageForHomeVO();
        yield return WaitForAudio();
        uiManager.ObserveACVoltageFluctuationsUI(false);
        uiManager.ReverseProbesInACSocketUI(true);

        soundManager.PlayReverseTheProbeOnACVO();
        yield return WaitForAudio();

        yield return new WaitUntil
               (() => (InteractionManager.Instance.redTouch == "SwitchMinus" && InteractionManager.Instance.blackTouch == "SwitchPlus")
                || (InteractionManager.Instance.redTouch == "SwitchPlus" && InteractionManager.Instance.blackTouch == "SwitchMinus"));
        yield return new WaitForSeconds(2f);

        uiManager.ReverseProbesInACSocketUI(false);
        soundManager.PlayACVoltageNatureVO();
        yield return WaitForAudio();

        componentManager.ToggleACSocketChildren(true);


        StartCoroutine(Scene5_Conclusison());
    }


    // =========================
    // SCENE 5   Scene5_Conclusion
    // =========================

    private IEnumerator Scene5_Conclusison()

    {
        yield return new WaitForSeconds(5f);
        uiManager.ConclusionUIActive(true);

        soundManager.PlayScene5IntroVO();
        yield return WaitForAudio();

        soundManager.PlayConclusionVO();
        yield return WaitForAudio();
        soundManager.PlayConclusionVO2();
        yield return WaitForAudio();
        soundManager.PlayActivity2NavigationVO();
        yield return WaitForAudio();

        componentManager.activity2HighlightEffect.enabled = true;
        componentManager.destinationMarker.SetActive(true);
        // uiManager.ExitRestartUI(true);






    }


    IEnumerator WaitForAudio()
    {
        yield return new WaitUntil(() => !SoundManager.Instance.IsPlaying());
    }
    /// <summary>
    /// Activity 2 
    /// </summary>
    /// <param name="active"></param>





    public void ExitButton(bool active)
    {
        Application.Quit();
    }

    public void RestartExperience()
    {
        SceneManager.LoadScene("MainScene");
    }
}
