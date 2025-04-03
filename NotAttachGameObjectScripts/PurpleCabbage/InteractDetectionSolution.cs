using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDetectionSolution : MonoBehaviour, IInteractable
{
    [Header("DetectionSolution")]
    [SerializeField] private TheSolutionBeforeAndAfterMaterialSO theSolutionBeforeAndAfterMaterialSO;

    [Header("Dropper")]
    [SerializeField] private ItemSO dropperItemSO;
    [SerializeField] private DropperTransformAndSpriteSO nthInDropperSO;
    [SerializeField] private DropperTransformAndSpriteSO sthInDropperSO;
    [SerializeField] private Transform addDropperTransform;

    [Header("PurpleCabbageLiquid")]
    [SerializeField] private ItemSO purpleCabbageLiquidItemSO;
    [SerializeField] private Transform addPurpleCabbageLiquidTransform;

    [Space]
    [SerializeField] private GameObject liquidImage;
    
    private Transform dropperTransform;

    private const string LIQUID_PIVOT_OF_DROPPER = "LiquidPivot";

    private Animator animator;

    private const string SQUEEZE_PURPLE_CABBAGE_LIQUID = "SqueezePurpleCabbageLiquid";

    private Transform liquidPivotOfDropperTransform;

    private Material liquidImageMat;

    private MeshRenderer liquidImageMeshRenderer;

    private float addPurpleCabbageLiquidTimer;
    private bool addPurpleCabbageLiquidAnime;

    private float animeTimer = 1.1f;

    private bool finish;
    private bool putTheDropper;
    private bool Squeeze;
    private bool solutionIsAcid;
    private bool solutionIsBasic;
    private bool solutionIsNeutral;

    private void Awake(){
        // dropperItemSO.itemTransform = nothingInDropperTransform;

        finish = false;
        putTheDropper = false;
        Squeeze = false;

        solutionIsAcid = false;
        solutionIsBasic = false;
        solutionIsNeutral = false;

        addPurpleCabbageLiquidAnime = false;

        addPurpleCabbageLiquidTimer = animeTimer;

        liquidImageMeshRenderer = liquidImage.GetComponent<MeshRenderer>();

        liquidImageMat = liquidImageMeshRenderer.sharedMaterial;
    }

    private void Start(){
        JudgeTheDetectionSolutionMat();
    }

    private void Update(){
        if(addPurpleCabbageLiquidAnime){
            addPurpleCabbageLiquidTimer -= Time.deltaTime;

            if(addPurpleCabbageLiquidTimer > 0){
                Instantiate(purpleCabbageLiquidItemSO.itemTransform , addPurpleCabbageLiquidTransform);
            }
            else{
                Squeeze = true;

                foreach(Transform child in addPurpleCabbageLiquidTransform){
                    child.gameObject.SetActive(false);
                    Destroy(child.gameObject);
                }

                TheSolutionChangeColor();

                addPurpleCabbageLiquidAnime = false;

                liquidPivotOfDropperTransform.gameObject.SetActive(false);

                addPurpleCabbageLiquidTimer = animeTimer;
            }
        }
    }

    public string GetInteractText()
    {
        if(finish){
            return "Color Changed!";
        }
        else{
            if(dropperItemSO.inInventory && dropperItemSO.icon == sthInDropperSO.dropperSprite){
                return "Put the dropper in the Solution!";
            }
            else{
                if(putTheDropper){
                    if(Squeeze){
                        // the dropper is empty , player can squeeze the purple cabbage liquid again
                        return "The Dropper is empty , <br>you need to squeeze the purple cabbage Again!";
                    }
                    else{
                        // add purple cabbage liquid in solution
                        return "Add The Purple Cabbage Liquid!";
                    }
                }
                else{
                    // player just enter the world a moment
                    return "Need the boiled Purple Cabbage Liquid in Dropper!!";
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
        if(finish){
            // return "Please Wait a moment";
            // Add Successful!
        }
        else{
            if(dropperItemSO.inInventory && dropperItemSO.icon == sthInDropperSO.dropperSprite){
                // Put the dropper in the Solution!
                SpawnDropper();

                liquidPivotOfDropperTransform.gameObject.SetActive(true);
            }
            else{
                if(putTheDropper){
                    if(Squeeze){
                        // the dropper is empty , player can pick up the dropper
                        dropperItemSO._name = nthInDropperSO.dropperName;
                        dropperItemSO.icon = nthInDropperSO.dropperSprite;
                        dropperItemSO.itemTransform = nthInDropperSO.dropperTransform;

                        dropperTransform.GetComponent<Item>().Interact(transform);

                        // everything about interact Solution need to be clear
                        putTheDropper = false;
                        Squeeze = false;
                        finish = true;
                    }
                    else{
                        // add purple cabbage liquid in solution
                        // liquidPivotOfDropperTransform.gameObject.SetActive(false);
                        animator.SetTrigger(SQUEEZE_PURPLE_CABBAGE_LIQUID);

                        // TheSolutionChangeColor();

                        // Squeeze = true;

                        addPurpleCabbageLiquidAnime = true;
                    }
                }
                else{
                    // player just enter the world a moment
                }
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

        animator = dropperTransform.GetComponent<Animator>();
    }

    private void TheSolutionChangeColor(){
        if(solutionIsAcid){
            liquidImageMeshRenderer.sharedMaterial = theSolutionBeforeAndAfterMaterialSO.AddPurpleCabbageLiquidForAcid;
        }
        if(solutionIsBasic){
            liquidImageMeshRenderer.sharedMaterial = theSolutionBeforeAndAfterMaterialSO.AddPurpleCabbageLiquidForBasic;
        }
        if(solutionIsNeutral){
            liquidImageMeshRenderer.sharedMaterial = theSolutionBeforeAndAfterMaterialSO.AddPurpleCabbageLiquidForNeutral;
        }
    }

    private void JudgeTheDetectionSolutionMat(){
        foreach(Material acidSolution in theSolutionBeforeAndAfterMaterialSO.acidSolutionArray){
            if(liquidImageMat == acidSolution){
                // liquid is acid
                solutionIsAcid = true;
            }
        }

        
        foreach(Material basicSolution in theSolutionBeforeAndAfterMaterialSO.basicSolutionArray){
            if(liquidImageMat == basicSolution){
                // liquid is basic
                solutionIsBasic = true;
            }
        }

        
        foreach(Material neutralSolution in theSolutionBeforeAndAfterMaterialSO.neutralSolutionArray){
            if(liquidImageMat == neutralSolution){
                // liquid is neutral
                solutionIsNeutral = true;
            }
        }
    }
}
