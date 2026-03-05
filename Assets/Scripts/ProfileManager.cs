using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Mathematics;

public class ProfileManager : MonoBehaviour {
    [Header("Ez UI Panels")]
    public GameObject menuPanel;
    public GameObject createPanel;
    public GameObject selectPanel;
    public GameObject settingsPanel;
    public GameObject confirmDeletePanel;

    [Header("Create Profile UI")]
    public TMP_InputField playerNameInput;
    public Button confirmCreateButton;

    [Header("Select Profile UI")]
    public TMP_Dropdown profileDropdown;


    ProfileData selectedProfile;

    void Start()
    {
        //Important to refresh shit
        RefreshProfileDropdown();
        //Double disable Just incase
        confirmCreateButton.interactable = false;
    }


    //We connect most of these methods into the buttons and this is easy because they have those bindable things in unity
    #region UI Menu
    public void OpenCreateProfile()
    {
        menuPanel.SetActive(false);
        createPanel.SetActive(true);
    }

    public void OpenSelectProfile()
    {
        menuPanel.SetActive(false);
        selectPanel.SetActive(true);
        RefreshProfileDropdown();
    }
    #endregion

    #region UI Create Profile

    //Use on the idk
    public void OnplayerNameChanged()
    {
        confirmCreateButton.interactable = !string.IsNullOrEmpty(playerNameInput.text);
    }

    /// <summary>
    /// We make a brand new class object and get the data that has been chosen or input
    /// We take all of the shit and then we save it in the profiles class list (Not to confuse with profile, that is its own single thing with no list or anything I don't know why I keep seing it as profile, whatever)
    /// And that's basically it 
    /// </summary>
    public void ConfirmCreateProfile()
    {
        ProfileData profile = new ProfileData
        {
            playerName = playerNameInput.text,
            Hearts = 3f,
            Money = 0f,
            PlayerPosition = Vector2.zero
        };
        //We add the profile to the profiles list thats in the Save Manager
        SaveManager.instance.profiles.Add(profile);
        //We fire the method from saveManager
        SaveManager.instance.SaveProfiles();

        //We return to the menu so that we can select our brand newly made profile
        ReturnToMenu();
    }
    #endregion


    #region Select Profile UI
    void RefreshProfileDropdown()
    {
        //We clear the shit first (This is because we need to clear any past profiles that might of been deleted and if we don't delete them we will get an error) then we gather all of the profiles.
        profileDropdown.ClearOptions();

        //Wow we using dynamic shit now!!!!
        //But yeah var makes it easy to get the type of data that we are looking for
        //We add new data (Profiles) to the dropdown menu
        foreach (var profile in SaveManager.instance.profiles)
            profileDropdown.options.Add(new TMP_Dropdown.OptionData(profile.playerName));

        //According to unity Documentation "If you have modified the list of options, you should call this method afterwards to ensure that the visual state of the dropdown corresponds to the updated options."
        profileDropdown.RefreshShownValue();

        //Summary
        //Delete all, Add profiles, Refresh so they popup
    }

    public void SelectProfile()
    {
        //Get the index value of the selected thing
        int index = profileDropdown.value;
        //Find the index so we can select a profile (Ez reference incase of deletion)
        //We add a reference of what data we selected to saveManager
        SaveManager.instance.currentProfile =
        selectedProfile = SaveManager.instance.profiles[index];

        selectPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    //public void SelectProfileBack()
    //{
    //   selectPanel.SetActive(false);

    //}
    #endregion


    #region Profile Settings Panel

    public void ConfirmDeletePanel()
    {
        settingsPanel.SetActive(false);
        confirmDeletePanel.SetActive(true);
    }
    public void ConfirmDeleteBack()
    {
        confirmDeletePanel.SetActive(true);
        confirmDeletePanel.SetActive(false);
    }
    public void DeleteProfile()
    {
        //Whatever we selected before we just get it and we can delete it from the list
        SaveManager.instance.profiles.Remove(selectedProfile);
        SaveManager.instance.SaveProfiles();
        //Obviously we head back to the menu so we can choose whatever option
        ReturnToMenu();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        Debug.Log(selectedProfile.playerName);
    }

    public void ReturnToMenu()
    {
        //Disable all other panels except menu (It's all a loop)
        createPanel.SetActive(false);
        selectPanel.SetActive(false);
        settingsPanel.SetActive(false);
        confirmDeletePanel.SetActive(false);
        menuPanel.SetActive(true);

    }
    #endregion
}