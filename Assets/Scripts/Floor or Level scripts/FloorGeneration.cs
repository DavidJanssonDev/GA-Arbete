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
        roomGeneration();




    }

    private void roomGeneration() {
        List<Transform> roomList = _FloorValueScript.RoomGameObjects;
        for (var roomIndex = 0; roomIndex < roomList.Count; roomIndex++) {
            roomInfoScript roomScript = roomList[roomIndex].GetComponent<roomInfoScript>();
            roomScript.roomStartUp();
            roomScript.GetClosestRooms(_FloorValueScript.RoomGameObjects);
        }
    }
}
