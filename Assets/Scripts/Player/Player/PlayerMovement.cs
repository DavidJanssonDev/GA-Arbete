using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;

public class PlayerMovement : MonoBehaviour
{
    // Player 
    private Rigidbody2D PlayreRIgidBody2D;
    private PlayerController InputConttroller;
    private PlayerValueStats PlayerValueStatsScript;
    
    // Variabels for smoth player movement
    private Vector2 PlayerMovementDirection;
    private Vector2 MovementSmoothedInput;
    private Vector2 MvementInputSmoothVelocity;

    private void Awake()
    {
        PlayreRIgidBody2D = GetComponent<Rigidbody2D>();
        InputConttroller = GetComponent<PlayerController>();
        PlayerValueStatsScript = GetComponent<PlayerValueStats>();
    }

    private void Update()
    {
        PlayerMovementDirection = InputConttroller.rawPlayerMovementControlls;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (PlayerValueStatsScript.GameOver == false)
        {
            MovementSmoothedInput = Vector2.SmoothDamp(MovementSmoothedInput, PlayerMovementDirection, ref MvementInputSmoothVelocity, 0.1f );
            PlayreRIgidBody2D.velocity = MovementSmoothedInput * PlayerValueStatsScript.MovementSpeed;
        }
    }
}
