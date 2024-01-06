using RoomStuff;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    public void Generate()
    {
        ImportRoomObjects(roomDoorTile);
        CopyRoomTilemapsToMainTilemap();
    }

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




    private void ImportRoomObjects(Tile doorTile)
    {
        // Import the Room Object in from the grid and generate a Room Object for each room 


        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            Transform gameChild = transform.GetChild(childIndex);

           if (gameChild.CompareTag("Room"))
           {
                // For each Room object makes a new script of it
                floorValueScript.RoomList.Add(GenerateRoom(gameChild, true, doorTile));
           }
               
        }
        
    }
    
    private Room GenerateRoom(Transform gameChild, bool canIncludeEnemy, Tile doorTile) 
    {
        // Generate the Room
        return new Room(gameChild.name, gameChild, canIncludeEnemy, doorTile);
    }

}

namespace RoomStuff
{
    public class Room
    {
        public string Name;
        public bool CanContainEnemies;
        public Transform RoomTransform;
        private List<Tilemap> RoomTilemaps;

        public Tile DoorTile;
        public Dictionary<string, Door> DoorPos;

        public List<Room> ClosestRooms;
        public List<Room> RoomObjectClosestRooms;
        public List<float> ValueClosestRooms;

        public int AvailableDoors;

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
            DoorPos = new Dictionary<string, Door>();

        }
        

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


        public float GetDistanceToRoom(Transform targetPoint)
        {
            return Vector3.Distance(RoomTransform.position, targetPoint.position);
        }

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

        public void CopyTileMap(Tilemap mainWallTilemap, Tilemap mainGroundTilemap)
        {
            DoorPos = TilemapScript.GetRoomDoorTilePos(RoomTilemaps, DoorTile);

            TilemapScript.CopyTileMapToTilemap(mainWallTilemap, RoomTilemaps.Find(tilemap => tilemap.CompareTag("Wall tilemap")));
            TilemapScript.CopyTileMapToTilemap(mainGroundTilemap, RoomTilemaps.Find(tilemap => tilemap.CompareTag("Ground tilemap")));
        }
    }

    public class Door
    {
        public Vector3 DoorPosition;
        public bool DoorAvailable;

        public Door(Vector3 position, bool available)
        {
            DoorPosition = position;
            DoorAvailable = available;
        }

        public static string GetDirection(Vector3Int vector)
        {
            if (vector.y > 0) return "North";
            if (vector.y < 0) return "South";
            if (vector.x > 0) return "East";
            if (vector.x < 0) return "West";
            return "Not a cardinal direction";
        }
    }

    public class TilemapScript
    {
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
                    if (sourceTile != null && tile != null ? tile.sprite : null == DoorTile.sprite)
                    {
                        string doorDirection = Door.GetDirection(cellPosition);

                        doorList.Add(doorDirection, new Door(tilemap.GetCellCenterWorld(cellPosition), true));
                    }
                }
            }

            return doorList;
        }

        public static void ReplaceTile(Vector3Int cellPosition, Tile replacementTile, Tilemap tilemap)
        {
            tilemap.SetTile(cellPosition, replacementTile);
            tilemap.RefreshTile(cellPosition);
        }
    }
}



