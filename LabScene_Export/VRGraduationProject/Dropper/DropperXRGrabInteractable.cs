using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DropperXRGrabInteractable : XRGrabInteractable
{
    private static DropperXRGrabInteractable _instance;
    public static DropperXRGrabInteractable Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<DropperXRGrabInteractable>();
            }

            return _instance;
        }
    }

    [SerializeField] private Transform _leftHandTransform;
    [SerializeField] private Transform _rightHandTransform;

    private bool _dropperTouched;

    public bool HoldDropper {get ; private set;}

    private const string DETECTION_SOLUTION = "Detection Solution";
    private const string LEFT_HAND = "LeftHand";
    private const string RIGHT_HAND = "RightHand";

    private Dropper _dropper;

    private void Start()
    {
        _dropperTouched = false;

        HoldDropper = false;

        _dropper = GetComponent<Dropper>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // 若是左手接觸滴管，則將滴管設為左端點
        if(args.interactorObject.transform.parent.CompareTag(LEFT_HAND))
        {
            attachTransform = _leftHandTransform;
        }

        else
        {
            attachTransform = _rightHandTransform;
        }

        HoldDropper = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        
        HoldDropper = false;
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        // add purple cabbage liquid to DetectionSolution
        base.OnActivated(args);

        if(_dropper.HavePurpleCabbageLiquid && !_dropperTouched)
        {
            _dropper.ReleaseAnimation();

            _dropper.StartReleasing();
        }
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        // squeeze purple cabbage liquid
        base.OnDeactivated(args);

        if(TriggerToSqueezePurpleCabbageLiquid.Instance.Contact)
        {
            _dropper.SqueezeAnimation();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.CompareTag(DETECTION_SOLUTION))
        {
            _dropperTouched = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.transform.CompareTag(DETECTION_SOLUTION))
        {
            _dropperTouched = false;
        }
    }
}
