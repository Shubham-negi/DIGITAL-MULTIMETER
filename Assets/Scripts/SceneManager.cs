using System.Collections;
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
        // componentManager.AllComponentsClick(true);
        //componentManager.AllComponentsHighlightEffect(true);





        // Wait until all components are clicked
        yield return new WaitUntil(() => componentManager.AllComponentsLearned());
        UIManager.Instance.UserActionOnRAndBProbesUI(false);
        soundManager.PlayProbesConnectedVO();


        // Next step (example)
        // uiManager.ShowInstruction("Connect the probes correctly");
        //soundManager.PlayConnectProbes();
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
}
