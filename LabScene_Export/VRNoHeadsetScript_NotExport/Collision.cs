using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private Transform playerHead;
    [SerializeField] private Transform playerFeet;

    private void Update(){
        gameObject.transform.position = new Vector3(playerHead.position.x , playerFeet.position.y , playerHead.position.z);
    }
}
