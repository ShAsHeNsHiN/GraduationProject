using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportOrTouchUI : MonoBehaviour
{
    [SerializeField] private GameObject leftTeleportRayGameObject;

    [SerializeField] private InputActionReference leftActivate;
    [SerializeField] private InputActionReference leftCancel;

    [SerializeField] private XRRayInteractor leftUIRay;

    public bool isLeftRayHovering;

    private void Start(){
        // Debug.Log(gameObject.activeSelf);
    }

    private void Update(){
        isLeftRayHovering = leftUIRay.TryGetHitInfo(out _ , out _ , out _ , out _);

        // leftTeleportRayGameObject.SetActive(!isLeftRayHovering && leftCancel.action.ReadValue<float>() is 0 && leftActivate.action.ReadValue<float>() > .1f);

        leftTeleportRayGameObject.SetActive(!isLeftRayHovering);
    }
}
