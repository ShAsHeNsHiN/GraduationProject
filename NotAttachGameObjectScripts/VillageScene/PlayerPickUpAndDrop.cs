using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpAndDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;

    private ObjectGrabbable objectGrabbable;

    private Transform objectTransform;
    private Vector3 objectLocalScale;
    private Vector3 limitScale = new Vector3(.3f , .3f , .3f);

    private void Update(){
        if(Input.GetKeyDown(KeyCode.R)){
            if(objectGrabbable == null){
                // no object grabbing , try to grab
                float pickUpDistance = 5f;
                if(Physics.Raycast(playerCameraTransform.position , playerCameraTransform.forward , out RaycastHit raycastHit , pickUpDistance , pickUpLayerMask )){

                    objectTransform = raycastHit.transform;
                    objectLocalScale = objectTransform.localScale;

                    if(raycastHit.transform.TryGetComponent<ObjectGrabbable>(out objectGrabbable)){

                        // avoid the object is too big!!
                        if(objectLocalScale.x > .3f && objectLocalScale.y > .3f && objectLocalScale.z > .3f){
                            objectTransform.localScale = limitScale;
                        }
                        else{
                            objectTransform.localScale = objectLocalScale;
                        }

                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else{
                // Carrying Something
                objectTransform.localScale = objectLocalScale;
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
    }
}
