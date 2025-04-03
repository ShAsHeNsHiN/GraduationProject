using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyTest : MonoBehaviour
{
    private new Rigidbody rigidbody;

    private void Awake(){
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update(){
        print(rigidbody.velocity);
    }
}
