using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGeneration : MonoBehaviour {

    private FloorValueScript _FloorValueScript;
    private CopyTileMapToMain _TileCopyScript;


    private void Awake()
    {
        _FloorValueScript = GetComponent<FloorValueScript>();
        _TileCopyScript = GetComponent<CopyTileMapToMain>();
    }

    private void Start() {
        _TileCopyScript.ImportRooms();
        _TileCopyScript.SortTileMapsInRoom();
        _TileCopyScript.CopyTileMap();
    }
}
