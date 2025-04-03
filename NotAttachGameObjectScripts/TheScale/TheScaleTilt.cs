using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheScaleTilt : MonoBehaviour
{
    // [Header("TheRotation")]
    // [SerializeField] private Transform originalTransform;
    // [SerializeField] private Transform leftTiltTransform;
    // [SerializeField] private Transform rightTiltTransform;

    [Header("TwoPartOfScale")]
    [SerializeField] private LeftPart leftPart;
    [SerializeField] private RightPart rightPart;

    private Animator animator;

    private const string TURN_LEFT = "TurnLeft";
    private const string TURN_RIGHT = "TurnRight";

    // private float lerpSpeed = 10f;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update(){
        if(leftPart.GetWeight() > rightPart.GetWeight()){
            animator.SetBool(TURN_LEFT , true);
            animator.SetBool(TURN_RIGHT , false);
        }
        if(leftPart.GetWeight() < rightPart.GetWeight()){
            animator.SetBool(TURN_RIGHT , true);
            animator.SetBool(TURN_LEFT , false);
        }
        if(leftPart.GetWeight() == rightPart.GetWeight()){
            animator.SetBool(TURN_LEFT , false);
            animator.SetBool(TURN_RIGHT , false);
        }
    }

    // private void OriginalPos(){
    //     transform.position = originalTransform.position;
    //     transform.rotation = originalTransform.rotation;

    //     // transform.position = Vector3.Lerp(transform.position , originalTransform.position , Time.deltaTime * lerpSpeed);

    //     // transform.rotation = Quaternion.Lerp(transform.rotation , originalTransform.rotation , Time.deltaTime * lerpSpeed);
    // }

    // private void LeftTiltPos(){
    //     transform.position = leftTiltTransform.position;
    //     transform.rotation = leftTiltTransform.rotation;

    //     // transform.position = Vector3.Lerp(transform.position , leftTiltTransform.position , Time.deltaTime * lerpSpeed);

    //     // transform.rotation = Quaternion.Lerp(transform.rotation , leftTiltTransform.rotation , Time.deltaTime * lerpSpeed);
    // }

    // private void RightTiltPos(){
    //     transform.position = rightTiltTransform.position;
    //     transform.rotation = rightTiltTransform.rotation;

    //     // transform.position = Vector3.Lerp(transform.position , rightTiltTransform.position , Time.deltaTime * lerpSpeed);

    //     // transform.rotation = Quaternion.Lerp(transform.rotation , rightTiltTransform.rotation , Time.deltaTime * lerpSpeed);
    // }

}
