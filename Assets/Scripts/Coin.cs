using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    ProfileData profile;
    [Tooltip("Assign ID Manually (Later do something automatic)")]
    [SerializeField] private string coinID;

    private void Start()
    {
        profile = SaveManager.instance.currentProfile;

        if (profile != null)
        {
            if (profile.collectedCoinIDs.Contains(coinID))
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (profile != null)
            {
                profile.AddCoin(coinID);
            }
            profile.Money = profile.Money += 1;
            SaveManager.instance.SaveProfiles();
            Destroy(gameObject);
        }
    }


}
