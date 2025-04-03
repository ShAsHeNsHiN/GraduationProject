using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SixthDiceXRGrabInteractable : XRGrabInteractable
{
    public static SixthDiceXRGrabInteractable Instance{get ; private set;}

    [SerializeField] private Transform _leftAttachTransform;
    [SerializeField] private Transform _rightAttachTransform;

    private const string LEFT_HAND = "LeftHand";
    private const string RIGHT_HAND = "RightHand";

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    private void Start()
    {
        ControlSixthDice.Instance.OnDiceCanBePickedUp += Handle_DiceCanBePickedUp;

        GetComponent<XRGrabInteractable>().enabled = false;
    }

    private void Handle_DiceCanBePickedUp()
    {
        GetComponent<XRGrabInteractable>().enabled = true;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.parent.CompareTag(LEFT_HAND))
        {
            attachTransform = _leftAttachTransform;
        }

        else
        {
            attachTransform = _rightAttachTransform;
        }

        base.OnSelectEntered(args);
    }
}
