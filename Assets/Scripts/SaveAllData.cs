using System.Collections.Generic;
using UnityEngine;

public class SaveAllData : MonoBehaviour
{
    ProfileData profile;
    [SerializeField] private GameObject player;
    private Transform playerTransform;
    private void Start()
    {
        profile = SaveManager.instance.currentProfile;
        playerTransform = player.GetComponent<Transform>();
    }

    public void SaveGame()
    {
        if (profile != null)
        {
            profile.PlayerPosition = playerTransform.position;
            SaveManager.instance.SaveProfiles();
        }
        else
        {
            Debug.LogWarning("Profile is null, you need to load a profile; go into profile scene");
        }



    }

}
