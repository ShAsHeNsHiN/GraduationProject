using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractGraduatedCylinder : MonoBehaviour , IInteractable , IChooseGold
{
    public static InteractGraduatedCylinder Instance{get ; private set;}

    [SerializeField] private ChooseGoldUI chooseGoldUI;

    [Space]
    [SerializeField] private Transform liquidTransform;

    [Space]
    [SerializeField] private Transform goldFellPosTransform;

    [Space]
    [SerializeField] private GraduationTextUI graduationTextUI;

    [Header("GoldInformation")]
    [SerializeField] private ItemSO _14KGoldItemSO;
    [SerializeField] private ItemSO _18KGoldItemSO;
    [SerializeField] private Transform _14KGoldSmallerTransform;
    [SerializeField] private Transform _18KGoldSmallerTransform;

    [Header("LiquidValue")]
    [SerializeField] private LiquidSO smallerMLSO;
    [SerializeField] private LiquidSO biggerMLSO;

    [Header("Water")]
    [SerializeField] private ItemSO waterItemSO;

    private Vector3 liquidHeightForSmaller;
    private Vector3 liquidHeightForBigger;

    private bool afterWater;

    private Transform goldTransform;

    private Animator animator;

    private void Awake(){
        Instance = this;

        afterWater = false;
        animator = GetComponent<Animator>();

        _14KGoldItemSO.inInventory = false;
        _18KGoldItemSO.inInventory = false;
        waterItemSO.inInventory = false;
    }

    private void Start(){
        HideLiquid();

        liquidHeightForSmaller = new Vector3(liquidTransform.localScale.x , smallerMLSO.height , liquidTransform.localScale.z);

        liquidHeightForBigger = new Vector3(liquidTransform.localScale.x , biggerMLSO.height , liquidTransform.localScale.z);
    }

    public Animator GetAnimator(){
        return animator;
    }

    private Transform ClearGoldTransform(){
        goldTransform = null;

        foreach(Transform child in goldFellPosTransform){
            Destroy(child.gameObject);
        }

        return goldTransform;
    }

    public void Interact(Transform interactTransform){
        if(afterWater){
            if(graduationTextUI.GetAddGold()){
                // after first time add gold
                if(goldTransform == null && graduationTextUI.GetBiggerToSmallerML()){
                    // player add gold
                    chooseGoldUI.Show();
                }
                else{
                    if(graduationTextUI.GetSmallerToBiggerML()){
                        // player remove gold
                        ClearGoldTransform();
                        animator.SetBool("RemoveGold" , true);
                        graduationTextUI.GraduationTextForSmallerMl();
                    }
                    else{
                        
                    }
                }
                
            }
            else{
                // first time add gold
                if(goldTransform == null && _14KGoldItemSO.inInventory && _18KGoldItemSO.inInventory){
                    // player add gold
                    chooseGoldUI.Show();
                }
                else{
                    NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.NoGold , .2f);
                }
            }
        }
        else{
            if(waterItemSO.inInventory){
                // player add water
                ShowLiquid();

                animator.SetTrigger("AddWater");

                liquidTransform.localScale = liquidHeightForSmaller;
                // liquidTransform.localScale += Vector3.up * _200mlSO.height;

                float aboveAnimeTimer = 1f;
                graduationTextUI.Invoke("Show" , aboveAnimeTimer);
                graduationTextUI.Invoke("GraduationTextForSmallerMl" , aboveAnimeTimer);

                Invoke("AfterWaterIsTrue" , aboveAnimeTimer);
            }
            else{
                // player doesn't pick up the water
                NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.NoWater , .2f);
            }
        }
    }

    private bool AfterWaterIsTrue(){
        afterWater = true;
        return afterWater;
    }

    public Transform GetTransform(){
        return transform;
    }

    public string GetInteractText(){
        if(afterWater){
            if(graduationTextUI.GetAddGold()){
                // after first time add gold
                if(goldTransform == null && graduationTextUI.GetBiggerToSmallerML()){
                    return "Add Gold?";
                }
                else{
                    if(graduationTextUI.GetSmallerToBiggerML()){
                        return "Remove!";
                    }
                    else{
                        return "Wait a moment…";
                    }
                }
                
            }
            else{
                // first time add gold
                if(goldTransform == null){
                    return "Add Gold?";
                }

                return null;
            }
        }
        else{
            if(waterItemSO.inInventory){
                return "Add Water?";
            }
            else{
                return null;
            }
        }
    }

    private void ShowLiquid(){
        liquidTransform.gameObject.SetActive(true);
    }

    private void HideLiquid(){
        liquidTransform.gameObject.SetActive(false);
    }

    // IChooseGold
    public Transform GetTheTransform(){
        return transform;
    }

    public void InstaGold1(){
        if(_14KGoldItemSO.inInventory){
            _14KGoldItemSO.itemTransform = _14KGoldSmallerTransform;

            Transform _14KGoldTransform = Instantiate(_14KGoldItemSO.itemTransform , goldFellPosTransform);

            goldTransform = _14KGoldTransform;
        }
        else{

        }
    }

    public void InstaGold2(){
        if(_18KGoldItemSO.inInventory){
            _18KGoldItemSO.itemTransform = _18KGoldSmallerTransform;

            Transform _18KGoldTransform = Instantiate(_18KGoldItemSO.itemTransform , goldFellPosTransform);

            goldTransform = _18KGoldTransform;
        }
        else{

        }
    }

    public void ClearAllGold(){
        foreach(Transform child in goldFellPosTransform){
            Destroy(child.gameObject);
        }
    }
}
