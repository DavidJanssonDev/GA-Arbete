using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RoomStuff;

public class CopyTileMapToMain : MonoBehaviour
{
    private FloorValueScript valueScript;

    private void Awake() { 
        valueScript = GetComponent<FloorValueScript>();
    }

    

}