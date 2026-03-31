using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;

    private string redTouch;
    private string blackTouch;

    public TextMeshProUGUI voltageDisplay;

    private string lastValue = ""; // prevent spam updates

    void Awake()
    {
        Instance = this;
    }

    public void UpdateProbe(Probe probe, Collider other)
    {
        // 🔹 Update touch state
        if (probe.probeType == Probe.ProbeType.Red)
            redTouch = probe.currentTouch;
        else
            blackTouch = probe.currentTouch;

        // 🔹 Check probe-to-probe contact FIRST
        if (AreProbesTouching())
        {
            Show("ON");
            SoundManager.Instance.PlayBeep();
            Debug.Log("Circuit is working (Probes touching)");
            return;
        }

        // 🔹 Otherwise check battery
        CheckPowerSources();
    }

    bool AreProbesTouching()
    {
        // If both probes are touching each other
        return redTouch == "Black Probe head" && blackTouch == "Red Probe head";
    }

   void CheckPowerSources()
{
    // 🔋 Battery
    if (redTouch == "BatteryPlus" && blackTouch == "BatteryMinus")
    {
        Show("+9V");
    }
    else if (redTouch == "BatteryMinus" && blackTouch == "BatteryPlus")
    {
        Show("-9V");
    }

    // 🔌 Switchboard
    else if (redTouch == "SwitchPlus" && blackTouch == "SwitchMinus")
    {
        Show("+220V"); // or whatever value you want
    }
    else if (redTouch == "SwitchMinus" && blackTouch == "SwitchPlus")
    {
        Show("-220V");
    }
    else if (redTouch == "SwitchPlus (1)" && blackTouch == "SwitchMinus (1)")
    {
        Show("+220V"); // or whatever value you want
    }
    else if (redTouch == "SwitchMinus (1)" && blackTouch == "SwitchPlus (1)")
    {
        Show("-220V");
    }

    // ❌ Default
    else
    {
        Show("0");
    }
}
    void Show(string value)
    {
        if (lastValue == value) return; // prevent flicker / spam

        lastValue = value;
        voltageDisplay.text = value;
        Debug.Log("Voltage: " + value);
    }
}