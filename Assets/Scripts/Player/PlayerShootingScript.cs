using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShootingScript : MonoBehaviour
{
    public Vector3 mousePos;
    private PlayerController PlayerControllerScript;

    private void Awake()
    {
        PlayerControllerScript = transform.parent.GetComponent<PlayerController>();
      
    }



    private void Update()
    {
        mousePos = PlayerControllerScript.PlayerMousePosition;
        Debug.Log($"parent Transform : {transform.parent.position} | GameObject: {transform.parent.gameObject}");



        float diffX = mousePos.x - transform.parent.position.x;
        float diffY = mousePos.y - transform.parent.position.y;

        Debug.Log($"DiffX : {diffX} | DiffY : {diffY} ");

        float angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;

        Debug.Log(angle);

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Debug.Log(Vector3.Angle(transform.parent.position, mousePos));

          

    }

}
