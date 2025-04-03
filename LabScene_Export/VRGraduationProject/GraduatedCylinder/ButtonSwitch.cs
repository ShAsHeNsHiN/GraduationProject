using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public static ButtonSwitch Instance{get; private set;}

    private Animator _animator;

    private const string SWITCH = "Switch";

    private void Awake()
    {
        Instance = this;

        _animator = GetComponent<Animator>();

        VRTriggerLiquid.Instance.OnGoldThrowIntoLiquidFirstTime += Handle_SwitchOn;
    }

    private void Handle_SwitchOn()
    {
        _animator.SetTrigger(SWITCH);
    }
}
