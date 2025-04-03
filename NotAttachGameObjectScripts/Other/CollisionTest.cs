using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision collision){
        print(collision.transform.name);
    }

    private void OnCollisionStay(UnityEngine.Collision collision){
        // print("haha");
    }
}
