using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGraduationCylinder : MonoBehaviour
{
    private static VRGraduationCylinder _instance;
    public static VRGraduationCylinder Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<VRGraduationCylinder>();
            }

            return _instance;
        }
    }

    private const string ADD_WATER = "AddWater";

    private const string REMOVE_GOLD = "RemoveGold";

    private const string ADD_GOLD_FIRST_TIME = "AddGoldFirstTime";

    private Animator _animator;

    private MeshCollider _meshCollider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _meshCollider = GetComponent<MeshCollider>();

        VRTriggerLiquid.Instance.OnRemoveGold += Handle_RemoveGoldAnime;

        ButtonForGraCyXRSimpleInteractable.Instance.OnAddWater += Handle_AddWater;

        ButtonForGraCyXRSimpleInteractable.Instance.OnRemoveGold += Handle_RemoveGoldAnime;

        VRTriggerLiquid.Instance.OnGoldThrowIntoLiquidFirstTime += Handle_AddGoldFirstTime;

        VRTriggerLiquid.Instance.OnGoldThrowIntoLiquid += Handle_ThrowGoldInsideAnime;
    }

    private void Start()
    {
        // *防止玩家先丟入黃金
        _meshCollider.convex = true;
    }

    private void Handle_AddWater()
    {
        _animator.SetTrigger(ADD_WATER);

        // *加水後才可丟入黃金
        _meshCollider.convex = false;
    }

    // *這個動畫與下方 Handle_AddGoldFirstTime 的動畫是一樣的
    private void Handle_ThrowGoldInsideAnime()
    {
        _animator.SetBool(REMOVE_GOLD , false);
    }

    private void Handle_RemoveGoldAnime()
    {
        _animator.SetBool(REMOVE_GOLD , true);
    }

    // *這個動畫與上方 Handle_ThrowGoldInsideAnime 的動畫是一樣的。
    // *這裡是我 Animator 那邊沒設計好，因此多了這個 Trigger，不過我沒打算再改。
    // *這個函式是 1 次性
    private void Handle_AddGoldFirstTime()
    {
        _animator.SetTrigger(ADD_GOLD_FIRST_TIME);
    }
}
