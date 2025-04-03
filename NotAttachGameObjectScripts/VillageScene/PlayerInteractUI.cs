using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerObject;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactText;

    private void Update(){
        if(playerInteract.GetNPCInteractableObject() != null){
            Show(playerInteract.GetNPCInteractableObject());
        }
        else{
            Hide();
            // NPCUI.Instance.Hide();
        }
    }

    private void Show(IInteractable interactable){
        containerObject.SetActive(true);
        interactText.SetText(interactable.GetInteractText());
    }

    private void Hide(){
        containerObject.SetActive(false);
    }
}
