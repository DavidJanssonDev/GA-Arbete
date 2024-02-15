using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValuesScript : MonoBehaviour
{
        

    [Header("Detection Varaible")]
    // SETTINGS ON DETECTION FOR THE ENEMY 
    public float DetectionRange;



    
    public PlayerValueStats PlayerStats;
    public Transform PlayerTransform;



    [Header("ENEMY STATS")]
    public int Health = 10;
    public float MovmentSpeed;
    public int Damage = 1;
    public bool isHit = false;
    

}
