using PetrushevskiApps.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINetworkButton_Inspector : MonoBehaviour
{
    private Button networkButton;

    private void Awake()
    {
        networkButton = GetComponent<Button>();
        networkButton.interactable = ConnectivityManager.Instance.IsConnected;
    }

    public void OnConnectivityChange(bool isConnected, string errorMsg)
    {
        networkButton.interactable = isConnected;

        if (!string.IsNullOrEmpty(errorMsg))
        {
            Debug.LogWarning(errorMsg);
        }
    }
}
