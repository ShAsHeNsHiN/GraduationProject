using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPurpleCabbageSliced : MonoBehaviour
{
    public static TriggerPurpleCabbageSliced Instance{get ; private set;}

    [SerializeField] private GameObject _purpleCabbageSliced;

    public bool HasAddedPurpleCabbageSliced {get ; private set;}

    private const string PURPLECABBAGESLICED = "PurpleCabbageSliced";

    public event Action OnAddPurpleCabbageSliced;

    private void Awake()
    {
        Instance = this;

        HasAddedPurpleCabbageSliced = false;

        OnAddPurpleCabbageSliced += () => 
        {
            // for static beaker
            Debug.Log("Touched!");

            HasAddedPurpleCabbageSliced = true;
        };
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.CompareTag(PURPLECABBAGESLICED))
        {
            OnAddPurpleCabbageSliced?.Invoke();

            Destroy(collider.gameObject);
        }
    }

    private void OnDestroy()
    {
        OnAddPurpleCabbageSliced = null;
    }
}
