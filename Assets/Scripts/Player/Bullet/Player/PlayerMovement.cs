using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;

public class PlayerMovement : MonoBehaviour
{
    // Player 
    private Rigidbody2D PlayerRigidBody2D;
    
    // Variabels for smoth player movement
    private Vector2 MovementSmoothedInput;
    private Vector2 MvementInputSmoothVelocity;



    public void PlayerMove(Vector2 PlayerMovementDirection, float speed, Rigidbody2D rb)
    {
        MovementSmoothedInput = Vector2.SmoothDamp(MovementSmoothedInput, PlayerMovementDirection, ref MvementInputSmoothVelocity, 0.1f);
        rb.velocity = MovementSmoothedInput * speed;
        
    }
}
