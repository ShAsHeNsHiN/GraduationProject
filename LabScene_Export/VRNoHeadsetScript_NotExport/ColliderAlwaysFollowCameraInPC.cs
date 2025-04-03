using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAlwaysFollowCameraInPC : MonoBehaviour
{   
    // this script can be close when use the VR device

    [SerializeField] private Transform playerCameraTransform;

    private CharacterController characterController;

    private void Awake(){
        characterController = GetComponent<CharacterController>();
    }

    private void Update(){
        characterController.center = playerCameraTransform.localPosition;
    }
}
