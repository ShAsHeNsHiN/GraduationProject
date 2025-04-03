using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class MineRayInteractor : XRRayInteractor
{
    [SerializeField] private XRInteractorLineVisual xRInteractorLineVisual;

    private new void Start(){
        xRInteractorLineVisual.enabled = false;
    }

    protected override void OnUIHoverEntered(UIHoverEventArgs args){
        xRInteractorLineVisual.enabled = true;

        base.OnUIHoverEntered(args);
    }

    protected override void OnUIHoverExited(UIHoverEventArgs args){
        xRInteractorLineVisual.enabled = false;

        base.OnUIHoverExited(args);
    }
}
