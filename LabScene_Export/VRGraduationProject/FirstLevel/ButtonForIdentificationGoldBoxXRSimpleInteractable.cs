using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonForIdentificationGoldBoxXRSimpleInteractable : XRSimpleInteractable
{
    public static ButtonForIdentificationGoldBoxXRSimpleInteractable Instance{get ; private set;}

    public event Action OnClickButton;

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    // *戳按鈕就會跑這個函式
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        OnClickButton?.Invoke();

        base.OnSelectEntered(args);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        OnClickButton = null;
    }
}
