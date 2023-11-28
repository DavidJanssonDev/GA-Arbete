using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomInfoScript : MonoBehaviour {

    public List<Transform> roomChildren = new();
   

    private FloorValueScript FloorValueScript;

    private void Awake() {
        FloorValueScript = GameObject.FindGameObjectWithTag("Floor").GetComponent<FloorValueScript>();
    }

    
    private void Start()
    {
        GetChildrenInRoom();
    }

    public void GetChildrenInRoom()
    {
        int childCount = transform.childCount;

        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            Transform childObject = transform.GetChild(childIndex);
            childObject.name = $"{transform.name} : {childObject.name}";
            roomChildren.Add(childObject);
        }
    }


    public void FindClosestRooms() {
       


    }






}
