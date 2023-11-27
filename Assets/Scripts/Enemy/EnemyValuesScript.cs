using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValuesScript : MonoBehaviour
{
    

    [Header("DITECTION AREA CIRCLE ")]
    public float _DitectionRange;
    public bool _PlayerDitected = false;
    public bool _DitectionEnabled = false;
    public GameObject Player = null;
    public float _movmentSpeed;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}
