using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisplayGraCyTextXRSimpleInteractable : XRSimpleInteractable
{
    // *因為這邊只有一個功能，而且我不打算加新功能，因此就不綁 Event 了

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        VRGraduationTextUI.Instance.Show();

        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        VRGraduationTextUI.Instance.Hide();

        base.OnHoverExited(args);
    }
}
