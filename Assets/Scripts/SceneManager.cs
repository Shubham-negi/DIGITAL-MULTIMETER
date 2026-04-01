using System.Collections;
using HighlightPlus;
using Unity.VisualScripting;
using Unity.VRTemplate;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [Header("Managers")]
    public UIManager uiManager;
    public SoundManager soundManager;
    public ComponentsManager componentManager;


    [Header("Light Cone")]
    public Transform softFocusLightCone;
    public int targetScale;
    public float scaleDuration = 2f;

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
        yield return new WaitForSeconds(2f);

        StartCoroutine(ScaleLightCone());

        float voiceLength = soundManager.scene1Intro.length;
        soundManager.PlayScene1Intro();

        yield return new WaitForSeconds(voiceLength);
        yield return new WaitForSeconds(2f);

        softFocusLightCone.gameObject.SetActive(false);

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

        yield return new WaitUntil(() => componentManager.AllComponentsLearned());
        UIManager.Instance.UserActionOnRAndBProbesUI(false);
        soundManager.PlayProbesConnectedVO();

        yield return WaitForAudio();


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


        componentManager.selectorDial.GetComponentInParent<XRKnob>().enabled = true;
        componentManager.selectorDial.GetComponentInParent<SphereCollider>().enabled = true;

        soundManager.PlayTurnToContinuityVO();
        UIManager.Instance.TurnToContinuityUI(true);
        //  componentManager.EnableInteraction(componentManager.selectorDial);
        yield return WaitForAudio();

        yield return new WaitUntil(() => InteractionManager.Instance.isContinuityMode);
        yield return new WaitForSeconds(2f);

        componentManager.EnableXRGrabbable(componentManager.blackProbeHolder, true);
        componentManager.EnableXRGrabbable(componentManager.redProbeHolder, true);


        soundManager.PlayTouchBothProbesVO();
        UIManager.Instance.TurnToContinuityUI(false);
        UIManager.Instance.TouchProbesTogetherUI(true);
        yield return WaitForAudio();




        yield return new WaitUntil(() => InteractionManager.Instance.AreProbesTouching());

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
        soundManager.PlayTurnTheDialToDCVO();

        yield return WaitForAudio();

        yield return new WaitUntil(() => InteractionManager.Instance.isDCMode);

        // SoundManager.Instance.PlayRotateDialTo20VO();
        // UIManager.Instance.RotateDialTo20UI(true);

        yield return new WaitUntil(() => InteractionManager.Instance.isDCMode && InteractionManager.Instance.isDcOn20V);

        yield return new WaitForSeconds(2f);

        UIManager.Instance.TurnTheDialToDCUI(false);
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
        yield return new WaitUntil(() => InteractionManager.Instance.redTouch == "BatteryMinus" && InteractionManager.Instance.blackTouch == "BatteryPlus");

        yield return new WaitForSeconds(2f);

        soundManager.PlayVoltageIsReversedInBatteryVO();
        UIManager.Instance.ObserveTheVoltageReadingUI(false);

        UIManager.Instance.VoltageIsReversedInBatteryUI(true);


        yield return WaitForAudio();

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
        soundManager.PlayMoveNearToWallSocketVO();
        yield return WaitForAudio();


        uiManager.SafetyTipsUI(true);
        soundManager.PlaySafteyTipsVO();
        yield return WaitForAudio();
        uiManager.SafetyTipsUI(false);



        uiManager.TurnDialToACUI(true);
        soundManager.PlayTurnDialToAC();
        yield return WaitForAudio();

        componentManager.EnableInteraction(componentManager.acDCSwitch.gameObject);

        yield return new WaitUntil(() => !InteractionManager.Instance.isDCMode);
        // SoundManager.Instance.PlayRotateDialTo200VO();
        // UIManager.Instance.RotateDialTo200UI(true);
        
        yield return new WaitUntil(() => !InteractionManager.Instance.isDCMode && InteractionManager.Instance.isACOn200V);

        yield return new WaitForSeconds(2f);

        uiManager.TurnDialToACUI(false);
        soundManager.PlayACVoltageChangesDirectionVO();
        yield return WaitForAudio();

        uiManager.InsertProbesInACSocketUI(true);
        componentManager.ToggleACSocketChildren(true);
        soundManager.PlayPlaceProbesOnACSocketVO();
        yield return WaitForAudio();

        yield return new WaitUntil(() => InteractionManager.Instance.redTouch == "SwitchMinus" && InteractionManager.Instance.blackTouch == "SwitchPlus");
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

        yield return new WaitUntil(() => InteractionManager.Instance.redTouch == "SwitchPlus" && InteractionManager.Instance.blackTouch == "SwitchMinus");
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
        soundManager.PlayScene5IntroVO();
        yield return WaitForAudio();

        uiManager.ConclusionUIActive(true);
        soundManager.PlayConclusionVO();
        yield return WaitForAudio();
    }

    // =========================
    // UTIL — LIGHT CONE SCALE
    // =========================
    private IEnumerator ScaleLightCone()
    {
        if (softFocusLightCone == null)
            yield break;

        Vector3 initialScale = softFocusLightCone.localScale;
        float time = 0f;

        while (time < scaleDuration)
        {
            time += Time.deltaTime;
            float t = time / scaleDuration;

            softFocusLightCone.localScale =
                Vector3.Lerp(initialScale, Vector3.one * targetScale, t);

            yield return null;
        }

        softFocusLightCone.localScale = Vector3.one * targetScale;
    }

    IEnumerator WaitForAudio()
    {
        yield return new WaitUntil(() => !SoundManager.Instance.IsPlaying());
    }

}
