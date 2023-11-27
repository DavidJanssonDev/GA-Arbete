using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting_2 : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shooting_point;

    // Shooting timer 
    bool _playerCanShoot;
    float _timer;
    public float _timeBetween;


    //  Getting the values from another script
    PlayerController _playerValueScript;
    bool _playerShoot;
    Vector3 _mousePos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _playerValueScript = transform.parent.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerShoot = _playerValueScript._playerFired;
        _mousePos = _playerValueScript._playerMousePosition;
        Vector3 rotation = _mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);

        if (!_playerCanShoot)
        {
            _timer += Time.deltaTime;
            if (_timer > _timeBetween)
            {
                _playerCanShoot = true;
                _timer = 0;
            }
        }

        if (_playerShoot && _playerCanShoot)
        {
            Debug.Log("Shoot");
            _playerCanShoot = false;
            Instantiate(bullet, shooting_point.position, Quaternion.identity);
        }
    }
}
