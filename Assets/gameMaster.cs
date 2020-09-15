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

    GameObject player1 = GameObject.Find("player1");    
    GameObject player2 = GameObject.Find("player2");
    GameObject player3 = GameObject.Find("player3");
    GameObject player4 = GameObject.Find("player4");

   if (turnVariable == 1) {
        player1.GetComponent<Try3Movement>().enabled = true;
   }

    if (turnVariable == 2){
            player1.GetComponent<Try3Movement>().enabled = false;
            player2.GetComponent<Try3Movement>().enabled = true;
        }
    if (turnVariable == 3) {
            player2.GetComponent<Try3Movement>().enabled = false;
            player3.GetComponent<Try3Movement>().enabled = true;
    }
    if (turnVariable == 4) {
            player3.GetComponent<Try3Movement>().enabled = false;
            player4.GetComponent<Try3Movement>().enabled = true;
    }
    
        
    }

    // Update is called once per frame
    void Update()
    {

        if (turnVariable != y){
            turnVariable = y;
            Start();
        }
    
        
    }
}
