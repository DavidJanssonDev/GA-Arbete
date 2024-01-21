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
    private Vector2 RbVelocity;

    private float RotBullet;
    private float Timer = 0;



    private GameObject PlayerObject;
    [SerializeField] private float BulletSpeed;
    [SerializeField] private int BulletLifteTime;



    private void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerControllerScript = PlayerObject.GetComponent<PlayerController>();
        PlayerValueScript = PlayerObject.GetComponent<PlayerValueStats>();

        Rb = GetComponent<Rigidbody2D>();

        MousePos = PlayerControllerScript.PlayerMousePosition;
        Vector3 Direction = MousePos - PlayerObject.transform.position;
        Vector3 Rotation = transform.position - MousePos;


        RbVelocity = new Vector2(Direction.x, Direction.y).normalized * BulletSpeed;
        RotBullet = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;
    }

    private void Start()
    {
        Rb.velocity = RbVelocity;
        transform.rotation = Quaternion.Euler(0, 0, RotBullet);
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > BulletLifteTime)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject collidedObject = collision.gameObject;
        if (collidedObject.layer == (int)LayerStuff.LayerEnum.ENEMY)
        {
            EnemyValuesScript EnemyStats = collidedObject.GetComponent<EnemyValuesScript>();
            EnemyStats.Health -= PlayerValueScript.Damage;
            EnemyStats.isHit = true;

            if (EnemyStats.Health <= 0)
            {
                Destroy(collidedObject);
            }

            Destroy(gameObject);
        }
    }
}


   
