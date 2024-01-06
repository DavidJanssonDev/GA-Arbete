using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




namespace PlayerUI
{
    public class PlayerUI : MonoBehaviour
    {
        public Dictionary<string, PlayerUIText> PlayerUIElements = new();
        public List<string> TypesOfUIElements = new();
        


        public Transform PlayerStatsMapTransform;
        public List<GameObject> UIGameObjects = new();

        public void Generate()
        {
            ImportUIGamgObject();
            GenerateUIElementTextClassObject();

        }


        private void ImportUIGamgObject()
        {
            // Importing the ui elements for the player UI
            
            for (int i = 0; i < PlayerStatsMapTransform.childCount; i++)
            {
                UIGameObjects.Add(PlayerStatsMapTransform.GetChild(i).gameObject);
            }
        }

        public void GenerateUIElementTextClassObject()
        {
            foreach (GameObject UIGameObject in UIGameObjects) 
            {
                string type = UIGameObject.tag;
                TextMeshPro TextUI = UIGameObject.GetComponent<TextMeshPro>();
                RectTransform TextTransfrom = UIGameObject.GetComponent<RectTransform>();



                PlayerUIText UIElement = new(TextUI, TextTransfrom);

                PlayerUIElements.Add(type, UIElement);
            }

        }
        
    }

    public class PlayerUIText
    {
        public TextMeshPro TextUI;
        public RectTransform TextTransfrom;
        public string TypeOfUI;

        public PlayerUIText(TextMeshPro textUI, RectTransform textTransfrom)
        {
            TextUI = textUI;
            TextTransfrom = textTransfrom;
        }
    }


}

