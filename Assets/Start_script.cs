using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_script : MonoBehaviour
{
    [SerializeField] private int SceneIndex;

    public void PlayGameLoading()
    {
        SceneManager.LoadScene(SceneIndex);
    }

}
