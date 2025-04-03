using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionTest : MonoBehaviour
{
    [Header("VR device Bottom Button(Primary Button)")]
    [SerializeField] private InputActionReference test1Ref;

    [Header("VR device Top Button(Secondary Button)")]
    [SerializeField] private InputActionReference test2Ref;

    [Header("VR device Menu Button(Menu Button)")]
    [SerializeField] private InputActionReference test3Ref;

    private void Start(){
        test1Ref.action.performed += TestOne;
        test2Ref.action.performed += TestTwo;
        // test3Ref.action.performed += TestThree;
    }

    private void TestOne(InputAction.CallbackContext obj){
        Debug.Log(obj);
    }

    private void TestTwo(InputAction.CallbackContext obj){
        Debug.Log(obj);
    }

    private void TestThree(InputAction.CallbackContext obj){
        Debug.Log(obj);
    }
}
