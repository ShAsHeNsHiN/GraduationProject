using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenu : MonoBehaviour
{
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private GameObject handMenuCanvas;

    private void Update(){
        if(Physics.Raycast(transform.position , transform.forward , out RaycastHit raycastHit , float.MaxValue , interactionLayer)){
            handMenuCanvas.SetActive(true);
        }

        else{
            handMenuCanvas.SetActive(false);
        }
    }
}
