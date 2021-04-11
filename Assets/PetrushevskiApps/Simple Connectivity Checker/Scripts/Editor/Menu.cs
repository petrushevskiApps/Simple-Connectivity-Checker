using UnityEditor;
using UnityEngine;

namespace PetrushevskiApps.Utilities
{
    public class Menu : MonoBehaviour
    {
        [MenuItem("Tools/Connectivity Checker/Add ConnectivityManager", false, 1)]
        static void AddConnectivityManager()
        {
            GameObject go = FindObjectOfType<ConnectivityManager>()?.gameObject;

            if (go != null)
            {
                Debug.LogWarning("ConnectivityManager already exists in scene");
                Selection.activeObject = go;
                return;
            }

            // Create a custom game object
            go = new GameObject("Custom Game Object");
            go.AddComponent<ConnectivityManager>();
            go.name = "ConnectivityManager";

            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }

        [MenuItem("Tools/Connectivity Checker/Toggle Connectivity in Editor", false, 2)]
        private static void ToggleConnectivity()
        {
            ConnectivityManager.Instance.IsConnectedDebugToggle = !ConnectivityManager.Instance.IsConnectedDebugToggle;
        }

    }

}

