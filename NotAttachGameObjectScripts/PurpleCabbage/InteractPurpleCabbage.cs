using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPurpleCabbage : MonoBehaviour , IInteractable , IHasProgress
{   
    public static InteractPurpleCabbage Instance{get ; private set;}

    [Header("PurpleCabbage")]
    [SerializeField] private ItemSO puprleCabbageItemSO;
    [SerializeField] private PurpleCabbageSO purpleCabbageSO;
    [SerializeField] private Transform addThePurpleCabbageTransform;

    private float cuttingNumber;
    private bool interactThisGameObject;
    private Transform pCabbageSlicedTransform;
    private Animator animator;

    private const string CUT = "Cut";

    public event EventHandler<IHasProgress.OnPrgressChangedEventArgs> OnProgressChanged;

    private void Awake()
    {
        Instance = this;
        purpleCabbageSO.cuttingFinish = false;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // purpleCabbageSO.cuttingFinish = false;
    }

    public ItemSO GetPurpleCabbageItemSO()
    {
        return puprleCabbageItemSO;
    }

    public string GetInteractText()
    {
        if(puprleCabbageItemSO.inInventory){
            // after pick up
            return "Put On!";
        }
        else{
            if(purpleCabbageSO.cuttingFinish){
                // after cut
                return "Pick up!";
            }
            else{
                if(interactThisGameObject){
                    // after put on
                    return "Cut";
                }
                else{
                    NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.PickUpThePurpleCabbage , 0f);
                    // original : player enter the world just a moment
                    // return "You need to pick up the Purple Cabbage First!";
                    return null;
                }
            }
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactTransform)
    {
        HandlePrograss();
    }

    private void HandlePrograss(){
        if(puprleCabbageItemSO.inInventory){
            // first time interact : add purpleCabbage
            interactThisGameObject = true;
            SpawnPurpleCabbage();
        }
        else{
            if(purpleCabbageSO.cuttingFinish){
                // after cut
                    pCabbageSlicedTransform.GetComponent<Item>().Interact(transform);
                    // Item.Instance.Interact(transform);

                    // everything about interact purple cabbage need to be clear
                    ResetValue();
            }
            
            else{
                if(interactThisGameObject){
                    cuttingNumber++;

                    OnProgressChanged?.Invoke(this , new IHasProgress.OnPrgressChangedEventArgs{
                        progressNormalized = cuttingNumber / purpleCabbageSO.cuttingNumber
                    });

                    animator.SetTrigger(CUT);

                    // Debug.Log(cuttingNumber);

                    if(cuttingNumber == purpleCabbageSO.cuttingNumber){
                        purpleCabbageSO.cuttingFinish = true;
                        SpawnPurpleCabbageSliced();
                    }
                }
                else{
                    // player just enter the world a moment
                }
            }
        }
    }

    private void SpawnPurpleCabbage(){
        Instantiate(purpleCabbageSO.purpleCabbageOriginTransform , addThePurpleCabbageTransform);
        puprleCabbageItemSO.inInventory = false;
        InventoryUI.Instance.RemoveItem(puprleCabbageItemSO);
    }

    private void SpawnPurpleCabbageSliced(){
        Transform purpleCabbageSlicedTransform = Instantiate(purpleCabbageSO.purpleCabbageSlicedTransform);
        purpleCabbageSlicedTransform.position = transform.position;

        pCabbageSlicedTransform = purpleCabbageSlicedTransform;

        Destroy(addThePurpleCabbageTransform.GetChild(0).gameObject);
    }

    private void ResetValue(){
        purpleCabbageSO.cuttingFinish = false;
        interactThisGameObject = false;
        puprleCabbageItemSO.inInventory = false;
        cuttingNumber = 0f;
    }
}
