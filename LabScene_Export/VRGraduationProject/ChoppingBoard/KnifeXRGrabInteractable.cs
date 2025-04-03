using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KnifeXRGrabInteractable : XRGrabInteractable
{
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;

    private const string LEFT_HAND = "LeftHand";
    private const string RIGHT_HAND = "RightHand";

    private BoxCollider boxCollider;

    private void Start()
    {
        foreach(BoxCollider collider in colliders.Cast<BoxCollider>())
        {
            boxCollider = collider;
        }

        attachTransform = leftHandTransform;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if(args.interactorObject.transform.parent.CompareTag(LEFT_HAND))
        {
            attachTransform = leftHandTransform;
        }

        else
        {
            attachTransform = rightHandTransform;
        }

        boxCollider.isTrigger = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        boxCollider.isTrigger = false;
    }
}
