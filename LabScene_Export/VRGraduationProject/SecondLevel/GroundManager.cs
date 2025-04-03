using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance{get ; private set;}

    private void Awake()
    {
        LeaveExperience.Instance.OnPlayerTriggerEnter += Handle_Hide;

        Instance = this;
    }

    private void Handle_Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
