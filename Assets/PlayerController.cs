using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveMult; //Multiplier for movement
    public float turnMult; //Multiplier for turning
    public float jumpMult; //Jump force multiplier
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveMult /10f, 0, Input.GetAxis("Vertical") * moveMult/10f);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * turnMult);
        Debug.Log(Input.GetAxis("Mouse X"));

        if(Input.GetButton("Jump")) {
            rb.AddForce(Vector2.up * jumpMult);
        }
    }
}
