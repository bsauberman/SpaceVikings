using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMaster : MonoBehaviour
{

    public int turnsVariable = 1;
    // Start is called before the first frame update
    void Start()
    {
    GameObject player2 = GameObject.Find("player2");
    player2.GetComponent<player2movement>().enabled = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
