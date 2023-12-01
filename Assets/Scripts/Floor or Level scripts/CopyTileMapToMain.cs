using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RoomStuff;

public class CopyTileMapToMain : MonoBehaviour
{
    private FloorValueScript valueScript;
    private FloorGeneration floorGeneration;

    private readonly List<Tilemap> wallTilemaps = new();
    private readonly List<Tilemap> groundTilemaps = new();

    private const string WallTilemapTag = "Wall tilemap";
    private const string GroundTilemapTag = "Ground tilemap";

    private const string MainWallTilemapTag = "Main wall tilemap";
    private const string MainGroundTilemapTag = "Main ground tilemap";


    private void Awake() { 
        valueScript = GetComponent<FloorValueScript>();
        floorGeneration = GetComponent<FloorGeneration>();
    }
    /*
        public void CopyTileMap() {
            SortTileMapsInRoom();
            CopyTilesToTilemap(wallTilemaps, mainWallTilemap);
            CopyTilesToTilemap(groundTilemaps, mainGroundTilemap);
            DisableRoomTiles(valueScript.RoomGameObjects);
        }
    */

    public List<Transform> ImportRooms() {
        List<Transform> MainTilemaps = new();

        for (int childIndex = 0; childIndex < transform.childCount; childIndex++) {
            Transform gameChild = transform.GetChild(childIndex);
            switch (gameChild.tag) {
                case "Room":
                    valueScript.RoomList.Add(new Room(gameChild.name, gameChild, true));
                    break;

                case MainWallTilemapTag:
                    MainTilemaps.Add(gameChild);
                    break;

                case MainGroundTilemapTag:
                    MainTilemaps.Add(gameChild);
                    break;
            }
        }
        return MainTilemaps;
    }

    public void SortTileMapsInRoom()
    {
        foreach (var room in valueScript.RoomObjects)
        {
            var roomScript = room.GetComponent<roomInfoScript>();
            roomScript.GetChildrenInRoom();

            foreach (var childTilemap in roomScript.roomChildren)
            {
                var tilemapComponent = childTilemap.GetComponent<Tilemap>();

                if (childTilemap.CompareTag(WallTilemapTag))
                {
                    wallTilemaps.Add(tilemapComponent);
                }
                else if (childTilemap.CompareTag(GroundTilemapTag))
                {
                    groundTilemaps.Add(tilemapComponent);
                }
            }
        }
    }

    
    public void CopyTilesToTilemap(List<Tilemap> tilemaps, Transform mainTilemapObject)
    {
        if (mainTilemapObject?.TryGetComponent(out Tilemap mainTilemap) == true) {

            foreach (var tilemapObject in tilemaps) {

                var tilemapPos = tilemapObject.transform.position;
                var bounds = tilemapObject.cellBounds;

                for (int tileX = bounds.x; tileX < bounds.x + bounds.size.x; tileX++) {
                    for (int tileY = bounds.y; tileY < bounds.y + bounds.size.y; tileY++) {
                        
                        
                        var cellPosition = new Vector3Int(tileX, tileY, 0);
                        var sourceTile = tilemapObject.GetTile(cellPosition);

                        if (sourceTile != null) {

                            mainTilemap.SetTile(
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

    public void DisableRoomTiles(List<Transform> rooms)
    {
        foreach (var room in rooms)
        {
            foreach (Transform child in room)
            {
                var childObject = child.gameObject;

                if (childObject.CompareTag(WallTilemapTag) || childObject.CompareTag(GroundTilemapTag))
                {
                    childObject.SetActive(false);
                }
            }
        }
    }
}
