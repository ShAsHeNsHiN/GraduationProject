using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BeakerForAddPurpleCabbageSlicedXRGrabInteractable : XRGrabInteractable
{
    [SerializeField] private Transform beakerAppearanceTransform;
    [SerializeField] private GameObject purpleCabbageSliced;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if(!purpleCabbageSliced.activeSelf)
        {
            // purpleCabbageSliced in here is after player put the purpleCabbageSliced in the beaker . In other words , if purpleCabbageSliced active , player must cut the purpleCabbage
            beakerAppearanceTransform.SetParent(transform);
            beakerAppearanceTransform.GetComponent<MeshCollider>().convex = true;
            beakerAppearanceTransform.gameObject.SetActive(true);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if(!purpleCabbageSliced.activeSelf)
        {
            beakerAppearanceTransform.parent = null;
            beakerAppearanceTransform.GetComponent<MeshCollider>().convex = false;
        }
    }

    private void Update()
    {
        if(!purpleCabbageSliced.activeSelf)
        {
            beakerAppearanceTransform.position = transform.position;
            Quaternion parentRotation = transform.rotation;
            beakerAppearanceTransform.rotation = parentRotation;
        }
    }
}
