using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRTriggerLiquid : MonoBehaviour , IPlayerOpenOrCloseHand
{
    private static VRTriggerLiquid _instance;
    public static VRTriggerLiquid Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<VRTriggerLiquid>();
            }

            return _instance;
        }
    }

    private const string _14KGold_TAG = "_14KGold";
    private const string _18KGold_TAG = "_18KGold";

    private bool _openHand;

    public bool GoldTouched {get ; private set;}
    
    private bool _hasThrowGoldBefore;

    public event Action OnRemoveGold;

    public event Action<Collider> OnGoldTriggerLiquid;

    public event Action OnGoldThrowIntoLiquidFirstTime;

    public event Action OnGoldThrowIntoLiquid;

    private void Awake()
    {
        GoldTouched = default;

        _hasThrowGoldBefore = default;

        ButtonForGraCyXRSimpleInteractable.Instance.OnRemoveGold += Handle_RemoveGold;

        ButtonForGraCyXRSimpleInteractable.Instance.OnRemoveGold += Handle_ResetGoldTouchLiquid;

        VRChooseGoldUI.Instance.OnPreviousGoldInGraduationCylinderRemoved += Handle_RemoveGold;

        OnGoldTriggerLiquid += Handle_GoldTriggerLiquid;

        OnGoldThrowIntoLiquidFirstTime += () => 
        {
            _hasThrowGoldBefore = true;
        };
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(_openHand)
        {
            if(collider.transform.CompareTag(_14KGold_TAG) || collider.transform.CompareTag(_18KGold_TAG))
            {
                OnGoldTriggerLiquid?.Invoke(collider);

                // 非首次丟入黃金
                if(_hasThrowGoldBefore)
                {
                    // a gold is throw into the water
                    OnGoldThrowIntoLiquid?.Invoke();
                }

                // 首次丟入黃金
                else
                {
                    // a gold is throw into the water
                    OnGoldThrowIntoLiquidFirstTime?.Invoke();
                }
            }
        }
    }

    private void Handle_GoldTriggerLiquid(Collider collider)
    {
        if(collider.TryGetComponent(out GoldXRGrabInteractable component))
        {
            // *讓黃金不可再抓取
            component.enabled = false;

            component.transform.SetParent(transform);

            GoldTouched = true;
        }
    }

    private void Handle_RemoveGold()
    {
        foreach (Transform child in transform)
        {
            // When spawnGold Trigger the GraCy liquid  
            if(child.transform.CompareTag(_14KGold_TAG) || child.transform.CompareTag(_18KGold_TAG))
            {
                Destroy(child.gameObject);

                OnRemoveGold?.Invoke();
            }
        }
    }

    private void Handle_ResetGoldTouchLiquid()
    {
        GoldTouched = false;
    }

    public void OpenHand()
    {
        _openHand = true;
    }

    public void CloseHand()
    {
        _openHand = false;
    }

    private void OnDestroy()
    {
        OnRemoveGold = null;

        OnGoldTriggerLiquid = null;

        OnGoldThrowIntoLiquidFirstTime = null;

        OnGoldThrowIntoLiquid = null;
    }
}
