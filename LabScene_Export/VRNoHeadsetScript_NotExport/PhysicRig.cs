using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRig : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private CapsuleCollider bodyCollider;
    [SerializeField] private BoxCollider ElevateFloorCollider;

    [SerializeField] private float bodyHeightMin = .5f;
    [SerializeField] private float bodyHeightMax = 2f;

    private void FixedUpdate(){
        bodyCollider.height = Mathf.Clamp(playerCameraTransform.localPosition.y , bodyHeightMin , bodyHeightMax);

        // bodyCollider.center = new Vector3(playerCameraTransform.localPosition.x , bodyCollider.height / 2 , playerCameraTransform.localPosition.z);

        bodyCollider.center = new Vector3(playerCameraTransform.localPosition.x , bodyCollider.height / 2 , playerCameraTransform.localPosition.z);

        // ElevateFloorCollider.center = new Vector3(playerCameraTransform.position.x , bodyCollider.height / 2 , playerCameraTransform.position.z);
    }
}
