using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime = 10.0f;

    // Update is called once per frame
    void Update()
    {
        if(lifetime > 0) {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0) {
                Destruction();
            }
        }
        if (this.transform.position.y <= -20) {
            Destruction();
        }
    
    }

    void Destruction() {
        Destroy(this.gameObject);
    }
}
