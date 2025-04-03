using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private void OnTriggerStay(Collider collider){
        // print(collider);
    }

    private void OnTriggerEnter(Collider collider){
        print(collider);
        Time.timeScale = 0f;
    }
}
