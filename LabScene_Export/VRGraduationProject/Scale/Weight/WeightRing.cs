using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightRing : MonoBehaviour
{
    [SerializeField] private GameObject weightRing;

    private void Update()
    {
        CharacterController characterController = ScratchPlayer();

        if(characterController != null)
        {
            // Show();
        }

        else
        {
            Hide();
        }
    }

    private CharacterController ScratchPlayer()
    {
        float interactableRange = .5f;
        
        Collider [] collider = Physics.OverlapSphere(transform.position , interactableRange);
        
        foreach(Collider collider1 in collider)
        {
            if(collider1.transform.TryGetComponent(out CharacterController characterController))
            {
                // detect which is player
                return characterController;
            }
        }

        return null;
    }

    private void Hide()
    {
        weightRing.SetActive(false);
    }

    private void Show()
    {
        weightRing.SetActive(true);
    }
}
