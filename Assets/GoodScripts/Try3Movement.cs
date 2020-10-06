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

    [SerializeField] public int dirChoice = 0;

    public GameObject currTile;
    public float speed = 7.0f;
    public GameObject target;
    public int playerNum = 0;


    // Start is called before the first frame update
    IEnumerator Start() 
    {
        coins = PlayerPrefs.GetInt("p"+playerNum+"coins");
        yield return new WaitForSeconds(1);
        if (this.currTile == null) {
            this.currTile = GameObject.Find("startingBlock");
        }
        this.target = currTile.GetComponent<tileHolder>().nextTile;
        currTile.GetComponent<tileHolder>().isOccupied--;

while (round) {            
            bool turn = true;
          
while (turn) {
        for (int j = 0; j < diceRoll; j++) {
            if ( currTile.GetComponent<tileHolder>().checker == 1) {
                print("Which way would you like to go?");
                print("A = Up    S = Right");
                yield return waitForKeyPress(KeyCode.L);
                if (dirChoice == 0) {
                    target = currTile.GetComponent<tileHolder>().nextTile;
                    yield return new WaitForSeconds(1);
                   // transform.position = currTile.GetComponent<tileHolder>().nextTile.transform.position;
                    currTile = currTile.GetComponent<tileHolder>().nextTile;
                    }
                    else if (dirChoice == 1) {
                       target = currTile.GetComponent<tileHolder>().otherTile;
                        yield return new WaitForSeconds(1);
                       // transform.position = currTile.GetComponent<tileHolder>().otherTile.transform.position;
                        currTile = currTile.GetComponent<tileHolder>().otherTile;
                    } 
            }
            else {
                target = currTile.GetComponent<tileHolder>().nextTile;
                yield return new WaitForSeconds(1);
                transform.position = currTile.GetComponent<tileHolder>().nextTile.transform.position;
                currTile = currTile.GetComponent<tileHolder>().nextTile;
            }
            currPos++;

        }
        currTile.GetComponent<tileHolder>().isOccupied++;
        PlayerPrefs.SetFloat("p"+playerNum+"x", transform.position.x);
        PlayerPrefs.SetFloat("p"+playerNum+"y", transform.position.y);
        PlayerPrefs.SetFloat("p"+playerNum+"z", transform.position.z);
        print("Player " + playerNum + " set");
        PlayerPrefs.SetInt("round", PlayerPrefs.GetInt("round") + 1);

        turn = false;
    }

    checkTileAction(currTile);
    round = false;
    PlayerPrefs.SetInt("p"+playerNum+"coins", coins);
    GameObject.Find("GameMaster").GetComponent<gameMaster>().y ++;
   
}

}

private void checkTileAction(GameObject currTile) {

    if (currTile.tag == "Blue") {
        coins = coins + 3;
    }
    if (currTile.tag == "Red") {
        coins = coins - 3;
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
            dirChoice = 0;
        } else if ((Input.GetKeyDown (KeyCode.S))) {
            dirChoice = 1;
        }

        Vector3 direction = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;


    }

}
    

