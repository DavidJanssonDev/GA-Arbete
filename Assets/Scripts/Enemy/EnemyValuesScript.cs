using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValuesScript : MonoBehaviour
{
        

    [Header("DITECTION Varaible")]
    public float _DitectionRange;
    public bool _PlayerDitected = false;
    public bool _DitectionEnabled = false;
    public bool EnemyDitectionEnabel = true;
    public GameObject Player = null;

    [Header("ENEMY STATS")]
    public int Health = 10;
    public float _movmentSpeed;
    public int Damage = 1;

    
}
