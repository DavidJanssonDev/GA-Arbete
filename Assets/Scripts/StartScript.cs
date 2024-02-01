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

        [SerializeField] private GameObject _PlayerObject;
        [SerializeField] private GameObject _GameMapObject;


        private void Awake()
        {
            PlayerUIScript = _PlayerObject.GetComponent<PlayerUI>();
            FloorGenerationScript = _GameMapObject.GetComponent<FloorGeneration>();
            PlayerHPScript = _PlayerObject.GetComponent<PlayerHP>();
        }

        private void Start()
        {


            PlayerHPScript.SetUp();
            PlayerUIScript.GenerateTextMeshDictanry();
            FloorGenerationScript.Generate();
            
        }

    }

    class LayerStuff
    { 
        
        public enum LayerEnum { ENEMY = 7, PLAYER = 6, Room = 9, Wall = 10, Ground = 12, Respawn = 11};
    }
}