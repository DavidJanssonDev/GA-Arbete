using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenerallStuff;
public class Bullet_script : MonoBehaviour
{
    PlayerController PlayerControllerScript;
    PlayerValueStats PlayerValueScript;
    Vector3 MousePos;
    Rigidbody2D Rb;
    Vector2 Rb_velocity;
    float Rot_bullet;
    int PlayerDamage;
    GameObject PlayerObject;
    [SerializeField] float Force;

    private void Awake()    
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerControllerScript = PlayerObject.GetComponent<PlayerController>();
        PlayerValueScript = PlayerObject.GetComponent<PlayerValueStats>();

        Rb = GetComponent<Rigidbody2D>();

        MousePos = PlayerControllerScript._playerMousePosition;
        PlayerDamage = PlayerValueScript.Damage;

        Vector3 Direction = MousePos - PlayerObject.transform.position;
        Vector3 Rotation = transform.position - MousePos;


        Rb_velocity = new Vector2(Direction.x, Direction.y).normalized * Force;
        Rot_bullet = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;
    }

    private void Start()
    {
        Rb.velocity = Rb_velocity;
        transform.rotation = Quaternion.Euler(0,0, Rot_bullet);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)LayerStuff.LayerEnum.ENEMY || collision.gameObject.layer == (int)LayerStuff.LayerEnum.Wall)
        {
            int EnemyHealth = collision.gameObject.GetComponent<EnemyValuesScript>().Health;
            EnemyHealth -= 1;
            if (EnemyHealth >= 0 || collision.gameObject.layer == (int)LayerStuff.LayerEnum.Wall)
            {

                Destroy(gameObject);
            }

        }
    }

}
