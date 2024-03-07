using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenerallStuff;
using UIStuff;
using PlayerStats;

public class PlayerHP : MonoBehaviour
{
    private PlayerUI PlayerUIScript;
    private PlayerValueStats PlayerValueStats;

    public void SetUp()
    {
        PlayerValueStats = transform.GetComponent<PlayerValueStats>();
        PlayerUIScript = GameObject.FindGameObjectWithTag("Start Object").GetComponent<PlayerUI>();
        

    }



    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.layer == (int)LayerStuff.LayerEnum.ENEMY)
        {
            Debug.Log("PLAYER DAMAGE");
            PlayerValueStats.Health -= collision.gameObject.GetComponent<EnemyValuesScript>().Damage;
            PlayerUIScript.UpdateUI("Health", PlayerValueStats.Health);
        }
    }
}
