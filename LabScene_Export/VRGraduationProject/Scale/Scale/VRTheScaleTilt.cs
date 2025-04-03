using System;
using UnityEngine;

public class VRTheScaleTilt : MonoBehaviour
{
    private Animator _animator;

    private const string TURN_LEFT = "TurnLeft";
    private const string TURN_RIGHT = "TurnRight";

    public event Action OnLeftPartHeavier;

    public event Action OnRightPartHeavier;

    public event Action OnBothPartEqual;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        OnLeftPartHeavier += () => 
        {
            _animator.SetBool(TURN_LEFT , true);

            _animator.SetBool(TURN_RIGHT , false);
        };

        OnRightPartHeavier += () => 
        {
            _animator.SetBool(TURN_RIGHT , true);

            _animator.SetBool(TURN_LEFT , false);
        };

        OnBothPartEqual += () => 
        {
            _animator.SetBool(TURN_LEFT , false);

            _animator.SetBool(TURN_RIGHT , false);
        };
    }

    private void Update()
    {
        TwoPartCompare();
    }

    /// <summary>
    /// 天秤兩端質量比較
    /// </summary>
    private void TwoPartCompare()
    {
        if(VRLeftPart.Instance.Mass > VRRightPart.Instance.Mass)
        {
            OnLeftPartHeavier?.Invoke();
        }

        else if(VRLeftPart.Instance.Mass < VRRightPart.Instance.Mass)
        {
            OnRightPartHeavier?.Invoke();
        }

        else
        {
            OnBothPartEqual?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnLeftPartHeavier = null;

        OnRightPartHeavier = null;

        OnBothPartEqual = null;
    }
}
