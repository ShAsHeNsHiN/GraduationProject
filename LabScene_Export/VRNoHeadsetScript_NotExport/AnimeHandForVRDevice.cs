using UnityEngine;
using UnityEngine.InputSystem;

public class AnimeHandForVRDevice : MonoBehaviour
{
    public static AnimeHandForVRDevice Instance{get; private set;}

    [Header("for VR Controller")]
    [SerializeField] private InputActionProperty _triggerProperty;
    [SerializeField] private InputActionProperty _gripProperty;

    private Animator _animator;

    private const string TRIGGER_ANIME = "Trigger";

    private const string GRIP_ANIME = "Grip";

    private const string POKE_ANIME = "Poke";

    public bool _squeezeDropper;

    private void Awake()
    {
        Instance = this;

        _squeezeDropper = false;

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(DropperXRGrabInteractable.Instance.HoldDropper)
        {
            float triggerValue = _triggerProperty.action.ReadValue<float>();

            _animator.SetFloat(TRIGGER_ANIME , triggerValue);
        }

        else
        {
            float triggerValue = _triggerProperty.action.ReadValue<float>();

            _animator.SetFloat(TRIGGER_ANIME , triggerValue);

            float gripValue = _gripProperty.action.ReadValue<float>();

            _animator.SetFloat(GRIP_ANIME , gripValue);
        }
    }

    private void Trigger(InputAction.CallbackContext obj)
    {
        float triggerValue = obj.action.ReadValue<float>();

        _animator.SetFloat(TRIGGER_ANIME , triggerValue);
    }

    public bool SqueezeDropper()
    {
        _squeezeDropper = true;

        return _squeezeDropper;
    }

    public bool OpenDropper()
    {
        _squeezeDropper = false;

        return _squeezeDropper;
    }
}
