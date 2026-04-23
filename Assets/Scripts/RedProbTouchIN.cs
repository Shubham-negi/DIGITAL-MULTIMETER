using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProbTouchIN : MonoBehaviour
{
    // Start is called before the first frame update
    public Activity2Manager activity2Manager;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "BatteryPositive")
        {

            activity2Manager.CheckPowerSource();
        }

        if (other.name == "SwitchPositive")
        {

            activity2Manager.FollowTheFlow();
        }

        if (other.name == "SwitchNegative")
        {

            activity2Manager.TheCriticalCheck();
        }


        
    }
}
