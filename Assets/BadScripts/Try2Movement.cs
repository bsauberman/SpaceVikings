using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Try2Movement : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(-10f, 0f, 0f);
    [SerializeField] float period = 4f;
    

    //todo remove from inspector
    [Range(0,1)] [SerializeField] float movementFactor;   // 0 for not moved, 1 for fully moved

    Vector3 startingPos;


    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {
            return;
        }
        if (movementFactor >= 0.9) {
            return;
        }
        float cycles = Time.time / period;  // grows continually from 0

        const float tau = Mathf.PI * 2;  // about 6.28, just to get the tau value
        float rawSinWave = Mathf.Sin(cycles * tau);


        movementFactor = rawSinWave / 2f + 0.5f;
       // Vector3 offset = movementVector * movementFactor;
       Vector3 offset =  GameObject.Find("TileOne").transform.position;;
      //  transform.position = startingPos + offset;
       transform.position = offset;
    }
}
