using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMaster : MonoBehaviour
{

    public int turnVariable = 1;
    public int y = 1;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    GameObject player1 = GameObject.Find("player1");    
    GameObject player2 = GameObject.Find("player2");
    GameObject player3 = GameObject.Find("player3");
    GameObject player4 = GameObject.Find("player4");
    GameObject playerCamera = GameObject.Find("playerCamera");
    GameObject player2Camera = GameObject.Find("player2Camera");
    GameObject player3Camera = GameObject.Find("player3Camera");
    GameObject player4Camera = GameObject.Find("player4Camera");

    GameObject player1rover = GameObject.Find("player1rover");

   if (turnVariable == 1) {
        player1.GetComponent<Try3Movement>().enabled = true;
        playerCamera.GetComponent<Camera>().enabled = true; 
        }
    if (turnVariable == 2){
            player1.GetComponent<Try3Movement>().enabled = false;
            player2.GetComponent<Try3Movement>().enabled = true;

            playerCamera.GetComponent<Camera>().enabled = false; 
            player2Camera.GetComponent<Camera>().enabled = true; 
        }
    if (turnVariable == 3) {
            player2.GetComponent<Try3Movement>().enabled = false;
            player3.GetComponent<Try3Movement>().enabled = true;

            player2Camera.GetComponent<Camera>().enabled = false; 
            player3Camera.GetComponent<Camera>().enabled = true; 
    }
    if (turnVariable == 4) {
            player3.GetComponent<Try3Movement>().enabled = false;
            player4.GetComponent<Try3Movement>().enabled = true;

            player3Camera.GetComponent<Camera>().enabled = false; 
            player4Camera.GetComponent<Camera>().enabled = true; 
    }
    if (turnVariable == 5) {
        player4.GetComponent<Try3Movement>().enabled = false;
        Invoke ("LoadMiniGame", 1.0f);
    }
    if (turnVariable == 6) {
        y = 1;
        Invoke ("LoadGameBoard", 1.0f);
    }
    
    
        
    }

    void LoadMiniGame() {
        SceneManager.LoadScene("Minigame1");
    }
    void LoadGameBoard() {
        SceneManager.LoadScene("GameBoard");
    }

    // Update is called once per frame
    void Update()
    {

        if (turnVariable != y){
            turnVariable = y;
            Start();
        }
        if(GameObject.Find("player1rover").GetComponent<roverMovement>().laps >= 1) {
            y = 6;
        }
    
        
    }
}
