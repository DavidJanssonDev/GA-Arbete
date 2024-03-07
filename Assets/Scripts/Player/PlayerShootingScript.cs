using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class PlayerShootingScript : MonoBehaviour
{
    public Vector3 mousePos;
    private PlayerController PlayerControllerScript;
    private PlayerValueStats PlayerValueStatsScript;

    private float CurrentTime = 0f;
    private float MaxRealodTime;
    
    [SerializeField] private Transform Player;
    [SerializeField] private float ShootingObjectOffset;
    [SerializeField] private GameObject BulletPrefab;
    private void Awake()
    {
        PlayerControllerScript = Player.GetComponent<PlayerController>(); 
        PlayerValueStatsScript = Player.GetComponent<PlayerValueStats>();

        MaxRealodTime = PlayerValueStatsScript.ReoloadTime;
    }

    private void Update()
    {

        if (PlayerControllerScript != null) 
        {
            mousePos = PlayerControllerScript.PlayerMousePosition;
            SetAimObjectShootingPosAndRot(mousePos);

            if (CurrentTime >= MaxRealodTime)
            {
                if (PlayerControllerScript.PlayerFired)
                {
                    Shooting();
                    CurrentTime = 0;
                }
            }
            else
            {
                CurrentTime += Time.deltaTime;
            }
            
        }
        


        //Debug.Log($"Angel: {Vector3.Angle(Player.position, mousePos)} Vector3.Angle");


    }

    private void SetAimObjectShootingPosAndRot(Vector3 MousePos)    
    {
        float angle = (Mathf.Atan2(MousePos.y - Player.position.y, MousePos.x - Player.position.x));
        float DY = ShootingObjectOffset * Mathf.Sin(angle);
        float DX = ShootingObjectOffset * Mathf.Cos(angle);

        Vector3 transform_pos = new(Player.position.x + DX, Player.position.y + DY, 20);
        Quaternion transform_rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * angle) - 90f);

        transform.SetPositionAndRotation(transform_pos, transform_rotation);



    }

    private void Shooting()
    {        
        Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y,20), Quaternion.identity, transform);
    }
}
