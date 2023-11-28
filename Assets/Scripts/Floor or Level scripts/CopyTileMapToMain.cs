using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CopyTileMapToMain : MonoBehaviour {

    private FloorValueScript ValueScript;

    private List<Tilemap> WallTilemaps = new();
    private List<Tilemap> GroundTilemaps = new();

    private const string WallTilemapTag = "Wall tilemap";
    private const string GroundTilemapTag = "Ground tilemap";

    private const string MainWallTilemapTag = "Main wall tilemap";
    private const string MainGroundTilemapTag = "Main ground tilemap";


    private Transform MainGroundTilemap;
    private Transform MainWallTilemap;

    private void Awake() {
        ValueScript = GetComponent<FloorValueScript>();
    }

    public void CopyTileMap() {
        CopyTilesToTilemap(WallTilemaps, MainWallTilemap);
        CopyTilesToTilemap(GroundTilemaps, MainGroundTilemap);
        // DisableRoomTiles(ValueScript.RoomGameObjects);
    }

   

    public void ImportRooms() {
        for (int Childindex = 0; Childindex < transform.childCount; Childindex++) {

            Transform gameChild = transform.GetChild(Childindex);
            
            if (gameChild.CompareTag("Room")) {

                ValueScript.RoomGameObjects.Add(gameChild);

            } else if (gameChild.CompareTag(MainWallTilemapTag)) {
                
                MainWallTilemap = gameChild;
                
            } else if (gameChild.CompareTag(MainGroundTilemapTag)) {
                
                MainGroundTilemap = gameChild;
            
            }
        }
    }

    public void SortTileMapsInRoom() {
        
        List<Transform> tilemaps = new();


        // Get the Room Objects childrens
        foreach (var room in ValueScript.RoomGameObjects) {

            roomInfoScript roomScript = room.GetComponent<roomInfoScript>();
;            roomScript.GetChildrenInRoom();

            foreach (var ChildTilemap in roomScript.roomChildren) {
            
                Tilemap tilemapComponent = ChildTilemap.GetComponent<Tilemap>();

                if (ChildTilemap.CompareTag(WallTilemapTag)) {
                    WallTilemaps.Add(tilemapComponent);
                } else if (ChildTilemap.CompareTag(GroundTilemapTag)) {
                    GroundTilemaps.Add(tilemapComponent);
                }
            }

        }
    }

    public Vector3Int CombineTileAndTilemapPos(Vector3Int tile_pos, Vector3 tilemap_pos ) {
        int xResult = Mathf.FloorToInt(tilemap_pos.x) + tile_pos.x;
        int yResult = Mathf.FloorToInt(tilemap_pos.y) + tile_pos.y;
        int zResult = Mathf.FloorToInt(tilemap_pos.z) + tile_pos.z;

        return new Vector3Int(xResult, yResult, zResult);

    }

    public void CopyTilesToTilemap(List<Tilemap> tilemap, Transform mainTilemapObject) {
        

        if (mainTilemapObject.TryGetComponent<Tilemap>(out var mainTilmap)) {

            foreach (var tilemapObject in tilemap) {
                Vector3 tilemapPos = tilemapObject.transform.position;
                BoundsInt bounds = tilemapObject.cellBounds;


                for (int TileX = bounds.x; TileX < bounds.x + bounds.size.x; TileX++) {
                    for ( int TileY = bounds.y; TileY < bounds.y + bounds.size.y; TileY++) {

                        Vector3Int cellPosition = new(TileX, TileY,0);
                        TileBase sourceTile = tilemapObject.GetTile(cellPosition);

                        if (sourceTile != null) {
                            mainTilmap.SetTile(CombineTileAndTilemapPos(cellPosition, tilemapPos), sourceTile);
                        }

                    }
                }
            }  
        }
    }

    public void DisableRoomTiles(List<Transform> Rooms) {
        foreach (var room in Rooms) {
            for (int ChildIndex = 0; ChildIndex < room.childCount; ChildIndex++) {
                
                GameObject childObject = room.GetChild(ChildIndex).gameObject;

                if (childObject.CompareTag(WallTilemapTag) || childObject.CompareTag(GroundTilemapTag)) {
                    childObject.SetActive(false);
                } 
            
            }
        }
    
    }
}
