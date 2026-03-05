using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SaveManager : MonoBehaviour {
    public ProfileManager profileManager;
    //One instance thing that can be changed anywhere and it isn't affected by reloading scenes (Ez for saving and such)
    public static SaveManager instance;

    public ProfileData currentProfile;

    //All profiles list
    public List<ProfileData> profiles = new List<ProfileData>();

    //To keep track of times
    public List<float> leaderboardTimes = new List<float>();

    //Save stuff later
    string folderPath;
    string savePath;

    private void Awake() //First thing that executes
    {
        //Instace is created and if null then whatever is inside the class it becomes itself, else die
        if (instance == null)
        {
            instance = this;
            //Don't destroy the object because we are gonna need this whole thing for sharing data and then saving it
            DontDestroyOnLoad(gameObject);


            //Application.datapath is your assets, and then we select a folder
            folderPath = Path.Combine(Application.dataPath, "UserData");

            savePath = Path.Combine(folderPath, "profiles.json");
            LoadProfiles();
        }
        else
        {
            Destroy(gameObject);
        }
    }



    [System.Serializable]
    public class ProfileListClass {
        public List<ProfileData> profiles;
    }

    public void SaveProfiles()
    {
        string json = JsonUtility.ToJson(new ProfileListClass { profiles = profiles }, true);
        File.WriteAllText(savePath, json);
        Debug.Log(savePath);
    }

    [ContextMenu("JSON Load")]
    public void LoadProfiles()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            profiles = JsonUtility.FromJson<ProfileListClass>(json).profiles;
        }

        else
        {
            Debug.LogError("Could not find save file path");
        }
    }
}