using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerStats
{
    public class PlayerValueStats : MonoBehaviour
    {
        public int Health = 10;
        
        public int Damage = 1;
        public float MovementSpeed = 5f;
        public float ReoloadTime = 1f;
        public Vector3 RespawnPoint = Vector3.zero;
        public bool GameOver = false;
    }

}
