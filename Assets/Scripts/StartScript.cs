using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenerationOfFloorClassStuff;
using UIStuff;

public class StartScript : MonoBehaviour
{
    private FloorGeneration FloorGenerationScript;
    private PlayerUI PlayerUIScript;

    [SerializeField] private GameObject _PlayerObject;
    [SerializeField] private GameObject _GameMapObject;

    private void Awake()
    {
        PlayerUIScript = _PlayerObject.GetComponent<PlayerUI>();
        FloorGenerationScript = _GameMapObject.GetComponent<FloorGeneration>();
    }

    private void Start()
    {

        PlayerUIScript.GenerateTextMeshDictanry();
        FloorGenerationScript.Generate();
        
    }

}
