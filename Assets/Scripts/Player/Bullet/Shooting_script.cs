using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_2 : MonoBehaviour
{
    [SerializeField] private GameObject _Bullet;
    [SerializeField] private Transform _Shooting_point;

    // Shooting timer 
    private bool _PlayerCanShoot;
    private float _Timer;
    public float TimeBetween;


    //  Getting the values from another script
    private PlayerController _PlayerControllerScript;
    private bool _PlayerShoot;
    private Vector3 MousePos;
    

    private PlayerValueStats _PlayerValueStats;
    
    // Start is called before the first frame update
    void Start()
    {
        _PlayerControllerScript = transform.parent.GetComponent<PlayerController>();
        _PlayerValueStats = transform.parent.GetComponent<PlayerValueStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_PlayerValueStats.GameOver)
        { 
            _PlayerShoot = _PlayerControllerScript.PlayerFired;
            MousePos = _PlayerControllerScript.PlayerMousePosition;
            Vector3 rotation = MousePos - transform.position ;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,rotZ);

            if (!_PlayerCanShoot)
            {
                _Timer += Time.deltaTime;
                if (_Timer > TimeBetween)
                {
                    _PlayerCanShoot = true;
                    _Timer = 0;
                }
            }

            if (_PlayerShoot && _PlayerCanShoot)
            {
                _PlayerCanShoot = false;
                Instantiate(_Bullet, _Shooting_point.position, Quaternion.identity);
            }
        }
    }
}
