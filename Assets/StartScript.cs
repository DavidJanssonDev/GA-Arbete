using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    private FloorGeneration floorGenerationScript;
    private PlayerUIScript playerUIScript;
    private PlayerValueStats playerValueStats;

    private void Awake()
    {
        floorGenerationScript = GameObject.FindGameObjectWithTag("Floor").GetComponent<FloorGeneration>();
        playerUIScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUIScript>();
        playerValueStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerValueStats>();
    }

    private void Start()
    {
        playerValueStats.AssainValues();
        playerUIScript.GeneratePlayerUI();
        floorGenerationScript.Generate();


    }
}
