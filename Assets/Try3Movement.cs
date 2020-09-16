using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Try3Movement : MonoBehaviour
{

    public int diceRoll;
    public bool round = true;
        

    // Player Information
    [SerializeField] public int coins = 0;
       
    [SerializeField] public int currPos = 0;

    [SerializeField] public int choiceTile = 0;

    // Colors
       Color blue = new Color (0,0,1,1);
       Color red = new Color (1,0,0,1);
       Color white = new Color (1,1,1,1);

       public GameObject currTile;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        currTile = GameObject.Find("StartingBlock");
      
while (round) {            
            int i = currPos;
            bool turn = true;
          
while (turn) {
        for (int j = 0; j < diceRoll; j++) {
            if ( currTile.GetComponent<tileHolder>().checker == 1) {
                print("Which way would you like to go?");
                print("A = Up    S = Right");
                yield return waitForKeyPress(KeyCode.L);
                if (choiceTile == 0) {
                    yield return new WaitForSeconds(1);
                    transform.position = currTile.GetComponent<tileHolder>().nextTile.transform.position;
                    currTile = currTile.GetComponent<tileHolder>().nextTile;
                    }
                    else if (choiceTile == 1) {
                        yield return new WaitForSeconds(1);
                        transform.position = currTile.GetComponent<tileHolder>().otherTile.transform.position;
                        currTile = currTile.GetComponent<tileHolder>().otherTile;
                    } 
            }
            else {
            yield return new WaitForSeconds(1);
            transform.position = currTile.GetComponent<tileHolder>().nextTile.transform.position;
            currTile = currTile.GetComponent<tileHolder>().nextTile;
            }
            currPos++;

        }
        turn = false;
    }

    checkTileAction(currTile);
    round = false;
    GameObject.Find("GameMaster").GetComponent<gameMaster>().y ++;
   
}

}

private void checkTileAction(GameObject currTile) {

    if (currTile.tag == "Tile") {
        coins = coins + 3;
    }


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
    

