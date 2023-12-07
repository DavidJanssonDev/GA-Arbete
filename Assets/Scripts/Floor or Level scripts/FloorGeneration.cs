using RoomStuff;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorGeneration : MonoBehaviour
{
    public static FloorValueScript floorValueScript;

    [Header("Map Room Sprite Stuff")]
    [SerializeField] private Tile roomDoorTile;
    [SerializeField] private Tile emptyGroundTile;

    private Tilemap MainWallTilemap;
    private Tilemap MainGroundTilemap;
    private Tilemap AlgorithmTilemap;

    private void Awake()
    {
        floorValueScript = GetComponent<FloorValueScript>();
    }

    private void Start()
    {
        List<Tilemap> tempMainMaps = ImportRoomObjects(roomDoorTile, emptyGroundTile);

        foreach (var tilemap in tempMainMaps)
        {
            AssignTilemap(tilemap);
        }

        foreach (var room in floorValueScript.RoomList)
        {
            room.CopyTileMap(MainWallTilemap, MainGroundTilemap);
        }
    }

    private void AssignTilemap(Tilemap tilemap)
    {
        switch (tilemap.CompareTag("Main wall tilemap"))
        {
            case true when tilemap.transform.CompareTag("Main wall tilemap"):
                MainWallTilemap = tilemap;
                break;
            case true when tilemap.CompareTag("Main ground tilemap"):
                MainGroundTilemap = tilemap;
                break;
            case true when tilemap.CompareTag("AlgoTilemap"):
                AlgorithmTilemap = tilemap;
                break;
        }
    }

    public List<Tilemap> ImportRoomObjects(Tile doorSprite, Tile emptyGroundSprite)
    {
        List<Tilemap> mainTilemaps = new List<Tilemap>();

        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            Transform gameChild = transform.GetChild(childIndex);

            switch (gameChild.tag)
            {
                case "Room":
                    floorValueScript.RoomList.Add(new Room(gameChild.name, gameChild, true, doorSprite, emptyGroundSprite));
                    break;
                case "Main wall tilemap":
                case "Main ground tilemap":
                    mainTilemaps.Add(gameChild.GetComponent<Tilemap>());
                    break;
            }
        }
        return mainTilemaps;
    }
}

namespace RoomStuff
{
    public class Room
    {
        public string Name;
        public bool CanContainEnemies;
        public Transform RoomTransform;
        public List<Tilemap> Tilemaps;

        public Tile DoorTile;
        public Tile GroundTile;
        public Dictionary<string, Door> DoorPos;

        public List<Room> ClosestRooms;
        public List<Room> RoomObjectClosestRooms;
        public List<float> ValueClosestRooms;

        public int AvailableDoors;

        public Room(string name, Transform transform, bool canContainEnemies, Tile doorSprite, Tile groundSprite)
        {
            Name = name;
            DoorTile = doorSprite;
            GroundTile = groundSprite;
            RoomTransform = transform;
            CanContainEnemies = canContainEnemies;
            AvailableDoors = 0;

            Tilemaps = new List<Tilemap>();
            RoomObjectClosestRooms = new List<Room>();
            ValueClosestRooms = new List<float>();
            DoorPos = new Dictionary<string, Door>();

            foreach (Transform child in transform)
            {
                if (child.CompareTag("Wall tilemap") || child.CompareTag("Ground tilemap"))
                {
                    Tilemaps.Add(child.GetComponent<Tilemap>());
                }
            }
        }

        public float GetDistanceToRoom(Transform targetPoint)
        {
            return Vector3.Distance(RoomTransform.position, targetPoint.position);
        }

        public void GetClosestRooms()
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
            DoorPos = TilemapScript.GetRoomDoors(false, Tilemaps, DoorTile, GroundTile);

            TilemapScript.CopyTileMapToTilemap(mainWallTilemap, Tilemaps.Find(tilemap => tilemap.CompareTag("Wall tilemap")));
            TilemapScript.CopyTileMapToTilemap(mainGroundTilemap, Tilemaps.Find(tilemap => tilemap.CompareTag("Ground tilemap")));
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

        public static Dictionary<string, Door> GetRoomDoors(bool replaceDoorSprites, List<Tilemap> tilemapList, Tile DoorTile, Tile GroundTile)
        {
            Dictionary<string, Door> doorList = new Dictionary<string, Door>();

            var tilemap = tilemapList.Find(tilemap => tilemap.CompareTag("Ground tilemap"));
            var bounds = tilemap.cellBounds;

            for (int tileX = bounds.x; tileX < bounds.x + bounds.size.x; tileX++)
            {
                for (int tileY = bounds.y; tileY < bounds.y + bounds.size.y; tileY++)
                {
                    var cellPosition = new Vector3Int(tileX, tileY, 0);
                    var sourceTile = tilemap.GetTile(cellPosition);

                    if (sourceTile != null && (sourceTile as Tile)?.sprite == DoorTile.sprite)
                    {
                        string doorDirection = Door.GetDirection(cellPosition);

                        doorList.Add(doorDirection, new Door(tilemap.GetCellCenterWorld(cellPosition), true));

                        if (replaceDoorSprites)
                        {
                            ReplaceTile(cellPosition, GroundTile, tilemap);
                        }
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
