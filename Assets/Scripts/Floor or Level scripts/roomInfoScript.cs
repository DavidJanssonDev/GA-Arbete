using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomInfoScript : MonoBehaviour {
    private List<Transform> RankingClosestRooms = new();
   

    private FloorValueScript FloorValueScript;

    private void Awake() {
        FloorValueScript = GameObject.FindGameObjectWithTag("Floor").GetComponent<FloorValueScript>();
    }


    public void FindClosestRooms() {



    }






}
