using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private PlayerValueStats _PlayerValueStats;
    public GameObject GameOverObject;

    private void Awake()
    {
        _PlayerValueStats = GetComponent<PlayerValueStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_PlayerValueStats.Health <= 0)
        {
            _PlayerValueStats.GameOver = true;
            GameOverObject.SetActive(true);
        }
        else
        { 
            _PlayerValueStats.GameOver = false;
            GameOverObject.SetActive(false);
        }
    }
}
