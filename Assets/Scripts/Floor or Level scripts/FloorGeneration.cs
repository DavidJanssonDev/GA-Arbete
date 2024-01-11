using RoomStuff;
using GenerationOfFloorClassStuff;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GenerallStuff;

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
            ImportRoomObjects(roomDoorTile);
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



        // Import room objects and generate Room objects for each room
        private void ImportRoomObjects(Tile doorTile)
        {
            // Import the Room Object in from the grid and generate a Room Object for each room 


            for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
            {
                Transform gameChild = transform.GetChild(childIndex);

               if (gameChild.gameObject.layer == (int)LayerStuff.LayerEnum.Room)
               {
                    // For each Room object makes a new script of it
                    floorValueScript.RoomList.Add(GenerateRoom(gameChild, true, doorTile));
               }
               
            }
        
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
        private List<Tilemap> RoomTilemaps;

        public Tile DoorTile;
        public Dictionary<string, Door> DoorListPos;

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
            DoorListPos = new Dictionary<string, Door>();
            SetTilemaps();
        }

        // Method to set tilemaps of the room
        private void SetTilemaps()
        {

            foreach (Transform child in RoomTransform)
            {
                if (child.CompareTag("Wall tilemap") || child.CompareTag("Ground tilemap"))
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
            DoorListPos = TilemapScript.GetRoomDoorTilePos(RoomTilemaps, DoorTile);
            AvailableDoors = DoorListPos.Count;

            TilemapScript.CopyTileMapToTilemap(mainWallTilemap, RoomTilemaps.Find(tilemap => tilemap.CompareTag("Wall tilemap")));
            TilemapScript.CopyTileMapToTilemap(mainGroundTilemap, RoomTilemaps.Find(tilemap => tilemap.CompareTag("Ground tilemap")));
        }
    }

    // Class representing a door in a room

    public class Door
    {
        public Vector3 DoorPosition;
        public bool DoorAvailable;

        // Constructor to initialize door properties
        public Door(Vector3 position, bool available)
        {
            DoorPosition = position;
            DoorAvailable = available;
        }

        // Method to get the cardinal direction based on a vector
        public static string GetDirection(Vector3Int vector)
        {
            if (vector.y > 0) return "North";
            if (vector.y < 0) return "South";
            if (vector.x > 0) return "East";
            if (vector.x < 0) return "West";
            return "Not a cardinal direction";
        }
    }

    // Class containing static methods related to Tilemaps
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

        // Method to get door positions from a ground tilemap
        public static Dictionary<string, Door> GetRoomDoorTilePos(List<Tilemap> tilemapList, Tile DoorTile)
        {
            Dictionary<string, Door> doorList = new();

            var tilemap = tilemapList.Find(tilemap => tilemap.CompareTag("Ground tilemap"));
            var bounds = tilemap.cellBounds;

            for (int tileX = bounds.x; tileX < bounds.x + bounds.size.x; tileX++)
            {
                for (int tileY = bounds.y; tileY < bounds.y + bounds.size.y; tileY++)
                {
                    var cellPosition = new Vector3Int(tileX, tileY, 0);
                    var sourceTile = tilemap.GetTile(cellPosition);

                    Tile tile = (sourceTile as Tile);
                    if (sourceTile != null && tile != null && tile.sprite == DoorTile.sprite)
                    {
                        string doorDirection = Door.GetDirection(cellPosition);

                        // Handle the case where the key already exists in the dictionary
                        if (doorList.ContainsKey(doorDirection))
                        {
                            int count = 1;
                            string uniqueKey;
                            do
                            {
                                uniqueKey = $"{doorDirection}_{count}";
                                count++;
                            } while (doorList.ContainsKey(uniqueKey));

                            doorDirection = uniqueKey;
                        }

                        doorList.Add(doorDirection, new Door(tilemap.GetCellCenterWorld(cellPosition), true));
                    }
                }
            }

            return doorList;
        }


        // Method to replace a tile in a tilemap
        public static void ReplaceTile(Vector3Int cellPosition, Tile replacementTile, Tilemap tilemap)
        {
            tilemap.SetTile(cellPosition, replacementTile);
            tilemap.RefreshTile(cellPosition);
        }
    }
}



