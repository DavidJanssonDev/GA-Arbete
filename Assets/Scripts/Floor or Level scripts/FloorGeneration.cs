using RoomStuff;
using GenerationOfFloorClassStuff;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GenerallStuff;
using static GenerallStuff.LayerStuff;

namespace GenerationOfFloorClassStuff
{
    public class FloorGeneration : MonoBehaviour
    {
        public static FloorValueScript floorValueScript;

        [Header("Map Room Sprite Stuff")]
       

        [SerializeField] private Tilemap MainWallTilemap;
        [SerializeField] private Tilemap MainGroundTilemap;

        private void Awake()
        {
            floorValueScript = GetComponent<FloorValueScript>();
        }

        // Method to generate the floor
        public void Generate()
        {
            // Import room objects and copy their tilemaps to the main tilemap
            RoomRelatedStuff();
            CopyRoomTilemapsToMainTilemap();
        }

        // Copy tilemaps of each room to the main tilemap
        private void CopyRoomTilemapsToMainTilemap()
        {
            // Check if MainWallTilemap and MainGroundTilemap are not null before calling CopyTileMap
            if (MainWallTilemap != null && MainGroundTilemap != null && floorValueScript.RoomList != null)
            {
                foreach (var room in floorValueScript.RoomList)
                {
                    room.CopyTileMap(MainWallTilemap, MainGroundTilemap);
                }
            }
        }

        private void RoomRelatedStuff()
        {
            for (int roomChildObjectIndex = 0; roomChildObjectIndex < transform.childCount; roomChildObjectIndex++)
            {
                Transform gameChild = transform.GetChild(roomChildObjectIndex);
                if (gameChild.gameObject.layer == (int)LayerStuff.LayerEnum.Room)
                {
                    ImportRoomObjects(gameChild);
                    DecabelAllRoomObjets(gameChild);
                }
            }
        }

        private void DecabelAllRoomObjets(Transform roomObject)
        {
            roomObject.gameObject.SetActive(false);
        }
        


        // Import room objects and generate Room objects for each room
        private void ImportRoomObjects(Transform gameChild)
        {
            floorValueScript.RoomList.Add(GenerateRoom(gameChild, true));
        }


        // Generate a Room object
        private Room GenerateRoom(Transform gameChild, bool canIncludeEnemy) 
        {
            // Generate the Room
            return new Room(gameChild.name, gameChild, canIncludeEnemy);
        }

    }
}

namespace RoomStuff
{
    // Class representing a room
    public class Room
    {
        public string Name;
        public bool CanContainEnemies;
        public Transform RoomTransform;
        private readonly List<Tilemap> RoomTilemaps;



        // Constructor to initialize room properties
        public Room(string name, Transform transform, bool canContainEnemies)
        {
            Name = name;

            RoomTransform = transform;
            CanContainEnemies = canContainEnemies;

            RoomTilemaps = new List<Tilemap>();


            SetTilemaps();
        }

        // Method to set tilemaps of the room
        private void SetTilemaps()
        {
            foreach (Transform child in RoomTransform)
            {
                if (child.gameObject.layer == (int)LayerStuff.LayerEnum.Ground ||
                    child.gameObject.layer == (int)LayerStuff.LayerEnum.Wall ||
                    child.gameObject.layer == (int)LayerStuff.LayerEnum.Respawn)
                {
                    RoomTilemaps.Add(child.GetComponent<Tilemap>());
                }
            }
        }

        // Method to copy the tilemap of the room to the main tilemap
        public void CopyTileMap(Tilemap mainWallTilemap, Tilemap mainGroundTilemap)
        {
            Tilemap WallTilemap = null;
            Tilemap GroundTilemap = null;


            foreach (Tilemap tilemapChild in RoomTilemaps)
            {
                if (tilemapChild.gameObject.layer == (int)LayerStuff.LayerEnum.Ground)
                {
                    GroundTilemap = tilemapChild;
                }
                else if (tilemapChild.gameObject.layer == (int)LayerStuff.LayerEnum.Wall)
                {
                    WallTilemap = tilemapChild;
                }
            }

            if (GroundTilemap != null && mainGroundTilemap != null)
            {
                TilemapScript.CopyTileMapToTilemap(mainGroundTilemap, GroundTilemap);
            }
            else
            {
                Debug.LogError("GroundTilemap or mainGroundTilemap is null.");
            }

            if (WallTilemap != null && mainWallTilemap != null)
            {
                TilemapScript.CopyTileMapToTilemap(mainWallTilemap, WallTilemap);
            }
            else
            {
                Debug.Log(WallTilemap);
                Debug.Log(mainWallTilemap);
                Debug.LogError("WallTilemap or mainWallTilemap is null.");
            }
        }



        // Class containing static methods related to Tilemap
        public class TilemapScript
        {

            // Method to copy the contents of one tilemap to another
            public static void CopyTileMapToTilemap(Tilemap tilemapToCopyTo, Tilemap orgTilemap)
            {
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
                            tilemapToCopyTo.SetTile(new Vector3Int(
                                        Mathf.FloorToInt(tilemapPos.x) + cellPosition.x,
                                        Mathf.FloorToInt(tilemapPos.y) + cellPosition.y,
                                        Mathf.FloorToInt(tilemapPos.z) + cellPosition.z)
                                , sourceTile);
                        }
                    }
                }
            }


            // Method to replace a tile in a tilemap
            public static void ReplaceTile(Vector3Int cellPosition, Tile replacementTile, Tilemap tilemap)
            {
                tilemap.SetTile(cellPosition, replacementTile);
                tilemap.RefreshTile(cellPosition);
            }
        }
    }
}



