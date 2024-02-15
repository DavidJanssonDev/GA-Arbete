using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenerallStuff;

public class EnemyMainScript : MonoBehaviour
{

    private EnemyValuesScript EnemyValues;
    private SpriteRenderer EnemySpriteRenderer;


    public Color HitColor; // Color to change to when hit
    public float HitDuration = 0.2f; // Duration of the hit effect in seconds
   
    private Color OriginalColor;
    private float Timer;



    private void Awake()
    {
        EnemyValues = GetComponent<EnemyValuesScript>();
        EnemySpriteRenderer = GetComponent<SpriteRenderer>();
        EnemyValues.PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyValues.PlayerStats = EnemyValues.PlayerTransform.GetComponent<PlayerValueStats>();


        OriginalColor = EnemySpriteRenderer.color;
    }

    void Update()
    {
       if (EnemyValues != null && EnemyValues.PlayerTransform != null && EnemyValues.PlayerStats != null)
       {
            if (!IsGameOver())
            {
                Vector3 target = PlayerDitection();        
                Debug.Log($"Target Pos:  {target}");
                //PlayerMovemnt(target);
                
            }

            if (EnemyValues.isHit)
            {
                Timer += Time.deltaTime;

                // Lerp the color from original to hitColor over hitDuration
                EnemySpriteRenderer.color = Color.Lerp(OriginalColor, HitColor, Timer / HitDuration);

                if (Timer >= HitDuration)
                {
                    // Hit effect is over, reset variables
                    EnemyValues.isHit = false;
                    EnemySpriteRenderer.color = OriginalColor;
                    Timer = 0f;
                }
            }
        }
    }

    private bool IsGameOver()
    {
        if (EnemyValues.PlayerStats.GameOver) return true;
        else return false;
    }

    private Vector3 PlayerDitection()
    {
        Transform player = EnemyValues.PlayerTransform;

        if (Vector2.Distance(transform.position, player.position) <= EnemyValues.DetectionRange)
        {
            Vector2 direction = player.position - transform.position;

            // Create a layer mask that excludes the "Enemies" layer
            int layerMask = ~(1 << LayerMask.NameToLayer("Enemies"));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, EnemyValues.DetectionRange, layerMask);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    return player.position;
                }
            }      
        }
        
        return transform.position;
        

    }

    private void PlayerMovemnt(Vector3 tagetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, tagetPosition, EnemyValues.MovmentSpeed * Time.deltaTime);
    }


}
