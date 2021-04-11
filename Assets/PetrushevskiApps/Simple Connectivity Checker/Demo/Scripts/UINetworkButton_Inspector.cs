using PetrushevskiApps.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace PetrushevskiApps.Demo
{
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
}


