using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    public int playerNum;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = Vector3.zero;
        moveVector.x = -(Input.GetAxis("P"+playerNum+"X")) * 3;
        moveVector.z = (Input.GetAxis("P"+playerNum+"Y")) * 3;

        controller.Move(moveVector * Time.deltaTime);
        
    }
}
