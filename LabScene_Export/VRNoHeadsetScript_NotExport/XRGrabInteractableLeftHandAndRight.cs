using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableLeftHandAndRight : XRGrabInteractable
{
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    private const string IDK_WHY = "Left Controller";

    private void Start(){

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args){
        if(args.interactableObject.transform.CompareTag("LeftHand")){
            attachTransform = leftHand;
        }

        if(args.interactableObject.transform.CompareTag("RightHand")){
            attachTransform = rightHand;
        }

        base.OnSelectEntered(args);
    }
}
