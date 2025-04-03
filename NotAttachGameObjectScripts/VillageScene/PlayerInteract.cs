using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayerMask;

    private Transform playerCameraRoot;

    private LayerMask layerMask;

    private const string PCR = "PlayerCameraRoot";

    private void Awake(){
        // playerCameraRoot = transform.Find(PCR).transform;
    }

    private void Start(){
        
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            IInteractable interactable = GetNPCInteractableObject();
            if(interactable != null){
                interactable.Interact(transform);
            }
        }
    }

    public IInteractable GetNPCInteractableObject(){
        List<IInteractable> interactableList = new List<IInteractable>();
        
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position , interactRange);
        foreach(Collider collider in colliderArray){
            if(collider.TryGetComponent(out IInteractable interactable)){
                interactableList.Add(interactable);
                // Debug.Log(interactable);
            }
        }   

        IInteractable closetNPCInteactable = null;
        foreach(IInteractable interactable in interactableList){
            if(closetNPCInteactable == null){
                closetNPCInteactable = interactable;
            }
            else{
                if(Vector3.Distance(transform.position , interactable.GetTransform().position) < Vector3.Distance(transform.position , closetNPCInteactable.GetTransform().position)){
                    // closer
                    closetNPCInteactable = interactable;    
                }
            }
        }

        return closetNPCInteactable;
    }

    // private void OnDrawGizmosSelected() {
    //     Gizmos.color = Color.red;
    //     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
    //     Gizmos.DrawWireSphere(transform.position , 2f);
    // }
}
