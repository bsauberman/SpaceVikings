﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("round", 0);
        PlayerPrefs.DeleteAll();
        
    }

    public void Loadscene(){
        SceneManager.LoadScene("GameBoard");
    }

    public void QuitGame() {
        Debug.Log("Quit!!!!");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}