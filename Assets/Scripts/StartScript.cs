using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GenerationOfFloorClassStuff;
using UIStuff;
using PlayerStats;

public class StartScript : MonoBehaviour
{
    private FloorGeneration floorGenerationScript;
    private PlayerUI _PlayerUIScript;
    [SerializeField] private GameObject _PlayerObject;
    [SerializeField] private GameObject _GameMapObject;

    private void Awake()
    {
        floorGenerationScript = _GameMapObject.GetComponent<FloorGeneration>();
        _PlayerUIScript = _PlayerObject.GetComponent<PlayerUI>();
    }

    private void Start()
    {

        floorGenerationScript.Generate();
        _PlayerUIScript.Generate();
    }

}
