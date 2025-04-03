using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveTeleportionRay : MonoBehaviour
{
    [Header("LeftAndRightTeleRay")]
    [SerializeField] private GameObject leftRay;
    [SerializeField] private GameObject rightRay;

    [Header("LeftAndRightActivateInput")]
    [SerializeField] private InputActionProperty leftActivate;
    [SerializeField] private InputActionProperty rightActivate;

    [Header("LeftAndRightSelect")]
    [SerializeField] private InputActionProperty leftSelect;
    [SerializeField] private InputActionProperty rightSelect;

    private void Update(){
        leftRay.SetActive(leftSelect.action.ReadValue<float>() is 0 && leftActivate.action.ReadValue<float>() > .1f);
        rightRay.SetActive(rightSelect.action.ReadValue<float>() is 0 && rightActivate.action.ReadValue<float>() > .1f);
    }
}
