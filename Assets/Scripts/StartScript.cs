using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    private FloorGeneration floorGenerationScript;
    private PlayerValueStats playerValueStats;

    private void Awake()
    {
        floorGenerationScript = GameObject.FindGameObjectWithTag("Floor").GetComponent<FloorGeneration>();
        playerValueStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerValueStats>();
    }

    private void Start()
    {

        floorGenerationScript.Generate();


    }
}
