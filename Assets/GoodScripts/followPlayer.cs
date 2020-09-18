using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
 [SerializeField] public GameObject player;
      public float cameraHeight = 10.0f;
      public float cameraDepth = 12.0f;
  
      void Update() {
          Vector3 pos = player.transform.position;
          pos.y += cameraHeight;
          pos.z += cameraDepth;
          transform.position = pos;
      }
  }
