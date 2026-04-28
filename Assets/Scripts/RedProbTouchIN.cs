using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProbTouchIN : MonoBehaviour
{
    public Activity2Manager activity2Manager;

    private bool batteryTouched = false;
    private bool switchPositiveTouched = false;
    private bool switchNegativeTouched = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "BatteryPositive" && !batteryTouched)
        {
            batteryTouched = true;
            activity2Manager.CheckPowerSource();
        }

        else if (other.name == "SwitchPositive" && !switchPositiveTouched)
        {
            switchPositiveTouched = true;
            activity2Manager.FollowTheFlow();
        }

        else if (other.name == "SwitchNegative" && !switchNegativeTouched)
        {
            switchNegativeTouched = true;
            activity2Manager.TheCriticalCheck();
        }
        else if (other.name == "New Working Switch")
        {
            activity2Manager.FaultySwitchReplaced();
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
    }
}