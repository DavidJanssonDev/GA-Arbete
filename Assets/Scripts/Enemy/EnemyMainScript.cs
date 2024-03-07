using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenerallStuff;

public class EnemyMainScript : MonoBehaviour
{

    private EnemyValuesScript EnemyValues;
    private SpriteRenderer EnemySpriteRenderer;

    private Vector3 LastKnowPosistion;


    public Color HitColor; // Color to change to when hit
    public float HitDuration = 0.2f; // Duration of the hit effect in seconds


    public bool EnemyDitectionIsOn = false;
    private Color OriginalColor;
    private float Timer;



    private void Awake()
    {
        EnemyValues = GetComponent<EnemyValuesScript>();
        EnemySpriteRenderer = GetComponent<SpriteRenderer>();
        EnemyValues.PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(EnemyValues.PlayerTransform);

        EnemyValues.PlayerStats = EnemyValues.PlayerTransform.GetComponent<PlayerValueStats>();


        OriginalColor = EnemySpriteRenderer.color;
        LastKnowPosistion = transform.position;
    }

    void Update()
    {
        if (EnemyValues != null && EnemyValues.PlayerTransform != null && EnemyValues.PlayerStats != null)
        {
            if (!IsGameOver())
            {
                if (EnemyDitectionIsOn)
                {
                    Vector3 target = PlayerDitection();
                    Debug.Log(target);
                    PlayerMovemnt(target);
                }

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

        if (!(Vector2.Distance(transform.position, player.position) <= EnemyValues.DetectionRange))
        {
            return LastKnowPosistion;
        }


        Debug.Log("Player in Range");
        Vector2 direction = player.position - transform.position;


        // Create a layer mask that excludes the "Enemies" layer
        int layerMask = ~(1 << LayerMask.NameToLayer("Enemies"));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, EnemyValues.DetectionRange, layerMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == (int)LayerStuff.LayerEnum.PLAYER)
            {
                Debug.Log("Player FIND");
                LastKnowPosistion = player.position;
                return player.position;
            }
        }

        return LastKnowPosistion;


    }

    private void PlayerMovemnt(Vector3 tagetPosition)
    {
        Debug.Log("ENEMY VOING");
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(tagetPosition.x, tagetPosition.y, 20), EnemyValues.MovmentSpeed * Time.deltaTime);
    }


}