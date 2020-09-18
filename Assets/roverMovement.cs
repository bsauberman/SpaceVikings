using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roverMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    [SerializeField] float upThrust = 20f;

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(translation,0,0);
        transform.Rotate(0,rotation,0);

        if(Input.GetButton("Fire1")) {
            this.rigidbody.AddRelativeForce(Vector3.left * upThrust);
        }
        

    }

}
