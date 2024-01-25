using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [Header("SCENE SETTINGS")]
    public int loadedSceneIndex;


   public void ResetSceen()
   {
        SceneManager.LoadScene(loadedSceneIndex);
   }
}
