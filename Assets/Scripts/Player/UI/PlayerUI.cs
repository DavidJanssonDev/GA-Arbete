using System.Collections;
using System.Collections.Generic;
using GenerallStuff;
using UnityEngine;
using TMPro;
using PlayerStats;


namespace UIStuff
{
    public class PlayerUI : MonoBehaviour
    {
        private PlayerValueStats PlayerStatsScript;
        private StartScript SetUpScript;
        public List<Transform> TextObjectList = new();
        public Dictionary<string, TextMeshProUGUI> TextObjectDictanry = new();


        private void Awake()
        {
            SetUpScript = transform.GetComponent<StartScript>();
            PlayerStatsScript = SetUpScript.PlayerObject.GetComponent<PlayerValueStats>();
        }

        public void GenerateTextMeshDictanry()
        {
            Debug.Log("BEGAIN GENENERATION");
            

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
            Debug.Log("UI BEGIN UPDATE");
            TextMeshProUGUI TextUI = TextObjectDictanry[TypeOfUI];
            Debug.Log(TextUI);
            TextUI.text = $"{TypeOfUI}: {newStat}";
        }
    }
    
}