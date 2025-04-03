using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathtubInteract : MonoBehaviour , IInteractable
{
    private MeshRenderer bathtubMeshRenderer;

    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material targetMaterial;

    private bool isOriginalMaterial;
    // original bool is always "false"

    private void Awake(){
        bathtubMeshRenderer = GetComponent<MeshRenderer>();
        // Debug.Log(isOriginalMaterial);
    }

    private void SetOriginalMaterial(){
        bathtubMeshRenderer.material = originalMaterial;
    }

    private void SetAnotherMaterial(){
        bathtubMeshRenderer.material = targetMaterial;
    }

    private void ToggleMaterial(){
        isOriginalMaterial = !isOriginalMaterial;
        if(isOriginalMaterial){
            SetOriginalMaterial();
        }
        else{
            SetAnotherMaterial();
        }
    }

    public void Interact(Transform interactTransform){
        ToggleMaterial();
    }

    public string GetInteractText(){
        return "I don't like this mat";
    }

    public Transform GetTransform(){
        return transform;
    }
}
