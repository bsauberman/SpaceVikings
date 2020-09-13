/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   // public GameObject currTile;
   public GameObject targTile;

    //public Vector3 currPos = currTile.transform.position;
   // public Vector3 targPos = targTile.transform.position;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = targTile.transform;
        target.transform.position = new Vector3(-0.470074832f,-5.62782717f,4.02470398f);
    }

    // Update is called once per frame
    void Update()
    {        
        float step = 1.0f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        
    }
}
*/