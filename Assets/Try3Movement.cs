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

    public GameObject currTile;
    public float speed = 7.0f;
    public GameObject target;



    // Start is called before the first frame update
    IEnumerator Start()
    {
        currTile = GameObject.Find("StartingBlock");
        target = currTile.GetComponent<tileHolder>().nextTile;



while (round) {            
            bool turn = true;
          
while (turn) {
        for (int j = 0; j < diceRoll; j++) {
            if ( currTile.GetComponent<tileHolder>().checker == 1) {
                print("Which way would you like to go?");
                print("A = Up    S = Right");
                yield return waitForKeyPress(KeyCode.L);
                if (choiceTile == 0) {
                    target = currTile.GetComponent<tileHolder>().nextTile;
                    yield return new WaitForSeconds(1);
                   // transform.position = currTile.GetComponent<tileHolder>().nextTile.transform.position;
                    currTile = currTile.GetComponent<tileHolder>().nextTile;
                    }
                    else if (choiceTile == 1) {
                       target = currTile.GetComponent<tileHolder>().otherTile;
                        yield return new WaitForSeconds(1);
                       // transform.position = currTile.GetComponent<tileHolder>().otherTile.transform.position;
                        currTile = currTile.GetComponent<tileHolder>().otherTile;
                    } 
            }
            else {
            target = currTile.GetComponent<tileHolder>().nextTile;
            yield return new WaitForSeconds(1);
           // transform.position = currTile.GetComponent<tileHolder>().nextTile.transform.position;
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
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.transform.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }

        if ((Input.GetKeyDown (KeyCode.A))) {
            choiceTile = 0;
        } else if ((Input.GetKeyDown (KeyCode.S))) {
            choiceTile = 1;
        }

    }

}
    

