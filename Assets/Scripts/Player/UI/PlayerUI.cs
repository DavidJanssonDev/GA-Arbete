using System.Collections;
using System.Collections.Generic;
using GenerallStuff;
using UnityEngine;
using TMPro;
using PlayerStats;
using static GenerallStuff.LayerStuff;


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


            List<string> keyList = new();

            foreach (Transform TextTransformObject in TextObjectList)
            {
                keyList.Add(TextTransformObject.name);
            }

            for (int index = 0; index < keyList.Count; index++)
            {
                TextObjectDictanry.Add(keyList[index], TextObjectList[index].GetComponent<TextMeshProUGUI>());
            }

            SetUI();
        }

       


        public void SetUI()
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

        public void UpdateUI(string TypeOfUI, object newStat)
        {
            TextMeshProUGUI TextUI = TextObjectDictanry[TypeOfUI];
            TextUI.text = $"{TypeOfUI}: {newStat}";
        }
    }
    
}