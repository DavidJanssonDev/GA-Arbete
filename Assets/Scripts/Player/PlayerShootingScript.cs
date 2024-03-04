using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShootingScript : MonoBehaviour
{
    public Vector3 mousePos;
    private PlayerController PlayerControllerScript;
    [SerializeField] private Transform Player;

    private void Awake()
    {
        PlayerControllerScript = Player.GetComponent<PlayerController>(); 
    }

    private void Update()
    {

        mousePos = PlayerControllerScript.PlayerMousePosition;
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(mousePos.y - Player.position.y, mousePos.x - Player.position.x));

        transform.rotation = Quaternion.Euler(0,0,angle); 
        

        
         Debug.Log($"Angel: {angle} Mathf.Atan2");

        //Debug.Log($"Angel: {Vector3.Angle(Player.position, mousePos)} Vector3.Angle");


    }

}
