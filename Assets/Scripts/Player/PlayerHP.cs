using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GenerallStuff.LayerStuff;
using UIStuff;
using PlayerStats;

public class PlayerHP : MonoBehaviour
{
    public PlayerUI PlayerUIScript;
    public PlayerValueStats PlayerValueStats;

    public void SetUp()
    {
        PlayerValueStats = GetComponent<PlayerValueStats>();
        PlayerUIScript = GetComponent<PlayerUI>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)LayerEnum.ENEMY)
        {
            int EnemyDamage = collision.transform.GetComponent<EnemyValuesScript>().Damage;
            PlayerValueStats.Health -= EnemyDamage;
            PlayerUIScript.UpdateUI("Health", PlayerValueStats.Health);
            
        }
    }



    public void PlayerIsDead()
    {

    }
}
