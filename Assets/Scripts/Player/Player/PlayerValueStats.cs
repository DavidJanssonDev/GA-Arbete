using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValueStats : MonoBehaviour
{
    public int Health = 10;
    public float MovementSpeed = 5f;
    
    public List<GameObject> playerStatsTextObjects = new();
        
    public enum PlayerUIEnums{HEALTH = 1, DAMAGE = 2, SPEED = 3}

    public Dictionary<PlayerUIEnums, object> playerTextUITypes = new();

    public void AssainValues()
    {
        playerTextUITypes[PlayerUIEnums.HEALTH] = Health;
        playerTextUITypes[PlayerUIEnums.SPEED] = MovementSpeed;
    }



}
