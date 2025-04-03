using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnableRigBuilder : MonoBehaviour
{
    [SerializeField] private Transform remy;

    private RigBuilder rigBuilder;

    private void Awake(){
        rigBuilder = remy.GetComponent<RigBuilder>();
    }

    private void Update(){
        float aboveRemy = .2f;
        Collider [] colliderForRemy = Physics.OverlapSphere(transform.position , aboveRemy);
        foreach(Collider collider in colliderForRemy){
            if(collider.GetComponent<RigBuilder>()){
                if(rigBuilder.enabled == false){
                    rigBuilder.enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.GetComponent<CharacterController>()){
            rigBuilder.enabled = true;
        }
    }
}
