using UnityEngine.XR.Interaction.Toolkit;

public class GoldXRGrabInteractable : XRGrabInteractable
{
    // *將黃金抓起來就會跑這個函式
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // *當黃金被抓起來，放黃金的那端一定會空下來
        VRLeftPart.Instance.MassDecrease();

        CloseHandInstances(VRTriggerTheGold.Instance , VRTriggerLiquid.Instance);

        base.OnSelectEntered(args);
    }

    // *將黃金放開就會跑這個函式
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        OpenHandInstances(VRTriggerTheGold.Instance , VRTriggerLiquid.Instance);

        base.OnSelectExited(args);
    }

    private void CloseHandInstances(params IPlayerOpenOrCloseHand[] playerOpenOrCloseHands)
    {
        foreach (var item in playerOpenOrCloseHands)
        {
            item.CloseHand();
        }
    }

    private void OpenHandInstances(params IPlayerOpenOrCloseHand[] playerOpenOrCloseHands)
    {
        foreach (var item in playerOpenOrCloseHands)
        {
            item.OpenHand();
        }
    }
}
