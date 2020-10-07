using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class p2UI : MonoBehaviour
{
    private int coins;

    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        coins = PlayerPrefs.GetInt("p2coins");
        
    }

    // Update is called once per frame
    void Update()
    {
        coins = PlayerPrefs.GetInt("p2coins");
        coinsText.text = "P2Coins: " + coins;
        if (Input.GetKeyDown(KeyCode.Space)) {
            coins++;
        }
        
    }
}
