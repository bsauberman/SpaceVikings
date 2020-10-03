using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceRace : MonoBehaviour
{
    private float thrust = 10f;
    public float rotationSpeed = 100.0f;
    public int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thrust);
        float rotation = Input.GetAxis("P"+playerNum+"Y") * rotationSpeed;
        rotation *= Time.deltaTime;
        transform.Rotate(rotation,0,0);
        
    }
}
