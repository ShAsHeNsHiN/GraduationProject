using UnityEngine.XR.Interaction.Toolkit;

public class PurpleCabbageXRGrabIneractable : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        TriggerPurpleCabbageToCut.Instance.CloseHand();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        TriggerPurpleCabbageToCut.Instance.OpenHand();
    }
}
