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

        _TileCopyScript.ImportRooms(); // Takes in the Rooms and seperates them
        
        foreach (var room in _FloorValueScript.RoomList) {
            Debug.Log(room.roomTransform.position);
        }
        

        
        // roomGeneration();
        // _TileCopyScript.CopyTileMap();
    }


   
    /*
    private void roomGeneration() {
        
        List<Transform> roomList = _FloorValueScript.RoomGameObjects;
        
        // Goes throw the 
        for (var roomIndex = 0; roomIndex < roomList.Count; roomIndex++) {
            Debug.Log($"ROOM SEARCHING: ROOM_{roomIndex}");           
            roomInfoScript roomScript = roomList[roomIndex].GetComponent<roomInfoScript>();
            roomScript.roomStartUp();
            roomScript.GetClosestRooms(_FloorValueScript.RoomGameObjects);
        }
    }
    */
   
}

namespace RoomStuff
{
    public class Room {


        public string Name;
        public Transform roomTransform; //array [x,y]
        public List<Vector3> DoorPos;
        public bool CanContainEnemies;

        // CUNSTRUCTOR
        public Room(string name, Transform transform, bool canContainEnemies){
            Name = name;
            roomTransform = transform;
            CanContainEnemies = canContainEnemies;
        }

        public float GetDistanceToRoom(Transform targetPoint) {
            return Vector3.Distance(roomTransform.position, targetPoint.position);
        }
    }
}