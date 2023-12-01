using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomStuff;

public class FloorValueScript : MonoBehaviour {
    public int FloorValueSize = 10000;
    public int RoomDistanceBetween = 7;
    public List<Transform> RoomObjects = new();
    public List<Room> RoomList = new();
}
