using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Camera MainCamera;
    private PlayerValueStats PlayerValueScript;


    [Header("PLAYER MOUSE")]
    public Vector3 PlayerMousePosition;
    [SerializeField] private Vector2 RawPlayerMousePosition;

    [Header("PLAYER MOVEMENT")]
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
        PlayerMousePosition = MainCamera.ScreenToWorldPoint(new(RawPlayerMousePosition.x, RawPlayerMousePosition.y, 20f));
    }
}