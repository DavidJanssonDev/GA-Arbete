using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSci : MonoBehaviour
{

    public Color HitColor; // Color to change to when hit
    public float HitDuration = 0.2f; // Duration of the hit effect in seconds

    private EnemyValuesScript _EnemyStats;
    private SpriteRenderer _SpriteRenderer;
    private Color _OriginalColor;
    private float _Timer;


   


    void Start()
    {

        if (_SpriteRenderer != null)
        {
            _OriginalColor = _SpriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found!");
        }
    }

    void Update()
    {
        
    }
}
