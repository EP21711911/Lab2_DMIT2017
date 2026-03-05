using UnityEditor;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting application...");

        // Quit the application
//#if UNITY_EDITOR
//        // Exits Play mode in the Unity Editor
//        EditorApplication.isPlaying = false;
//#else
//            Application.Quit();
//#endif
    }
}
