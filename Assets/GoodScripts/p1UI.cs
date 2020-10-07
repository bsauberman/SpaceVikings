using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class p1UI : MonoBehaviour
{
    private int coins;

    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        coins = PlayerPrefs.GetInt("p1coins");
        
    }

    // Update is called once per frame
    void Update()
    {
        coins = PlayerPrefs.GetInt("p1coins");
        coinsText.text = "P1Coins: " + coins;
        if (Input.GetKeyDown(KeyCode.Space)) {
            coins++;
        }
        
    }
}
