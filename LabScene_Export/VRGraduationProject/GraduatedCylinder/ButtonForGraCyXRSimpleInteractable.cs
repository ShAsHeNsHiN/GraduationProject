using System;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonForGraCyXRSimpleInteractable : XRSimpleInteractable
{
    private static ButtonForGraCyXRSimpleInteractable _instance;

    public static ButtonForGraCyXRSimpleInteractable Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<ButtonForGraCyXRSimpleInteractable>();
            }

            return _instance;
        }
    }

    private bool _waterInGraduationCylinder;

    public event Action OnAddWater;

    public event Action OnRemoveGold;

    protected override void Awake()
    {
        base.Awake();

        OnAddWater += () => 
        {
            _waterInGraduationCylinder = true;
        };
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // this is click button
        if(VRTriggerLiquid.Instance.GoldTouched)
        {
            OnRemoveGold?.Invoke();
        }

        else
        {
            if(!_waterInGraduationCylinder)
            {
                // add the water
                OnAddWater?.Invoke();
            }
        }

        base.OnSelectEntered(args);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        OnAddWater = null;

        OnRemoveGold = null;
    }
}
