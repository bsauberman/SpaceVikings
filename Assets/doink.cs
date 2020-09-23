using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("round", 0);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("GameBoard");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
