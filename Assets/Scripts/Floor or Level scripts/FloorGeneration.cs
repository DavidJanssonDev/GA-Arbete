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
        [SerializeField] private Tile roomDoorTile;

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
            RoomRelatedStuff(roomDoorTile);
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

        private void RoomRelatedStuff(Tile roomDoorTile)
        {
            for (int roomChildObjectIndex = 0; roomChildObjectIndex < transform.childCount; roomChildObjectIndex++)
            {
                Transform gameChild = transform.GetChild(roomChildObjectIndex);
                if (gameChild.gameObject.layer == (int)LayerStuff.LayerEnum.Room)
                {
                    ImportRoomObjects(roomDoorTile, gameChild);
                    DecabelAllRoomObjets(gameChild);
                }
            }
        }

        private void DecabelAllRoomObjets(Transform roomObject)
        {
            roomObject.gameObject.SetActive(false);
        }
        


        // Import room objects and generate Room objects for each room
        private void ImportRoomObjects(Tile doorTile, Transform gameChild)
        {
            floorValueScript.RoomList.Add(GenerateRoom(gameChild, true, doorTile));
        }


        // Generate a Room object
        private Room GenerateRoom(Transform gameChild, bool canIncludeEnemy, Tile doorTile) 
        {
            // Generate the Room
            return new Room(gameChild.name, gameChild, canIncludeEnemy, doorTile);
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

        public Tile DoorTile;



        public List<Room> ClosestRooms;
        public List<Room> RoomObjectClosestRooms;
        public List<float> ValueClosestRooms;

        public int AvailableDoors;

        // Constructor to initialize room properties
        public Room(string name, Transform transform, bool canContainEnemies, Tile doorSprite)
        {
            Name = name;
            DoorTile = doorSprite;
            RoomTransform = transform;
            CanContainEnemies = canContainEnemies;
            AvailableDoors = 0;

            RoomTilemaps = new List<Tilemap>();
            RoomObjectClosestRooms = new List<Room>();
            ValueClosestRooms = new List<float>();


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

        // Method to get the distance to another room
        public float GetDistanceToRoom(Transform targetPoint)
        {

            return Vector3.Distance(RoomTransform.position, targetPoint.position);
        }


        // Methods to generate and sort data about closest rooms
        public void GenerateSoritingRoomData()
        {
            GenerateData();
            SortListOfClosestRoom();
        }

        public void GenerateData()
        {
            foreach (var room in FloorGeneration.floorValueScript.RoomList)
            {
                ValueClosestRooms.Add(GetDistanceToRoom(room.RoomTransform));
                RoomObjectClosestRooms.Add(room);
            }
        }

        public void SortListOfClosestRoom()
        {
            List<KeyValuePair<Room, float>> roomDistances = new List<KeyValuePair<Room, float>>();

            for (int i = 0; i < FloorGeneration.floorValueScript.RoomList.Count; i++)
            {
                Room otherRoom = FloorGeneration.floorValueScript.RoomList[i];
                float distance = GetDistanceToRoom(otherRoom.RoomTransform);
                roomDistances.Add(new KeyValuePair<Room, float>(otherRoom, distance));
            }

            roomDistances.Sort((a, b) => a.Value.CompareTo(b.Value));

            ClosestRooms.Clear();

            foreach (var pair in roomDistances)
            {
                ClosestRooms.Add(pair.Key);
            }
        }


        // Method to copy the tilemap of the room to the main tilemap
        public void CopyTileMap(Tilemap mainWallTilemap, Tilemap mainGroundTilemap)
        {
            Tilemap WallTilemap = null;
            Tilemap GroundTilemap = null;
            Tilemap RespawnTilemap = null;

            foreach (Tilemap tilemapChild in RoomTilemaps)
            {
                int layerValue = tilemapChild.gameObject.layer;
                Debug.Log($"Tilemap Layer: {layerValue}");

                if (layerValue == (int)LayerStuff.LayerEnum.Ground)
                {
                    GroundTilemap = tilemapChild;
                }
                else if (layerValue == (int)LayerStuff.LayerEnum.Wall)
                {
                    WallTilemap = tilemapChild;
                }
                else if (layerValue == (int)LayerStuff.LayerEnum.Respawn)
                {
                    RespawnTilemap = tilemapChild;
                }
            }

          
            if (RespawnTilemap != null)
            {
                Debug.Log("___________________________________________________");
                Debug.Log("Copy Started: Copy RespawnTilemap to GroundTilemap");
                TilemapScript.CopyTileMapToTilemap(GroundTilemap, RespawnTilemap);
                Debug.Log("Copy Completed: RespawnTilemap copied to GroundTilemap");
            }

            if (GroundTilemap != null && mainGroundTilemap != null)
            {
                Debug.Log("___________________________________________________");
                Debug.Log("Copy Started: Copy GroundTilemap to MainGroundTilemap");
                TilemapScript.CopyTileMapToTilemap(mainGroundTilemap, GroundTilemap);
                Debug.Log("Copy Completed: GroundTilemap copied to MainGroundTilemap");
            }
            else
            {
                Debug.LogError("GroundTilemap or mainGroundTilemap is null.");
            }

            if (WallTilemap != null && mainWallTilemap != null)
            {
                Debug.Log("___________________________________________________");
                Debug.Log("Copy Started: Copy WallTilemap to MainWallTilemap");
                TilemapScript.CopyTileMapToTilemap(mainWallTilemap, WallTilemap);
                Debug.Log("Copy Completed: WallTilemap copied to MainWallTilemap");
            }
            else
            {
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



