using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;
using PlayerStats;


namespace UIStuff
{
    public class PlayerUI : MonoBehaviour
    {
        private PlayerValueStats PlayerStatsScript; 
        public List<Transform> TextObjectList = new();
        public Dictionary<string, TextMeshProUGUI> TextObjectDictanry = new();


        public void GenerateTextMeshDictanry()
        {
            PlayerStatsScript = GetComponent<PlayerValueStats>();

            const string REMOVEDSTRING = "Player";

            List<string> keyList = new();

            foreach (Transform TextTransformObject in TextObjectList)
            {
                string TextObjectTag = TextTransformObject.tag;

                string[] splitTagString = TextObjectTag.Split();

                foreach(string splitString in splitTagString)
                {
                    if (splitString != REMOVEDSTRING) 
                    {
                        keyList.Add(splitString);
                    }
                }
            }

            for (int index = 0; index < keyList.Count; index++)
            {
                TextObjectDictanry.Add(keyList[index], TextObjectList[index].GetComponent<TextMeshProUGUI>());
            }

            SetUI();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
       
            Debug.Log(collision);

            if (collision.gameObject.CompareTag("Enemy"))
            {
                string UpdateUIType = "Health";
                int EnemyDamage = collision.transform.GetComponent<EnemyValuesScript>().Damage;
                PlayerStatsScript.Health -= EnemyDamage;
                UpdateUI(UpdateUIType, PlayerStatsScript.Health);
            }
        }
     

        private void SetUI()
        {
            List<string> keyList = new(TextObjectDictanry.Keys);
            for (int index = 0; index < keyList.Count; index++)
            {
                if (keyList[index] == "Health")
                {
                    UpdateUI("Health", PlayerStatsScript.Health);
                }
            }
        }

        private void UpdateUI(string TypeOfUI, object newStat)
        {
            TextMeshProUGUI TextUI = TextObjectDictanry[TypeOfUI];
            Debug.Log(newStat);
            TextUI.text = $"{TypeOfUI}: {newStat}";
            

        }


    }
    
}