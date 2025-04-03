using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportRay : MonoBehaviour
{
    [SerializeField] private InputActionReference triggerRef;

    private void Start(){
        triggerRef.action.performed += Teleport;
    }

    private void Teleport(InputAction.CallbackContext obj){
        Debug.Log(obj);
    }
}
