using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            transform.position += Vector3.right;
        }
    }
}
