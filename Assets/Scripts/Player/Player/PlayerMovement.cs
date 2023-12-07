using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player 
    private Rigidbody2D _playerRigidbody2D;
    private PlayerController _inputConttroller;
    private PlayerValueStats _playerValueStats;

    // Player Movement Speed
    [SerializeField] 
    

    // Variabels for smoth player movement
    private Vector2 _playerMovement;
    private Vector2 _movementSmoothedInput;
    private Vector2 _movementInputSmoothVelocity;

    private void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _inputConttroller = GetComponent<PlayerController>();
        _playerValueStats = GetComponent<PlayerValueStats>();
    }

    private void Update()
    {
        _playerMovement = _inputConttroller.rawPlayerMovementControlls;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _movementSmoothedInput = Vector2.SmoothDamp(
            _movementSmoothedInput,
            _playerMovement,
            ref _movementInputSmoothVelocity,
            0.1f);
        _playerRigidbody2D.velocity = _movementSmoothedInput * _playerValueStats.MovementSpeed;
    }
}
