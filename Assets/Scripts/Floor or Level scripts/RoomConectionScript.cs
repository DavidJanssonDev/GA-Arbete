using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class RoomConectionScript : MonoBehaviour {
  
    private FloorValueScript FloorValueScript;
    public List<List<int>> RoomStructureConectionOrNot = new();

    private void Awake() {
        FloorValueScript = GetComponent<FloorValueScript>();
    }

    // Start is called before the first frame update
    void Start() {
        List<Transform> Rumlista = FloorValueScript.RoomGameObjects;
        CreateDataSctructure(Rumlista);
        DebugLogDataStrucsture(RoomStructureConectionOrNot);
        
    }



    private void DebugLogDataStrucsture(List<List<int>> data) {
        for (var rumLista = 0; rumLista < data.Count; rumLista++) {
            foreach (var varde in data[rumLista])
            {
                Debug.Log($" {rumLista} : {varde} ");
            }
        }
    }

    /* DATA STRUCTUR
     * 
     * 1 = det är en kopling mellan rummen
     * 0 = det är inte en kopling mellan rummen
     * 
     * 
     *       Rum 1 | Rum 2 | Rum 3 | Rum 4 | Rum 5
     * RUM 1  0        1      0         1      1
     * RUM 2  1        0      1         0      0
     * RUM 3  0        1      0         1      0
     * RUM 4  1        0      1         0      0
     * RUM 5  1        0      0         0      0
     * 
     * 
     */

   

    private void CreateDataSctructure(List<Transform> Rum) {
        // Creating the template of the scrute list 
        for (var radRum = 0;  radRum < Rum.Count; radRum++) {
            List<int> tempRad = new();

            for (var columRum = 0; columRum < Rum.Count; columRum++) {
                tempRad.Add(0);
            }
            RoomStructureConectionOrNot.Add(tempRad);
        }

        // Setting the values
        for (int radRumLista = 0; radRumLista < Rum.Count; radRumLista++) {

            for (int columRum = 0; columRum < Rum.Count; columRum++) {

                if (columRum != radRumLista) {

                    if (RoomStructureConectionOrNot[columRum][radRumLista] == 1) {
                        RoomStructureConectionOrNot[radRumLista][columRum] = 1;
                    } else {
                        RoomStructureConectionOrNot[radRumLista][columRum] = (int)Mathf.Round(Random.value);
                    }

                }
            }
        }

    }
}