using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Camera _camera;
    Vector2 rawPlayerMousePosition;

    public Vector3 _playerMousePosition;
    public Vector2 rawPlayerMovementControlls;
    public bool _playerFired;


    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    public void Movement(InputAction.CallbackContext context)
    {
        rawPlayerMovementControlls = context.ReadValue<Vector2>();
    }

    public void MouseMovement(InputAction.CallbackContext context)
    { 
        rawPlayerMousePosition = context.ReadValue<Vector2>();
    }

    public void PlayerShooting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerFired = context.ReadValueAsButton();
        }
    }

   
    private void Update()
    {
        _playerMousePosition = _camera.ScreenToWorldPoint(new Vector3(rawPlayerMousePosition.x, rawPlayerMousePosition.y, _camera.transform.position.y));
    }
}