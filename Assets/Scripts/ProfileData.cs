using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ProfileData {

    //public string vehicleType;
    //public string vehicleColor;
    //public float bestTime;

    public string playerName;
    public float Hearts;
    public float Money;
    public Vector3 PlayerPosition;
    public List<string> collectedCoinIDs = new List<string>();

    public void AddCoin(string coinID)
    {
        if (!collectedCoinIDs.Contains(coinID))
        {
            collectedCoinIDs.Add(coinID);
        }
    }

}


