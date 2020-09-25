using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidDrop : MonoBehaviour
{

    public GameObject spawnPos;
    public GameObject spawnee;
    int number = 0;
    string spawnChoice;

    int i;


    // Start is called before the first frame update
    void Start()
    {

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
