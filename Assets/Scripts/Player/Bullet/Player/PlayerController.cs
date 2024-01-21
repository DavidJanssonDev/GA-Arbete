using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Camera MainCamera;
    private Vector2 RawPlayerMousePosition;
    private PlayerValueStats PlayerValueScript;

    public Vector3 PlayerMousePosition;
    public Vector2 RawPlayerMovementControlls;
    public bool PlayerFired;


    private void Awake()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        PlayerValueScript = GetComponent<PlayerValueStats>();
    }


    public void Movement(InputAction.CallbackContext context)
    {
        RawPlayerMovementControlls = context.ReadValue<Vector2>();
    }

    public void MouseMovement(InputAction.CallbackContext context)
    {
        RawPlayerMousePosition = context.ReadValue<Vector2>();
    }

    public void PlayerShooting(InputAction.CallbackContext context)
    {
        if (context.performed && PlayerValueScript.GameOver == false)
        {
            PlayerFired = context.ReadValueAsButton();
        }
    }

   
    private void Update()
    {
        PlayerMousePosition = MainCamera.ScreenToWorldPoint(new Vector3(RawPlayerMousePosition.x, RawPlayerMousePosition.y, MainCamera.transform.position.y));
    }
}