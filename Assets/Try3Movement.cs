using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Try3Movement : MonoBehaviour
{

    public GameObject tile;
    public GameObject nextTile;
    public GameObject tileOption;
    public int diceRoll;

       GameObject[] tileArray = new GameObject[6];



    // Start is called before the first frame update
    IEnumerator Start()
    {
            tileArray = GameObject.FindGameObjectsWithTag("Tile"); 


        for(int i = 0; i<= diceRoll - 1; i++ ){

                yield return new WaitForSeconds(1);
                transform.position = tileArray[i].transform.position;
                
        }
    }
     

    // Update is called once per frame
    void Update()
    {

    }

}
    

