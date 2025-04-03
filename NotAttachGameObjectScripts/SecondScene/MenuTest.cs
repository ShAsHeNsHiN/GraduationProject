using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuTest : XRSimpleInteractable
{
    protected override void OnHoverEntered(HoverEnterEventArgs args){
        print("HOVER!");
        
        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args){
        // print("EXIT!");

        base.OnHoverExited(args);
    }
}
