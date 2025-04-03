using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRigVer2 : MonoBehaviour
{
    [SerializeField] private Transform leftControllerTransform;
    [SerializeField] private Transform rightControllerTransform;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float bodyHeightMin = .5f;
    [SerializeField] private float bodyHeightMax = 2f;

    private void Start(){
        // print(transform.position);
        // print(playerCameraTransform.position);
    }

    private void FixedUpdate(){
        characterController.height = Mathf.Clamp(playerCameraTransform.localPosition.y , bodyHeightMin , bodyHeightMax);

        characterController.center = new Vector3(playerCameraTransform.localPosition.x , characterController.height / 2 , playerCameraTransform.localPosition.z);

        // characterController.center = new Vector3(playerCameraTransform.localPosition.x , playerCameraTransform.localPosition.y , playerCameraTransform.localPosition.z);
    }

    public void RigMatchCamera(){
        playerCameraTransform.position = transform.position;
        leftControllerTransform.position = transform.position;
        rightControllerTransform.position = transform.position;

        // print(transform.position);
        // print(playerCameraTransform.position);
    }
}
