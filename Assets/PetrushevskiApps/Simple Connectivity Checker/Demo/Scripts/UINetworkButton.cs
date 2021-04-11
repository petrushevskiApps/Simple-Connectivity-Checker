using PetrushevskiApps.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace MyNamespace
{
    
}
public class UINetworkButton : MonoBehaviour
{
    private Button networkButton;

    private void Awake()
    {
        ConnectivityManager.Instance.AddConnectivityListener(OnConnectivityChange);

        networkButton = GetComponent<Button>();
        networkButton.interactable = ConnectivityManager.Instance.IsConnected;
    }
    private void OnDestroy()
    {
        ConnectivityManager.Instance.RemoveConnectivityListener(OnConnectivityChange);
    }
    private void OnConnectivityChange(bool isConnected, string errorMsg)
    {
        networkButton.interactable = isConnected;
        
        if(!string.IsNullOrEmpty(errorMsg))
        {
            Debug.LogWarning(errorMsg);
        }
    }
}
