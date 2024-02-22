using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerMainScript : MonoBehaviour
{
    private Rigidbody2D PlayerRB2D;
    private PlayerController InputConttroller;
    private PlayerMovement PlayerMovementScrpt;
    private PlayerValueStats PlayerValueStatsScript;


    public Vector2 MovementDirection;


    private void Awake()
    {
        InputConttroller = GetComponent<PlayerController>();
        PlayerMovementScrpt = GetComponent<PlayerMovement>();
        PlayerValueStatsScript = GetComponent<PlayerValueStats>();
        PlayerRB2D = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    private void Update()
    {
        if (!IsGameOver() && InputConttroller != null && PlayerMovementScrpt != null && PlayerValueStatsScript != null)
        {
            MovementDirection = InputConttroller.RawPlayerMovementControlls;   
        }

    }


    private void FixedUpdate()
    {
        if (!IsGameOver() && InputConttroller != null && PlayerMovementScrpt != null && PlayerValueStatsScript != null) {
            
            PlayerMovementScrpt.PlayerMove(MovementDirection, PlayerValueStatsScript.MovementSpeed, PlayerRB2D);
        }
    }


    private bool IsGameOver()
    {
        if (PlayerValueStatsScript.GameOver) return true;
        else return false;
    }




}
