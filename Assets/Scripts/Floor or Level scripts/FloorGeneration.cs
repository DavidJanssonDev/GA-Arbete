using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorGeneration : MonoBehaviour {

    private FloorValueScript floorValueScript;
    private CopyTileMapToMain tileCopyScript;

    private Transform MainWallTilemap;
    private Transform MainGroundTilemap;



    private void Awake() {


        floorValueScript = GetComponent<FloorValueScript>();
        tileCopyScript = GetComponent<CopyTileMapToMain>();
    }

    private void Start() {

        tileCopyScript.ImportRooms(MainGroundTilemap, MainWallTilemap); // Takes in the Rooms and seperates them
        
        // Goes throw each room 
        foreach (var room in floorValueScript.RoomList) {
            room.CopyTileMap()
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
        public Transform RoomTransform; //array [x,y]
        public List<Vector3> DoorPos;
        public bool CanContainEnemies;
        public List<Tilemap> Tilemaps;
    

        // CUNSTRUCTOR
        public Room(string name, Transform transform, bool canContainEnemies){
            Name = name;
            RoomTransform = transform;
            CanContainEnemies = canContainEnemies;
            foreach (Transform child in transform) {

                if (child.CompareTag("Wall tilemap")) {
                    
                   Tilemaps.Add(child.GetComponent<Tilemap>());

                } else if (child.CompareTag("Ground tilemap")) {

                   Tilemaps.Add(child.GetComponent<Tilemap>());

                }
            }
        }

        public float GetDistanceToRoom(Transform targetPoint) {
            return Vector3.Distance(RoomTransform.position, targetPoint.position);
        }
        


        public void CopyTileMap(List<Tilemap> Maintilemaps) {
            foreach(var tilemap in  Maintilemaps)
            {
                switch (tilemap.transform.tag) {
                    case "Main wall tilemap":
                        CopysTileMapToTilemap(tilemap, tilemap);
                        break;

                    case "Main ground tilemap":
                        CopysTileMapToTilemap(tilemap, tilemap);
                        break;

                }
            }
        }

        private void CopysTileMapToTilemap(Tilemap tilemapToCopyTo, Tilemap orgTilemap) {

            var tilemapPos = orgTilemap.transform.position;
            var bounds = orgTilemap.cellBounds;

            for (int tileX = bounds.x; tileX < bounds.x + bounds.size.x; tileX++)
            {
                for (int tileY = bounds.y; tileY < bounds.y + bounds.size.y; tileY++)
                {


                    var cellPosition = new Vector3Int(tileX, tileY, 0);
                    var sourceTile = orgTilemap.GetTile(cellPosition);

                    if (sourceTile != null)
                    {

                        tilemapToCopyTo.SetTile(
                                new Vector3Int(
                                    Mathf.FloorToInt(tilemapPos.x) + cellPosition.x,
                                    Mathf.FloorToInt(tilemapPos.y) + cellPosition.y,
                                    Mathf.FloorToInt(tilemapPos.z) + cellPosition.z),
                                sourceTile);
                    }
                }
            }
        }

    }

}