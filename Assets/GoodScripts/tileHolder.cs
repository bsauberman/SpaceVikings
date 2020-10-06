using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileHolder : MonoBehaviour
{
    [SerializeField] public GameObject nextTile;
    [SerializeField] public GameObject otherTile;
    [SerializeField] public int isOccupied = 0;

    [SerializeField] public int checker;

    

    // Start is called before the first frame update
    IEnumerator Start()
    {
    GameObject player1 = GameObject.Find("player1");    
    GameObject player2 = GameObject.Find("player2");
    GameObject player3 = GameObject.Find("player3");
    GameObject player4 = GameObject.Find("player4");
    yield return new WaitForSeconds(1);
       if (transform.position == player1.transform.position) {
           Debug.Log("player 1 pos set");
           player1.GetComponent<Try3Movement>().currTile = this.gameObject;
       }
       if (transform.position == player2.transform.position) {
           Debug.Log("player 2 pos set");
           player2.GetComponent<Try3Movement>().currTile = this.gameObject;
       }
       if (transform.position == player3.transform.position) {
           Debug.Log("player 3 pos set");
           player3.GetComponent<Try3Movement>().currTile = this.gameObject;
       }
       if (transform.position == player4.transform.position) {
           Debug.Log("player 4 pos set");
           player4.GetComponent<Try3Movement>().currTile = this.gameObject;
       }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
