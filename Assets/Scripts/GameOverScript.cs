using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private PlayerValueStats PlayerValueStats;
    public GameObject GameOverObject;

    private void Awake()
    {
        PlayerValueStats = GetComponent<PlayerValueStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerValueStats.Health <= 0)
        {
            PlayerValueStats.GameOver = true;
            GameOverObject.SetActive(true);
        }
        else
        { 
            PlayerValueStats.GameOver = false;
            GameOverObject.SetActive(false);
        }
    }
}
