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

    public bool isDcOn20V = false;
    public bool isACOn200V = false;


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

        // Reset all states first (important)
        isContinuityMode = false;
        isACOn200V = false;
        isDcOn20V = false;

        // 🔹 Continuity Mode
        if (value >= 0.73f && value <= 0.78f)
        {
            isContinuityMode = true;
        }
        // 🔹 DC 20V
        else if (value >= 0.61f && value <= 0.64f)
        {
            isDcOn20V = true;
        }
        // 🔹 AC 200V
        else if (value >= 0.56f && value <= 0.6f)
        {
            isACOn200V = true;
        }

       // Debug.Log($"Dial Value: {value} | Continuity: {isContinuityMode} | DC20V: {isDcOn20V} | AC200V: {isACOn200V}");
    }

    // =========================
    // MAIN INTERACTION
    // =========================
    public void UpdateProbe(Probe probe, Collider other)
    {
        // ❌ Block everything if not in continuity mode
       
        // 🔹 Update touch state
        if (probe.probeType == Probe.ProbeType.Red)
            redTouch = probe.currentTouch;
        else
            blackTouch = probe.currentTouch;

        // 🔹 Probe-to-probe contact
        if (AreProbesTouching())
        {
            Show("ON");
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
            Show("+9V");
        }
        else if (redTouch == "BatteryMinus" && blackTouch == "BatteryPlus")
        {
            Show("-9V");
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
       else if (redTouch == "SwitchMinus" && blackTouch == "SwitchPlus")

        {
            Show("220V");
        }
         else
        {
            Show("0");
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