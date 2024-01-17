using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenerallStuff;
public class Bullet_script : MonoBehaviour
{
    private PlayerController PlayerControllerScript;
    private PlayerValueStats PlayerValueScript;
    
    private Rigidbody2D Rb;
    private Vector3 MousePos;
    private Vector2 Rb_velocity;
    
    private float Rot_bullet;
    private float timer = 0;
    

    private GameObject PlayerObject;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private int BulletLifteTime;



    private void Awake()    
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerControllerScript = PlayerObject.GetComponent<PlayerController>();
        PlayerValueScript = PlayerObject.GetComponent<PlayerValueStats>();

        Rb = GetComponent<Rigidbody2D>();

        MousePos = PlayerControllerScript._playerMousePosition;
        Vector3 Direction = MousePos - PlayerObject.transform.position;
        Vector3 Rotation = transform.position - MousePos;


        Rb_velocity = new Vector2(Direction.x, Direction.y).normalized * BulletSpeed;
        Rot_bullet = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;
    }

    private void Start()
    {
        Rb.velocity = Rb_velocity;
        transform.rotation = Quaternion.Euler(0,0, Rot_bullet);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > BulletLifteTime)
        {
            Debug.Log("Shoot Deastryd");
            Destroy(gameObject);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.layer == (int) LayerStuff.LayerEnum.ENEMY)
        {
            collidedObject.GetComponent<EnemyValuesScript>().Health -= PlayerValueScript.Damage;
            Destroy(gameObject);
        } 
       
        
    }
    

}
