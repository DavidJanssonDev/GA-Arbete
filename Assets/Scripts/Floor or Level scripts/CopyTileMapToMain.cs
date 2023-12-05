using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RoomStuff;

public class CopyTileMapToMain : MonoBehaviour
{
    private FloorValueScript valueScript;

    private void Awake() { 
        valueScript = GetComponent<FloorValueScript>();
    }

    public List<Tilemap> ImportRoomObjects(Tile doorSprite, Tile emptyGroundSprite) {
        List<Tilemap> MainTilemaps = new();
        

        for (int childIndex = 0; childIndex < transform.childCount; childIndex++) {
            Transform gameChild = transform.GetChild(childIndex);


            switch (gameChild.tag) {
                case "Room":
                    valueScript.RoomList.Add(new Room(gameChild.name, gameChild, true, doorSprite, emptyGroundSprite));
                    break;

                case "Main wall tilemap":
                    MainTilemaps.Add(gameChild.GetComponent<Tilemap>());
                    break;

                case "Main ground tilemap":
                    MainTilemaps.Add(gameChild.GetComponent<Tilemap>());
                    break;
            }
        }
        return MainTilemaps;
    }

}