using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;



public class FloorGeneration : MonoBehaviour {

    private FloorValueScript floorValueScript;
    private CopyTileMapToMain tileCopyScript;

    [Header("Map Room Sprite Stuff")]
    [SerializeField] private Tile roomDoorTile;
    [SerializeField] private Tile emptyGroundTile;
    
    private Tilemap MainWallTilemap;
    private Tilemap MainGroundTilemap;



    private void Awake() {


        floorValueScript = GetComponent<FloorValueScript>();
        tileCopyScript = GetComponent<CopyTileMapToMain>();
    }

    private void Start() {
       
        List<Tilemap> TempMainMaps = tileCopyScript.ImportRoomObjects(roomDoorTile, emptyGroundTile); // Takes in the Rooms and seperates them
        foreach (var tilemap in TempMainMaps) {
            if (tilemap.transform.CompareTag("Main wall tilemap")){
                MainWallTilemap = tilemap;
            } else if (tilemap.CompareTag("Main ground tilemap")) {
                MainGroundTilemap = tilemap;
            }
        }


        
        foreach (var room in floorValueScript.RoomList) {
            Debug.Log(room.Name);
            room.CopyTileMap(MainWallTilemap,MainGroundTilemap);
        }
        

    }
}

namespace RoomStuff
{
    public class Room
    {
        public string Name;
        public bool CanContainEnemies;
        public Transform RoomTransform; // The rooms Transfrom where the center point is 
        public List<Tilemap> Tilemaps;

        public Tile DoorTile;
        public Tile GroundTile;
        public List<Vector3> DoorPos; // a list of postions of where the doors is
        public List<Transform> ClosestRooms;


        // CUNSTRUCTOR
        public Room(string name, Transform transform, bool canContainEnemies, Tile doorSprite, Tile groundSprite)
        {
            Name = name;
            DoorTile = doorSprite;
            GroundTile = groundSprite;
            RoomTransform = transform;
            CanContainEnemies = canContainEnemies;

            // Initialize lists
            Tilemaps = new();
            ClosestRooms = new();
            DoorPos = new();

            foreach (Transform child in transform) {
                if (child.CompareTag("Wall tilemap")) {
                    Tilemaps.Add(child.GetComponent<Tilemap>());
                } else if (child.CompareTag("Ground tilemap")) {
                    Tilemaps.Add(child.GetComponent<Tilemap>());
                }
            }
        }

        public float GetDistanceToRoom(Transform targetPoint)
        {
            return Vector3.Distance(RoomTransform.position, targetPoint.position);
        }

        public void GetClosestRooms() {

        }

        public void GetRoomDoors() {

            var tilemap = Tilemaps.Find(tilemap => tilemap.CompareTag("Ground tilemap"));
            var bounds = tilemap.cellBounds;

            for (int tileX = bounds.x; tileX < bounds.x + bounds.size.x; tileX++) {
                
                for (int tileY = bounds.y; tileY < bounds.y + bounds.size.y; tileY++) {

                    var cellPosition = new Vector3Int(tileX, tileY, 0);
                    var sourceTile = tilemap.GetTile(cellPosition);

                    if (sourceTile != null)
                    {
                        // Get the sprite of the tile
                        Sprite tileSprite = (sourceTile as Tile)?.sprite;

                        if (tileSprite == DoorTile.sprite) {
                            DoorPos.Add(tilemap.GetCellCenterWorld(cellPosition));
                            tilemap.SetTile(cellPosition, GroundTile);
                            tilemap.RefreshTile(cellPosition);
                        }
                    }
                }
            }
        }

        public void CopyTileMap(Tilemap mainWallTilemap, Tilemap mainGroundTilemap)
        {
            // Copys the Wall tilemaps to the tilemap
            CopysTileMapToTilemap(mainWallTilemap, Tilemaps.Find(tilemap => tilemap.CompareTag("Wall tilemap")));

            // Copys the wall tilemaps to the tilemap
            CopysTileMapToTilemap(mainGroundTilemap, Tilemaps.Find(tilemap => tilemap.CompareTag("Ground tilemap")));

            GetRoomDoors();
        }

        // Copies tiles from one Tilemap (orgTilemap) to another Tilemap (tilemapToCopyTo)
        private void CopysTileMapToTilemap(Tilemap tilemapToCopyTo, Tilemap orgTilemap) {

            var tilemapPos = orgTilemap.transform.position;
            var bounds = orgTilemap.cellBounds;

            for (int tileX = bounds.x; tileX < bounds.x + bounds.size.x; tileX++) {
                for (int tileY = bounds.y; tileY < bounds.y + bounds.size.y; tileY++) {

                    var cellPosition = new Vector3Int(tileX, tileY, 0);
                    var sourceTile = orgTilemap.GetTile(cellPosition);

                    if (sourceTile != null) {

                        tilemapToCopyTo.SetTile(new Vector3Int(
                                    Mathf.FloorToInt(tilemapPos.x) + cellPosition.x,
                                    Mathf.FloorToInt(tilemapPos.y) + cellPosition.y,
                                    Mathf.FloorToInt(tilemapPos.z) + cellPosition.z)
                            ,sourceTile);
                    }
                }
            }
        }

    }

}