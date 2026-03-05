using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    // Function to load a specific scene by its build index
    public void LoadMenu()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadProfile()
    {
        SceneManager.LoadScene("Profile");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
