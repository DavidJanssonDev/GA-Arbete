using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomInfoScript : MonoBehaviour {

    public List<Transform> roomChildren = new();
    public List<Dictionary<string, Vector3>> doorPostion = new();

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









}
