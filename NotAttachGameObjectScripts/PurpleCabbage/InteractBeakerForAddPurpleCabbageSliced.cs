using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBeakerForAddPurpleCabbageSliced : MonoBehaviour, IInteractable , IHasProgress
{
    [Header("PurpleCabbageSliced")]
    [SerializeField] private ItemSO purpleCabbageSlicedItemSO;
    [SerializeField] private Transform addPurpleCabbageSlicedTransform;

    private bool addPurpleCabbageSliced;

    [Header("Water")]
    [SerializeField] private ItemSO waterItemSO;
    [SerializeField] private GameObject liquidPivot;
    [SerializeField] private Transform liquidImage;
    
    private bool waterToPurpleCabbageLiquid;
    private float changeTimer;
    private float waterToPurpleCabbageLiquidTimer = 3f;

    [Header("PurpleCabbageLiquid")]
    [SerializeField] private Material purpleCabbageLiquidMat;
    [SerializeField] private ItemSO purpleCabbageLiquidItemSO;

    // About AddWater
    private float addWaterTimer;
    private Animator animator;
    private const string FILTER_PURPLE_CABBAGE_LIQUID = "filterPurpleCabbageLiquid";
    private float animeTimer = 1.1f;
    private bool addWater;
    private bool addWaterAnime;

    public event EventHandler<IHasProgress.OnPrgressChangedEventArgs> OnProgressChanged;

    private void Awake(){
        animator = GetComponent<Animator>();

        purpleCabbageSlicedItemSO.inInventory = false;
        purpleCabbageLiquidItemSO.inInventory = false;
        waterItemSO.inInventory = false;

        addPurpleCabbageSliced = false;
        addWater = false;
        addWaterAnime = false;
        waterToPurpleCabbageLiquid = false;

        addWaterTimer = animeTimer;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.J)){
            // Item.Instance.Interact(transform);
        }
        
        if(addWaterAnime){
            addWaterTimer -= Time.deltaTime;
            if(addWaterTimer < 0){
                // addWater = true;
                waterToPurpleCabbageLiquid = true;
                addWaterAnime = false;
                addWaterTimer = animeTimer;
            }
        }

        if(waterToPurpleCabbageLiquid){
            changeTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this , new IHasProgress.OnPrgressChangedEventArgs{
                progressNormalized = changeTimer / waterToPurpleCabbageLiquidTimer
            });
            if(changeTimer > waterToPurpleCabbageLiquidTimer){
                addWater = true;
                liquidImage.GetComponent<MeshRenderer>().sharedMaterial = purpleCabbageLiquidMat;

                changeTimer = 0f;
                waterToPurpleCabbageLiquid = false;
            }
        }
    }

    public string GetInteractText()
    {
        if(purpleCabbageSlicedItemSO.inInventory){
            return "Add Purple Cabbage Sliced!";
        }
        else{
            if(addPurpleCabbageSliced){
                if(waterItemSO.inInventory){
                    return "Add water";
                }
                else{
                    if(addWater){
                        return "Pick up!";
                    }
                    else{
                        if(addWaterAnime || waterToPurpleCabbageLiquid){
                            return "Please Wait a moment";
                        }

                        return "You need to pick up the Water!";
                    }
                }
            }
            else{
                return "You need to cut the Purple Cabbage First!";
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
        if(purpleCabbageSlicedItemSO.inInventory){
            SpawnPurpleCabbageSliced();
        }
        else{
            if(addPurpleCabbageSliced){
                if(waterItemSO.inInventory){
                    liquidPivot.SetActive(true);

                    animator.SetTrigger(FILTER_PURPLE_CABBAGE_LIQUID);

                    addWaterAnime = true;

                    waterItemSO.inInventory = false;

                    InventoryUI.Instance.RemoveItem(waterItemSO);
                }
                else{
                    if(addWater){
                        // this object can pick up!
                        InventoryUI.Instance.AddItem(purpleCabbageLiquidItemSO);
                        purpleCabbageLiquidItemSO.inInventory = true;
                        Destroy(gameObject);

                        // everything about interact beaker need to be clear
                        ResetValue();
                    }
                    else{
                        if(addWaterAnime || waterToPurpleCabbageLiquid){
                            // water animation is playing!
                        }

                        // player no water in inventory
                    }
                }
            }
            else{
                // player just enter the world a moment
            }
        }
    }

    private void SpawnPurpleCabbageSliced(){
        Transform purpleCabbageSlicedTransform = Instantiate(purpleCabbageSlicedItemSO.itemTransform , addPurpleCabbageSlicedTransform);

        // purpleCabbageSlicedTransform.position = addPurpleCabbageSlicedTransform.position;

        addPurpleCabbageSliced = true;
        purpleCabbageSlicedItemSO.inInventory = false;
        InventoryUI.Instance.RemoveItem(purpleCabbageSlicedItemSO);
    }

    private void ResetValue(){
        addPurpleCabbageSliced = false; 
        addWater = false;
    }
}
