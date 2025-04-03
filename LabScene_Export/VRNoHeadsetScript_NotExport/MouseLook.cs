using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private InputActionReference horizonalLook;
    [SerializeField] private InputActionReference verticalLook;

    [SerializeField] private float lookSpeed;
    [SerializeField] private Transform playerCameraTransform;

    private float pitch;
    private float yaw;

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;

        horizonalLook.action.performed += Handle_HorizonalLook;

        verticalLook.action.performed += Handle_VerticalLook;
    }

    private void Handle_HorizonalLook(InputAction.CallbackContext obj){
        yaw += obj.ReadValue<float>();
        transform.localRotation = Quaternion.AngleAxis(yaw * lookSpeed , Vector3.up);
    }

    private void Handle_VerticalLook(InputAction.CallbackContext obj){
        pitch -= obj.ReadValue<float>();
        playerCameraTransform.localRotation = Quaternion.AngleAxis(pitch * lookSpeed , Vector3.right);
    }
}
