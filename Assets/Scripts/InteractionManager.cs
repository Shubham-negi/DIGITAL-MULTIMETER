using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;

    public string redTouch;
    public string blackTouch;

    public TextMeshProUGUI voltageDisplay;

    private string lastValue = ""; // prevent spam updates

    // =========================
    // FLAGS
    // =========================
    public bool isContinuityMode = false;
    public bool isDCMode = false;

    void Awake()
    {
        Instance = this;
    }

    // =========================
    // XR KNOB TRACKING
    // =========================
    public void UpdateDialValue(float value)
    {

       // SoundManager.Instance.PlaySlideSwitch();
        // 🔹 Continuity Mode
        if (value >= 0.735f && value <= 0.775f)
        {
            isContinuityMode = true;
        }
        else
        {
            isContinuityMode = false;
        }

        // 🔹 DC Mode (example range — adjust as per your dial)
        // if (value >= 0.50f && value <= 0.65f)
        // {
        //     isDCMode = true;
        // }
        // else
        // {
        //     isDCMode = false;
        // }

        Debug.Log($"Dial Value: {value} | Continuity: {isContinuityMode} | DC Mode: {isDCMode}");
    }

    // =========================
    // MAIN INTERACTION
    // =========================
    public void UpdateProbe(Probe probe, Collider other)
    {
        // ❌ Block everything if not in continuity mode
        if (!isContinuityMode)
        {
            Show("0");
            return;
        }

        // 🔹 Update touch state
        if (probe.probeType == Probe.ProbeType.Red)
            redTouch = probe.currentTouch;
        else
            blackTouch = probe.currentTouch;

        // 🔹 Probe-to-probe contact
        if (AreProbesTouching())
        {
            Show("ON");
            SoundManager.Instance.PlayBeep();
            Debug.Log("Circuit is working (Probes touching)");
            return;
        }

        // 🔹 Power source check
        CheckPowerSources();
    }

    // =========================
    // CHECKS
    // =========================
    public bool AreProbesTouching()
    {
        return redTouch == "Black Probe head" && blackTouch == "Red Probe head";
    }

    void CheckPowerSources()
    {
        if (isDCMode)
        {
            CheckDC();   // 🔋 Battery
        }
        else
        {
            CheckAC();   // 🔌 Switchboard
        }
    }

    // =========================
    // 🔋 DC (Battery)
    // =========================
    void CheckDC()
    {
        if (redTouch == "BatteryPlus" && blackTouch == "BatteryMinus")
        {
            SoundManager.Instance.PlayRedAndBlackIsConnectedVO();
            Show("+9V");
        }
        else if (redTouch == "BatteryMinus" && blackTouch == "BatteryPlus")
        {
            Show("-9V");
            SoundManager.Instance.PlayVoltageIsReversedInBatteryVO();
        }
        else
        {
            Show("0");
        }
    }

    // =========================
    // 🔌 AC (Switchboard)
    // =========================
    void CheckAC()
    {
        if (redTouch == "SwitchPlus" && blackTouch == "SwitchMinus")
        {
            Show("220V");

        }
        if (redTouch == "SwitchMinus" && blackTouch == "SwitchPlus")

        {
            Show("220V");
        }

    }

    // =========================
    // DISPLAY
    // =========================
    void Show(string value)
    {
        if (lastValue == value) return;

        lastValue = value;
        voltageDisplay.text = value;
        Debug.Log("Voltage: " + value);
    }
}