using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidDrop : MonoBehaviour
{

    public GameObject spawnPos;
    public GameObject spawnee;
    int number = 0;
    string spawnChoice;

    public GameObject player1;

    int i;


    // Start is called before the first frame update
    void Start()
    {
       // GameObject player1 = GameObject.Find("player1");    
        GameObject player2 = GameObject.Find("player2");
        GameObject player3 = GameObject.Find("player3");
        GameObject player4 = GameObject.Find("player4");
        GameObject p1Pos = GameObject.Find("p1Pos");
        GameObject p2Pos = GameObject.Find("p2Pos");
        GameObject p3Pos = GameObject.Find("p3Pos");
        GameObject p4Pos = GameObject.Find("p4Pos");

        Instantiate(player1, p1Pos.transform.position, p1Pos.transform.rotation);

    }


    // Update is called once per frame
    void Update()
    {
        number = Random.Range(0,8);
        spawnChoice = "Spawner" + number;
        spawnPos = GameObject.Find(spawnChoice);
        if (Time.time>i){               
            Instantiate(spawnee,spawnPos.transform.position, spawnPos.transform.rotation);
            i+=1;
        }
    }
}
