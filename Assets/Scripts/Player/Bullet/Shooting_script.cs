using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_2 : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shooting_point;

    // Shooting timer 
    bool PlayerCanShoot;
    float _timer;
    public float _timeBetween;


    //  Getting the values from another script
    PlayerController PlayerValueScript;
    bool PlayerShoot;
    Vector3 MousePos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerValueScript = transform.parent.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerShoot = PlayerValueScript.PlayerFired;
        MousePos = PlayerValueScript.PlayerMousePosition;
        Vector3 rotation = MousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);

        if (!PlayerCanShoot)
        {
            _timer += Time.deltaTime;
            if (_timer > _timeBetween)
            {
                PlayerCanShoot = true;
                _timer = 0;
            }
        }

        if (PlayerShoot && PlayerCanShoot)
        {
            PlayerCanShoot = false;
            Instantiate(bullet, shooting_point.position, Quaternion.identity);
        }
    }
}
