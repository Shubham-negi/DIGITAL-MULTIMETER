using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{    public static UIManager Instance;

    public GameObject beginUI;
    public GameObject componentsUI;

    public GameObject clickEachComponentText;

    public GameObject userActionOnRAndBProbes;

    
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
   public void ClickEachComponentTextUI( bool active)
    {
        clickEachComponentText.SetActive(active);
    }
    public void  UserActionOnRAndBProbesUI( bool active)
    {
        userActionOnRAndBProbes.SetActive(active);
    }
   
    public void ShowComponentsUI( bool active)
    {
        componentsUI.SetActive(active);
    }

  
}
