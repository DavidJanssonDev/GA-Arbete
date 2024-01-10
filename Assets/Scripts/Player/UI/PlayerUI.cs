using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayerStats;

namespace UIStuff
{
    public class PlayerUI : MonoBehaviour
    {
        public GameObject PlayerObject;
        public GameObject PlayerStatsObjectParent;

        public List<FieldInfo> PlayerStatsField = new();
        public List<Transform> PlayerUITranformsList = new();

        public KeyValuePair<string, List<object>> UIStructrue = new();

        /*
         * KeyParValues {     VAD FÖR SORTS AV STAT
         *                  string : List<object>  [
         *                                          Object,
         *                                          PlayerStats variable Name,
         *                                          PlayerStats variable Value
         *                                          ]
         *                                          
         */



        public void Generate()
        {
            GetRefrenceToPlayerStats();
            GetPlayerStatsObjects();
        }

        private void GetRefrenceToPlayerStats()
        {
            PlayerValueStats playerStatsClass = GetComponent<PlayerValueStats>();
            PlayerStatsField = new List<FieldInfo>(playerStatsClass.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance));
        }

        private void GetPlayerStatsObjects()
        {
            for (int childindex = 0;  childindex < PlayerStatsObjectParent.transform.childCount; childindex++)
            {
                PlayerUITranformsList.Add(PlayerStatsObjectParent.transform.GetChild(childindex));
            }
        }

        private void CreateUIStructre()
        {
            foreach (FieldInfo playerStatvarable in PlayerStatsField)
            {

                // UIStructrue += new KeyValuePair<string, List<object>>;
            }



        }
    }
    
}