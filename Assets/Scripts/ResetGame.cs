using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [Header("SCENE SETTINGS")]
    public int PlayAgainSceneIndex;
    public int GoBackToMenuSceneIndex;

   public void ResetSceen()
   {
        SceneManager.LoadScene(PlayAgainSceneIndex);
   }
    public void MenuSceen()
    {
        SceneManager.LoadScene(GoBackToMenuSceneIndex);
    }
}
