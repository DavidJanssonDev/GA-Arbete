using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextInfo : MonoBehaviour
{
    private PlayerValueStats _playerStats;
    private Text _text;
    

    // Start is called before the first frame update
    void Awake()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerValueStats>();    
        _text = GameObject.FindGameObjectWithTag("Health tag").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $"Health {_playerStats.Health}";
    }
}
