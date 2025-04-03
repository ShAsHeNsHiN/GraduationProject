using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBeakerForTheBoiledPurpleCabbageLiquid : MonoBehaviour, IInteractable , IHasProgress
{
    [Header("PurpleCabbageLiquid")]
    [SerializeField] private ItemSO purpleCabbageLiquidItemSO;
    [SerializeField] private GameObject liquidPivotOfBeaker;

    [Header("Dropper")]
    [SerializeField] private ItemSO dropperItemSO;
    [SerializeField] private DropperTransformAndSpriteSO nthInDropperSO;
    [SerializeField] private DropperTransformAndSpriteSO sthInDropperSO;
    [SerializeField] private Transform addDropperTransform;
    

    private const string LIQUID_PIVOT_OF_DROPPER = "LiquidPivot";

    private Transform liquidPivotOfDropperTransform;
    private Transform dropperTransform;

    private Animator animator;

    private float squeezePurpleCabbageLiquidTimer;
    private float filterPurpleCabbageLiquidTimer;

    private float animeTimer = 1.1f;

    private const string FILTER_PURPLE_CABBAGE_LIQUID = "filterPurpleCabbageLiquid";
    private const string SQUEEZE_PURPLE_CABBAGE_LIQUID = "squeezePurpleCabbageLiquid";

    private bool filterPurpleCabbageLiquid;
    private bool putTheDropper;
    private bool Squeeze;
    private bool filterPurpleCabbageLiquidAnime;
    private bool squeezePurpleCabbageLiquidAnime;

    public event EventHandler<IHasProgress.OnPrgressChangedEventArgs> OnProgressChanged;

    private void Awake(){
        // this is the original value
        purpleCabbageLiquidItemSO.inInventory = false;
        dropperItemSO.inInventory = false;
        filterPurpleCabbageLiquid = false;
        putTheDropper = false;
        Squeeze = false;

        squeezePurpleCabbageLiquidAnime = false;
        filterPurpleCabbageLiquidAnime = false;

        dropperItemSO._name = nthInDropperSO.dropperName;
        dropperItemSO.icon = nthInDropperSO.dropperSprite;
        dropperItemSO.itemTransform = nthInDropperSO.dropperTransform;

        animator = GetComponent<Animator>();

        squeezePurpleCabbageLiquidTimer = animeTimer;
        filterPurpleCabbageLiquidTimer = animeTimer;

        // if this script in "InteractDetectionSolution" isn't take effect , open below script!
        // dropperItemSO.itemTransform = nothingInDropperTransform;
        
    }

    private void Update(){
        if(squeezePurpleCabbageLiquidAnime){
            squeezePurpleCabbageLiquidTimer -= Time.deltaTime;
            if(squeezePurpleCabbageLiquidTimer < 0){
                Squeeze = true;
                squeezePurpleCabbageLiquidAnime = false;
                squeezePurpleCabbageLiquidTimer = animeTimer;
            }
        }

        if(filterPurpleCabbageLiquidAnime){
            filterPurpleCabbageLiquidTimer -= Time.deltaTime;
            if(filterPurpleCabbageLiquidTimer < 0){
                filterPurpleCabbageLiquid = true;
                filterPurpleCabbageLiquidAnime = false;
                filterPurpleCabbageLiquidTimer = animeTimer;
            }
        }
    }

    public string GetInteractText()
    {
        if(filterPurpleCabbageLiquid && dropperItemSO.icon == nthInDropperSO.dropperSprite){
            // player add the boiled liquid
            if(dropperItemSO.inInventory){
                return "Put the dropper in the Purple Cabbage Liquid!";
            }
            else{

                if(putTheDropper){
                    if(Squeeze){
                        return "Pick up!";
                    }
                    else{
                        if(squeezePurpleCabbageLiquidAnime){
                            return "Please Wait a moment!";
                        }

                        return "Squeeze The Purple Cabbage Liquid!";
                    }
                }

                return "You need to pick up the dropper!";
            }
        }
        else{
            // player must need the boiled liquid then can interact this object
            if(purpleCabbageLiquidItemSO.inInventory){
                

                return "filter the boiled Purple Cabbage Liquid!";
            }
            if(dropperItemSO.icon == sthInDropperSO.dropperSprite){
                return "You already have the Purple Cabbage Liquid!";
            }
            else{
                if(filterPurpleCabbageLiquidAnime){
                    return "Please wait a moment!";
                }
                // player enter the world just a moment
                return "Need The Boiled Purple Cabbage Liquid!";
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
        if(filterPurpleCabbageLiquid && dropperItemSO.icon == nthInDropperSO.dropperSprite){
                // player add the boiled liquid
                if(dropperItemSO.inInventory){
                    // Put the dropper in the Purple Cabbage Liquid
                    SpawnDropper();
                }
                else{
                    if(putTheDropper){
                        if(Squeeze){
                            // player can pick up and the dropper icon need to change
                            dropperItemSO.icon = sthInDropperSO.dropperSprite;
                            dropperItemSO.itemTransform = sthInDropperSO.dropperTransform;
                            dropperItemSO._name = sthInDropperSO.dropperName;

                            dropperTransform.GetComponent<Item>().Interact(transform);

                            // everything about interact beaker need to be clear , but the purple cabbage liquid is still exist
                            putTheDropper = false;
                            Squeeze = false;
                        }

                        else{
                            // Squeeze The Purple Cabbage Liquid!
                            liquidPivotOfDropperTransform.gameObject.SetActive(true);
                            dropperTransform.GetComponent<Animator>().SetTrigger(SQUEEZE_PURPLE_CABBAGE_LIQUID); 
                            // Squeeze = true;

                            squeezePurpleCabbageLiquidAnime = true;
                        }
                    }
                }
            }
        else{
            // mean player can add the purple cabbage liquid
            if(purpleCabbageLiquidItemSO.inInventory){
                liquidPivotOfBeaker.SetActive(true);
                // Vector3 waterVolume = new(1 , 1 , 2);
                // liquidPivotOfBeaker.transform.localScale = waterVolume;
                animator.SetTrigger(FILTER_PURPLE_CABBAGE_LIQUID);

                filterPurpleCabbageLiquidAnime = true;

                // filterPurpleCabbageLiquid = true;

                InventoryUI.Instance.RemoveItem(purpleCabbageLiquidItemSO);
                purpleCabbageLiquidItemSO.inInventory = false;
            }

            if(dropperItemSO.icon == nthInDropperSO.dropperSprite){
                // player already has the Purple Cabbage Liquid;
            }

            else{
                // player just enter the world a moment
            }
        }
    }

    private void SpawnDropper(){
        Transform dropperSpawnTransform = Instantiate(dropperItemSO.itemTransform , addDropperTransform);

        putTheDropper = true;

        InventoryUI.Instance.RemoveItem(dropperItemSO);

        dropperItemSO.inInventory = false;

        dropperTransform = dropperSpawnTransform;

        liquidPivotOfDropperTransform = dropperTransform.Find(LIQUID_PIVOT_OF_DROPPER).transform;
    }
}
