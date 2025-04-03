using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Transform objectGrabPointTransform;
    private BoxCollider boxCollider;

    private Rigidbody objectRigidbody;

    private void Awake(){
        objectRigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Grab(Transform objectGrabPointTransform){
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        objectRigidbody.freezeRotation = true;
        // boxCollider.isTrigger = true;
    }

    public void Drop(){
        objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        // boxCollider.isTrigger = false;
    }

    private void FixedUpdate(){
        if(objectGrabPointTransform != null){
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position , objectGrabPointTransform.position , Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
