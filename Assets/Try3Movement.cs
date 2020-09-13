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



    // Start is called before the first frame update
    IEnumerator Start()
    {
            int x = 0;
            tileArray = GameObject.FindGameObjectsWithTag("Tile"); 
            int count = 0;
            int i = currPos;
          

        for (int j = 0; j < diceRoll; j++) {

                yield return new WaitForSeconds(1);
                transform.position = tileArray[i].transform.position;
                x = i;

                if(i == 6) {
                    print("Which way would you like to go?");
                    print("A = Up    S = Right");
                    yield return waitForKeyPress(KeyCode.L);
                    if (choiceTile == 0) {
                    }
                    else if (choiceTile == 1) {
                        i = 30;
                    } 
                }
                else if (i == 10) {
                    print("Which way would you like to go?");
                    print("A = Up    S = Right");
                    yield return waitForKeyPress(KeyCode.L);
                    if (choiceTile == 0) {
                    }
                    else if (choiceTile == 1) {
                        i = 38;
                    } 
                }
                else if(i == 14) {
                    print("Which way would you like to go?");
                    print("A = Right    S = Down");
                    yield return waitForKeyPress(KeyCode.L);
                    if (choiceTile == 0) {
                    }
                    else if (choiceTile == 1) {
                        i = 45;
                    } 
                }
                else if(i == 44) {
                    print("Which way would you like to go?");
                    print("A = Right    S = Down");
                    yield return waitForKeyPress(KeyCode.L);
                    if (choiceTile == 0) {
                        i = 52;
                    }
                    else if (choiceTile == 1) {
                        i = 44;
                    } 
                }

                else if (i == 38) {
                    i = 27;
                }
                else if (i == 30) {
                    i = -1;
                }
                else if (i == 45) {
                    i = 36;
                }
                else if (i == 52) {
                    i = 22;
                }
                else if (i == 54) {
                    i = 22;
                }
                count++; 
                i++;
        }
        currPos = x + 1;
    }


    private IEnumerator waitForKeyPress(KeyCode key)
{
    bool done = false;
    while(!done)
    {
        if(Input.GetKeyDown(key))
        {
            done = true; 
        }
        yield return null; 
    }
 
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
    

