using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValueStats : MonoBehaviour
{
    public int Health = 10;
    public float MovementSpeed = 5f;
    

    public List<Text> playerStatsText = new();

    public void GetPlayerStatsRefrences()
    {
        var playerTextObject = GameObject.FindGameObjectsWithTag("Player Stats Text");
        foreach (var TextObject in playerTextObject) 
        { 
            playerStatsText.Add(TextObject.GetComponent<Text>());
        }
    }

    public void UpdatePlayerStats()
    {

    }
}
