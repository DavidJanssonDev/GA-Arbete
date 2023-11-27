using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _Speed;

    private Vector2 _Movement;
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnMovement(InputValue value)
    {
        _Movement = value.Get<Vector2>();

        if (_Movement.x != 0 || _Movement.y != 0)
        {
            _animator.SetFloat("X", _Movement.x);
            _animator.SetFloat("Y", _Movement.y);
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

    }

    private void FixedUpdate()
    {

        _rb.MovePosition(_rb.position + _Movement * Time.fixedDeltaTime * _Speed);

    }
}
