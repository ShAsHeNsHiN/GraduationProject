using System;
using UnityEngine;

public class TriggerPurpleCabbageToCut : MonoBehaviour , IPlayerOpenOrCloseHand
{
    public static TriggerPurpleCabbageToCut Instance{get; private set;}

    private const string PURPLE_CABBAGE = "PurpleCabbage";

    private bool _openHand;

    public bool PurpleCabbageOnChoppingBoard {get ; private set;}

    public event Action OnPurpleCabbageCanbeChopped;

    private void Awake()
    {
        Instance = this;

        OnPurpleCabbageCanbeChopped += () => 
        {
            Debug.Log("Ready to cut!");

            PurpleCabbageOnChoppingBoard = true;
        };

        PurpleCabbageOnChoppingBoard = false;
    }

    public void CloseHand()
    {
        _openHand = false;
    }

    public void OpenHand()
    {
        _openHand = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(_openHand)
        {
            if(collider.transform.CompareTag(PURPLE_CABBAGE))
            {
                OnPurpleCabbageCanbeChopped?.Invoke();
            }
        }
    }

    private void OnDestroy()
    {
        OnPurpleCabbageCanbeChopped = null;
    }
}
