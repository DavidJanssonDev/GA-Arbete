using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;

public class PlayerMovement : MonoBehaviour
{
    // Player 
    private Rigidbody2D PlayerRigidBody2D;
    private PlayerController InputConttroller;
    private PlayerValueStats PlayerValueStatsScript;
    
    // Variabels for smoth player movement
    private Vector2 PlayerMovementDirection;
    private Vector2 MovementSmoothedInput;
    private Vector2 MvementInputSmoothVelocity;

    private void Awake()
    {
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        InputConttroller = GetComponent<PlayerController>();
        PlayerValueStatsScript = GetComponent<PlayerValueStats>();
    }

    private void Update()
    {
        PlayerMovementDirection = InputConttroller.RawPlayerMovementControlls;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (PlayerValueStatsScript.GameOver == false)
        {
            MovementSmoothedInput = Vector2.SmoothDamp(MovementSmoothedInput, PlayerMovementDirection, ref MvementInputSmoothVelocity, 0.1f );
            PlayerRigidBody2D.velocity = MovementSmoothedInput * PlayerValueStatsScript.MovementSpeed;
        }
        else
        {
            PlayerRigidBody2D.velocity = new(0, 0);
        }
    }
}
