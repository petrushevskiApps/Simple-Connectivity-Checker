using PetrushevskiApps.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace PetrushevskiApps.Demo
{
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
                btnText.text = "Stop testing connectivity \n ( State ON )";
            }
            else
            {
                btnText.text = "Start testing connectivity \n ( State OFF )";
            }
        }

        private void ToggleConnectivityCheck()
        {
            if (ConnectivityManager.Instance.IsTestingConnectivity)
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

}

