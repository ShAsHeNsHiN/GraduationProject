using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimeHandForVRSimulator : MonoBehaviour
{
    [Header("for VR Simulator")]
    [SerializeField] private InputActionReference triggerReference;
    [SerializeField] private InputActionReference gripReference;
    [SerializeField] private InputActionReference leftControllerReference;
    [SerializeField] private InputActionReference rightControllerReference;
    [SerializeField] private InputActionReference bodyControllerReference;
    [SerializeField] private InputActionReference cycleControllerReference;

    private Animator animator;
    private const string TRIGGER_ANIME = "Trigger";
    private const string GRIP_ANIME = "Grip";

    private void Awake(){
        transform.GetComponent<AnimeHandForVRSimulator>().enabled = false;

        animator = GetComponent<Animator>();

        gripReference.action.performed += GripAnime;
        triggerReference.action.performed += TriggerAnime;
        leftControllerReference.action.performed += ResetAnime;
        rightControllerReference.action.performed += ResetAnime;
        bodyControllerReference.action.performed += ResetAnime;
        cycleControllerReference.action.performed += ResetAnime;
    }

    private void GripAnime(InputAction.CallbackContext obj){
        animator.SetFloat(GRIP_ANIME , obj.ReadValue<float>());
    }

    private void TriggerAnime(InputAction.CallbackContext obj){
        animator.SetFloat(TRIGGER_ANIME , obj.ReadValue<float>());
    }

    private void ResetAnime(InputAction.CallbackContext obj){
        animator.SetFloat(GRIP_ANIME , 0);
        animator.SetFloat(TRIGGER_ANIME , 0);
    }
}
