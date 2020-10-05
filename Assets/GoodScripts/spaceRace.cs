using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceRace : MonoBehaviour
{
    private float thrust = 150f;
    public float rotationSpeed = 100.0f;
    public int playerNum;

    public int maxSpeed = 700000;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thrust * Time.deltaTime);
        float rotation = Input.GetAxis("P"+playerNum+"Y") * rotationSpeed;
        rotation *= Time.deltaTime;
        transform.Rotate(rotation,0,0);

        // if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed) {
        //     GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        // }
        
    }
}
