using PetrushevskiApps.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConnectivityToggle : MonoBehaviour
{
    [SerializeField] private Text btnText;
    private Button checkToggle;
    
    private void Awake()
    {
        checkToggle = GetComponent<Button>();
        checkToggle.onClick.AddListener(ToggleConnectivityCheck);

        SetButtonText();
    }

    private void SetButtonText()
    {
        if (ConnectivityManager.Instance.IsTestingConnectivity)
        {
            btnText.text = "Testing Connectivity ON";
        }
        else
        {
            btnText.text = "Testing Connectivity OFF";
        }
    }

    private void ToggleConnectivityCheck()
    {
        if(ConnectivityManager.Instance.IsTestingConnectivity)
        {
            ConnectivityManager.Instance.StopConnectionCheck();
        }
        else
        {
            ConnectivityManager.Instance.StartConnectionCheck();
        }
        SetButtonText();
    }
}
