using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class roomInfoScript : MonoBehaviour {

    private FloorGeneration floorGeneration;

    // THE LIST WITH THE CLOSEST ROOM ARE STORED
    public List<Dictionary<string, Transform>> closestRoomList = new();

    public List<float> roomLengthToOtherRoom = new(); // The index is the index of the room list

    public List<Transform> roomChildren = new();
    
    public List<Vector3> roomDoorPos = new();

    public int roomDoorAvailebole;
    
    private Tilemap floorTilemap = null;

    [SerializeField] private TileBase doorTargetTile;

    private void Awake()
    {
        floorGeneration = GetComponent<FloorGeneration>();
    }

    public void roomStartUp() {
        GetChildrenInRoom();
        GetGroundTileMap();
        GetDoorTiles();
    }

    public void GetChildrenInRoom()
    {
        int childCount = transform.childCount;

        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            Transform childObject = transform.GetChild(childIndex);
            childObject.name = $"{transform.name} : {childObject.name}";
            roomChildren.Add(childObject);
        }
    }
    
    public void GetGroundTileMap() {
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++) {
            if (transform.GetChild(childIndex).CompareTag("Ground tilemap")) {
                floorTilemap = transform.GetChild(childIndex).GetComponent<Tilemap>();
            }
        }
    }

    public void GetDoorTiles() {

        BoundsInt bounds = floorTilemap.cellBounds;

        foreach(var tilePosition in bounds.allPositionsWithin) { 

            if (floorTilemap.GetTile(tilePosition) == doorTargetTile) {

                roomDoorPos.Add(floorTilemap.GetCellCenterWorld(tilePosition));
                roomDoorAvailebole++;
            }
        }
    }


    public void GetClosestRooms(List<Transform> roomList) {

        roomDoorAvailebole = roomDoorPos.Count;
        float closestdist = 0.0f;
        Transform closestRoom = null;

        foreach (var room in roomList) {

            Debug.Log($"CURRENT ROOM THAT ARE SEARCHING {transform.name}, THE ROOM THE ARE SEACHED IS {room.name}");
           
            Debug.Log($"SEARCH FOR {room.name} IS DONE");  
        }


        /*

        foreach(Transform room in roomList) {
            float dist = Vector3.Distance(transform.position, room.position);
            if (dist < closestdist | closestRoom == null) {
                closestRoom = room;
                closestdist = dist;
            }
        }
        */
    }









}
