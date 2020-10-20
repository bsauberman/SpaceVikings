using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterSelect : MonoBehaviour
{
    public GameObject viking;
    public GameObject cube;

    public GameObject rover;

    public int playerNum;

    public int characterInt = 1;
    // Start is called before the first frame update
    void Start()
    {
        viking.SetActive(false);
            cube.SetActive(false);
            rover.SetActive(true);

        
    }

    public void Next(){

        switch (characterInt){

            case 1: 
            viking.SetActive(true);
            cube.SetActive(false);
            rover.SetActive(false);
            characterInt++;
            break;
            case 2:
            viking.SetActive(false);
            cube.SetActive(true);
            rover.SetActive(false);
            characterInt++;
            break;
            case 3:
            viking.SetActive(false);
            cube.SetActive(false);
            rover.SetActive(true);
            characterInt = 1;
            break;
            default:
            if(characterInt >= 3) {
                characterInt = 1;
            }
            break;


        }


    }

    public void Previous() {
        switch (characterInt){

            case 1: 
            viking.SetActive(true);
            cube.SetActive(false);
            rover.SetActive(false);
            characterInt = 3;
            break;
            case 2:
            viking.SetActive(false);
            cube.SetActive(true);
            rover.SetActive(false);
            characterInt--;
            break;
            case 3:
            viking.SetActive(false);
            cube.SetActive(false);
            rover.SetActive(true);
            characterInt--;
            break;
            default:
            if(characterInt <= 1) {
                characterInt = 1;
            }
            break;


        }

    }

    public void Confirm(){
        PlayerPrefs.SetInt("p"+playerNum+"Char", characterInt);
        Debug.Log("Player 1 Set");
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetInt("p1Char") == 1) {
            SceneManager.LoadScene("GameBoard");
        }
        
    }
}
