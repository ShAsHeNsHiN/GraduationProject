using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentificationGoldBoxAnime : MonoBehaviour
{
    public static IdentificationGoldBoxAnime Instance{get; private set;}

    private const string IDENTIFICATION = "Identification";
    private const string APPEAR = "Appear";
    private const string IDLE = "Idle";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        Instance = this;
    }

    private void Start()
    {
        VRTriggerIdentificationGold.Instance.OnLevelClear += Handle_Hide;

        VRTriggerIdentificationGold.Instance.OnTouchedGameObjectStop += Handle_IdentificationAnime;
    }

    public void AppearAnime()
    {
        _animator.SetTrigger(APPEAR);
    }

    private void Handle_IdentificationAnime(Collider collider)
    {
        _animator.SetTrigger(IDENTIFICATION);
    }

    public void IdleAnime()
    {
        _animator.SetTrigger(IDLE);
    }

    private void Handle_Hide()
    {
        gameObject.SetActive(false);
    }
}
