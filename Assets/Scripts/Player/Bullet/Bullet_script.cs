using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_script : MonoBehaviour
{
    PlayerController _playerValueScript;
    Vector3 _mousePos;
    Rigidbody2D _rb;
    Vector2 _rb_velocity;
    float _rot_bullet;
    [SerializeField]  float Force;

    private void Awake()    
    {
        _playerValueScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();

        _mousePos = _playerValueScript._playerMousePosition;

        Vector3 direction = _mousePos - transform.position;
        Vector3 Rotation = transform.position - _mousePos;
        

        _rb_velocity = new Vector2(direction.x, direction.y).normalized * Force;
        _rot_bullet = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;
    }

    private void Start()
    {   
        _rb.velocity = _rb_velocity;
        transform.rotation = Quaternion.Euler(0,0, _rot_bullet);
        
    }


}
