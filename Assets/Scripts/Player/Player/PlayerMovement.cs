using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player 
    private Rigidbody2D _playerRigidbody2D;
    private PlayerController InputConttroller;

    // Player Movement Speed
    [SerializeField] 
    private float _movementSpeed = 5f;

    // Variabels for smoth player movement
    private Vector2 _playerMovement;
    private Vector2 _movementSmoothedInput;
    private Vector2 _movementInputSmoothVelocity;

    private void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        InputConttroller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        _playerMovement = InputConttroller.rawPlayerMovementControlls;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _movementSmoothedInput = Vector2.SmoothDamp(
            _movementSmoothedInput,
            _playerMovement,
            ref _movementInputSmoothVelocity,
            0.1f);
        _playerRigidbody2D.velocity = _movementSmoothedInput * _movementSpeed;
    }
}
