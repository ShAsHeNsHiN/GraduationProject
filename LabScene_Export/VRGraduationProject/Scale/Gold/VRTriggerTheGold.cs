using System;
using UnityEngine;

public class VRTriggerTheGold : MonoBehaviour , IPlayerOpenOrCloseHand
{
    public static VRTriggerTheGold Instance{get ; private set;}

    private bool _openHand;

    public event Action<Collider> OnTouchedGameObjectStop;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void OnTriggerStay(Collider collider)
    {
        if(_openHand)
        {
            OnTouchedGameObjectStop?.Invoke(collider);
        }
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
        OnTouchedGameObjectStop = null;
    }
}
