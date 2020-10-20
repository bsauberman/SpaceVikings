using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour
{

    public int turnVariable = 1;
    public int y = 1;

    public Text minigameUI;
    int minigameSelection;

    // Start is called before the first frame update
    void Start()
    {
       //  DontDestroyOnLoad(this.gameObject);

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
        LoadGameStuff(player1, player2, player3, player4);
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
        player4Camera.GetComponent<Camera>().enabled = false;
        minigameSelection = Random.Range(1,3);
        minigameUI.text = "Mini Game " + minigameSelection + " Chosen";
        Invoke ("LoadMiniGame", 1.0f);
    }
    if (turnVariable == 6) {
        Invoke ("LoadGameBoard", 1.0f);
        y = 1;
    }
    
    
    }

    void LoadGameStuff(GameObject player1, GameObject player2, 
    GameObject player3, GameObject player4) {
        player1.transform.position = new Vector3((PlayerPrefs.GetFloat("p1x", 5)), 
            (PlayerPrefs.GetFloat("p1y", 16)),(PlayerPrefs.GetFloat("p1z", -142)));

        player2.transform.position = new Vector3((PlayerPrefs.GetFloat("p2x", -2)), 
            (PlayerPrefs.GetFloat("p2y", 16)),(PlayerPrefs.GetFloat("p2z", -142)));                                                                             

        player3.transform.position = new Vector3((PlayerPrefs.GetFloat("p3x", -5)), 
            (PlayerPrefs.GetFloat("p3y", 16)),(PlayerPrefs.GetFloat("p3z", -142)));

         player4.transform.position = new Vector3((PlayerPrefs.GetFloat("p4x", -8)), 
            (PlayerPrefs.GetFloat("p4y", 16)),(PlayerPrefs.GetFloat("p4z", -142)));
    }

    void LoadMiniGame() {
        SceneManager.LoadScene("Minigame" + minigameSelection);
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
