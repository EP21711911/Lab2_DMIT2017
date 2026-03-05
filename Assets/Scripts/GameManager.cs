using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Header("UI Objects")]
    [SerializeField] private TextMeshProUGUI Coins;
    ProfileData profile;
    //[Header("Death Stuff")]
    [SerializeField] private Transform heartContainer;

    [Tooltip("The disabled heart object we want to clone")]
    [SerializeField] private GameObject heartTemplate;
    [Header("Player")]
    [SerializeField] private GameObject player;


    private Vector3 startPosition;
    private void Start()
    {
        startPosition = new Vector3(-0.990442932f, 0.175058454f, 0f);
        profile = SaveManager.instance.currentProfile;
        ExtraLogic(profile);
        if (profile != null)
        {
            Debug.Log($"Loaded Profile {profile.playerName}");
            Debug.Log($"Loaded Profile {profile.Hearts}");
            Debug.Log($"Loaded Profile {profile.Money}");
        }
        else
        {
            Debug.LogWarning("Profile is NUll");
        }
        // Start the race
        //RaceTimer.Instance.StartTimer();
    }

    void ExtraLogic(ProfileData profile)
    {
        if (profile == null)
        {
            Debug.LogError("Profile is NUll");
            return;
        }

        Coins.text = ($"{profile.Money.ToString()}");
        DrawHearts(profile.Hearts);
        Transform playerT = player.GetComponent<Transform>();
        playerT.position = profile.PlayerPosition;
    }

    public void DrawHearts(float currentHealth)
    {
        // Clear out the old hearts first (so we don't just infinitely add more)
        foreach (Transform child in heartContainer)
        {
            // We check to make sure we don't destroy our hidden template
            if (child.gameObject != heartTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        // Loop through the player's current health number
        for (int i = 0; i < currentHealth; i++)
        {
            // Clone the hidden template and place it inside the container
            GameObject newHeart = Instantiate(heartTemplate, heartContainer);

            // Turn the new cloned heart on so we can see it
            newHeart.SetActive(true);
        }
    }

    public void Update()
    {
        if (Coins.text == null)
        {
            return;
        }
        Coins.text = ($" A: {profile.Money.ToString()}");
        DrawHearts(profile.Hearts);
        if (profile.Hearts <= 0)
        {
            Death();
        }
    }

    public void ToMenu()
    {
        SaveManager.instance.SaveProfiles();
        SceneManager.LoadScene("Menu");
    }
    private void Death()
    {
        profile.Hearts = 1;
        profile.PlayerPosition = startPosition;
        SaveManager.instance.SaveProfiles();
        SceneManager.LoadScene("Menu");
    }
    void ApplyVehicleColor(GameObject vehicle, string colorName)
    {
        //Material material = White;
        //switch (colorName)
        //{
        //    case "Red": material = Red; break;
        //    case "Orange": material = Orange; break;
        //    case "Yellow": material = Yellow; break;
        //    case "Green": material = Green; break;
        //    case "Blue": material = Blue; break;
        //}

        //Renderer renderer = vehicle.GetComponentInChildren<Renderer>();
        //renderer.material = material;
    }
}
