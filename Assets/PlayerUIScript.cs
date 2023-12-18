using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUIScript : MonoBehaviour
{
    private PlayerValueStats playerValueStats;


    private void Awake()
    {
        playerValueStats = GetComponent<PlayerValueStats>();
    }

    public void GeneratePlayerUI()
    {
        GetPlayerStatsText();




    }

    public void GetPlayerStatsText()
    {
        var playerTextObject = GameObject.FindGameObjectsWithTag("Player Stats Text");
       
        foreach (var TextObject in playerTextObject) 
        {
            playerValueStats.playerStatsTextObjects.Add(TextObject);
        }
    }

    public void AssignPlayerUI()
    {
        foreach (var TextObject in playerValueStats.playerStatsTextObjects)
        {
            var renderScript = TextObject.GetComponent<TextUIRender>();
            
        }

    }


}
