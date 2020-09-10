using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Try3Movement : MonoBehaviour
{

    public int diceRoll;

       GameObject[] tileArray = new GameObject[57];
       [SerializeField] public int currPos = 0;

       [SerializeField] public int choiceTile = 0;

//       Color white = new Color(1,1,1,1);


    // Start is called before the first frame update
    IEnumerator Start()
    {
            int x = 0;
            tileArray = GameObject.FindGameObjectsWithTag("Tile"); 
            int count = 0;

       
        for(int i = currPos; i<= currPos + (diceRoll - 1); i++ ){

                yield return new WaitForSeconds(1);
                transform.position = tileArray[i].transform.position;
                x = i;

                if(i == 6) {
                    print("Which way would you like to go?");
                    print("A = Up    S = Right");
                    yield return new WaitForSeconds(3);
                    if (choiceTile == 0) {
                    }
                    else if (choiceTile == 1) {
                        diceRoll = diceRoll - count;
                        i = 30;
                        currPos = 30;
                    } 
                    
                }
                count++; 

        }
        currPos = x + 1;
    }

     

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown (KeyCode.A))) {
            choiceTile = 0;
        } else if ((Input.GetKeyDown (KeyCode.S))) {
            choiceTile = 1;
        }

    }

}
    

