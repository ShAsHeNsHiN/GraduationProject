using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCabinet : MonoBehaviour , IInteractable
{
    private Animator animator;

    private const string ANIME_BOOL = "Open";

    private bool open;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    public string GetInteractText()
    {
        if(open){
            return "Close The Door";
        }
        else{
            return "Open The Door";
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactTransform)
    {
        if(open){
            open = false;
            animator.SetBool(ANIME_BOOL , open);
        }
        else{
            open = true;
            animator. SetBool(ANIME_BOOL , open);
        }
    }
}
