using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Try3Movement : MonoBehaviour
{

    public int diceRoll;

       GameObject[] tileArray = new GameObject[57];
       [SerializeField] public int currPos = 0;
       Color white = new Color(1,1,1,1);



    // Start is called before the first frame update
    IEnumerator Start()
    {
            int x = 0;
            tileArray = GameObject.FindGameObjectsWithTag("Tile"); 

       
        for(int i = currPos; i<= currPos + (diceRoll - 1); i++ ){

                yield return new WaitForSeconds(1);
                transform.position = tileArray[i].transform.position;
                x = i;

                if(tileArray[i].GetComponent<Renderer>().material.color == white) {
                    print("Which way would you like to go?");
                    int y = 0;
                    if (y == 0) {
                    }
                    if (y ==1) {
                        x = x;
                    }
                }

        }
        currPos = x + 1;
    }
     

    // Update is called once per frame
    void Update()
    {

    }

}
    

