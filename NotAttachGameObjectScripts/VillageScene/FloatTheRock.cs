using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FloatTheRock : MonoBehaviour , IInteractable
{
    public static FloatTheRock Instance{get; private set ;}

    private Animator animator;

    private const string FTR = "Float";

    private bool state;

    private void Awake(){
        Instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start(){
        // animator.SetFloat(NPFY , transform.position.y);
    }

    private void ToggleFloating(){
        state = !state;
        animator.SetBool(FTR , state);
    }

    public void Interact(Transform interactTransform){
        ToggleFloating();
    }

    public string GetInteractText(){
        if(state){
            return "StopIt!!";
        }
        else{
            return "MagicTime!!";
        }
    }

    public Transform GetTransform(){
        return transform;
    }
}

