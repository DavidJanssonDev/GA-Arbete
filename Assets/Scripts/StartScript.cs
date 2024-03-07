using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenerationOfFloorClassStuff;
using UIStuff;


namespace GenerallStuff
{
    public class StartScript : MonoBehaviour
    {
        private FloorGeneration FloorGenerationScript;
        private PlayerUI PlayerUIScript;


        private PlayerHP PlayerHPScript;

        public GameObject PlayerObject;
        public GameObject GameMapObject;


        private void Awake()
        {
            PlayerUIScript = transform.GetComponent<PlayerUI>();
            FloorGenerationScript = GameMapObject.GetComponent<FloorGeneration>();
            PlayerHPScript = PlayerObject.GetComponent<PlayerHP>();
        }

        private void Start()
        {
            if (PlayerUIScript == null)
            {
                Debug.LogError("PlayerUIScript is not assigned!");
                return;
            }

            PlayerHPScript.SetUp();
            Debug.Log("GENERATING UI SCRIPT ");
            PlayerUIScript.GenerateTextMeshDictanry();
            FloorGenerationScript.Generate();
        }

    }

    class LayerStuff
    { 
        
        public enum LayerEnum { ENEMY = 7, PLAYER = 6, Room = 9, Wall = 8, Ground = 12, Respawn = 11};
    }
}