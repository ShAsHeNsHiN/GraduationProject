using System;
using UnityEngine;

public class LeaveExperience : MonoBehaviour
{
    private static LeaveExperience _instance;
    public static LeaveExperience Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<LeaveExperience>();
            }

            return _instance;
        }
    }

    public event Action OnPlayerTriggerEnter;

    private void OnTriggerEnter(Collider collider)
    {
        // sure the object is player
        if(collider.TryGetComponent(out CharacterController _))
        {
            Time.timeScale = 0f;

            OnPlayerTriggerEnter?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnPlayerTriggerEnter = null;
    }
}
