 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class roverMovement : MonoBehaviour
{
    public int playerNum;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    private float upThrust = 4f;
    Rigidbody rigidbody;
    public int laps = 0;

    GameObject spawnpos;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
       // yield return new WaitForSeconds(2);
        spawnpos = GameObject.Find("p"+playerNum+"spawnpos");
       // GameObject.Find("GameMaster").GetComponent<gameMaster>().y = 6;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            print("Fire");
        }
        float translation = Input.GetAxis("P"+playerNum+"Y") * speed;
        print(Input.GetAxis("P"+playerNum+"Y"));
        float rotation = Input.GetAxis("P"+playerNum+"X") * rotationSpeed;
        print(Input.GetAxis("P"+playerNum+"X"));
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(translation,0,0);
        transform.Rotate(0,rotation,0);

        if (this.transform.position.y <= 23) {
            this.transform.rotation = (spawnpos.transform.rotation);
            this.transform.position = (spawnpos.transform.position);
            this.transform.rotation = (spawnpos.transform.rotation);
        }

       /* if(Input.GetButton("Fire1")) {
            this.rigidbody.AddRelativeForce(Vector3.left * upThrust);
            print(Vector3.left * upThrust);
            laps++;
        }*/
        
    }


}



