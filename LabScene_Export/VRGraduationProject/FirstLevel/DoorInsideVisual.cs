using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInsideVisual : MonoBehaviour
{
    public static DoorInsideVisual Instance{get ; private set;}

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        VRTriggerIdentificationGold.Instance.OnLevelClear += Handle_DestroyDoorInside;
    }

    private void Handle_DestroyDoorInside()
    {
        Destroy(gameObject);
    }
}
