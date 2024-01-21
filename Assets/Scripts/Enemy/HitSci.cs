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


    private void Awake()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _EnemyStats = transform.parent.GetComponent<EnemyValuesScript>();
    }


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
        if (_EnemyStats.isHit)
        {
            _Timer += Time.deltaTime;

            // Lerp the color from original to hitColor over hitDuration
            _SpriteRenderer.color = Color.Lerp(_OriginalColor, HitColor, _Timer / HitDuration);

            if (_Timer >= HitDuration)
            {
                // Hit effect is over, reset variables
                _EnemyStats.isHit = false;
                _SpriteRenderer.color = _OriginalColor;
                _Timer = 0f;
            }
        }
    }
}
