using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace PetrushevskiApps.Utilities
{
    [DefaultExecutionOrder(-50)]
    public class ConnectivityManager : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Enable to print debug messages in console. Disable for production builds to improve performance.")]
        [SerializeField] private bool printDebugMessages;
        
        [Tooltip("Start checking connectivity on awake.")]
        [SerializeField] private bool StartOnAwake = true;


        [Header("Ping parameters")]
        [Tooltip("Ping this url to test connection. Recommendation: Use link to your own website page with simple text.")]
        [SerializeField] private string pingUrl = "http://google.com";

        [Tooltip("Set how often is connection tested in seconds.")]
        [SerializeField] private float pingInterval = 5f;

        [Header("Events")]        
        [SerializeField] private UnityConnectivityEvent OnConnectivityChange = new UnityConnectivityEvent();
        
        public static ConnectivityManager Instance;

        private UnityWebRequest webRequest;
        private bool isConnected;
        private Coroutine connectionTestCoroutine;
        private bool isConnectedDebugToggle = true;

        public bool IsTestingConnectivity { get; private set; } = false;
        
        public bool IsConnectedDebugToggle
        {
            get
            {
                return isConnectedDebugToggle;
            }
            set
            {
                if(Application.isEditor)
                {
                    isConnectedDebugToggle = value;
                }
                else
                {
                    isConnectedDebugToggle = true;
                }
            }
        }
        
        /// <summary>
        /// Check if there is network connection.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            private set
            {
                if (isConnected == value) return;
                isConnected = value;
                OnConnectivityChange.Invoke(isConnected, webRequest.error);
                PrintDebugMessage($"Is Connected:: {isConnected}");
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(Instance.gameObject);

                if (StartOnAwake) StartConnectionCheck();
            }
        }

        /// <summary>
        /// Register to connection status changes event.
        /// </summary>
        /// <param name="onConnectionStatusChange">
        /// Unity Action which will be called when connection change is detected.
        /// </param>
        public void AddConnectivityListener(UnityAction<bool, string> onConnectionStatusChange)
        {
            OnConnectivityChange.AddListener(onConnectionStatusChange);
        }

        /// <summary>
        /// Unregister from connection status changes event.
        /// </summary>
        /// <param name="onConnectionStatusChange">
        /// Unity Action which was registered on connection status change event.
        /// </param>
        public void RemoveConnectivityListener(UnityAction<bool, string> onConnectionStatusChange)
        {
            OnConnectivityChange.RemoveListener(onConnectionStatusChange);
        }

        /// <summary>
        /// Start testing connectivity.
        /// </summary>
        public void StartConnectionCheck()
        {
            if (connectionTestCoroutine == null)
            {
                connectionTestCoroutine = StartCoroutine(TestConnection());
                IsTestingConnectivity = true;
            }
            else
            {
                PrintDebugMessage("Connection check already started!");
            }
        }

        /// <summary>
        /// Stop testing connectivity when is not needed. 
        /// </summary>
        public void StopConnectionCheck()
        {
            if (connectionTestCoroutine != null)
            {
                StopCoroutine(connectionTestCoroutine);
                connectionTestCoroutine = null;
                IsTestingConnectivity = false;
            }
            else
            {
                PrintDebugMessage("No active Connection check!");
            }
        }

        private IEnumerator TestConnection()
        {
            while (true)
            {
                webRequest = new UnityWebRequest(pingUrl);
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    if(Application.isEditor)
                    {
                        IsConnected = IsConnectedDebugToggle;
                    }
                    else
                    {
                        IsConnected = true;
                    }
                }
                else if (webRequest.result != UnityWebRequest.Result.InProgress)
                {
                    IsConnected = false;
                }
                
                yield return new WaitForSeconds(pingInterval);
            }
        }

        private void PrintDebugMessage(string msg)
        {
            if (printDebugMessages)
            {
                Debug.Log($"ConnectivityManager:: {msg}");
            }
        }


    }

    [Serializable] public class UnityConnectivityEvent : UnityEvent<bool, string> { }
}
