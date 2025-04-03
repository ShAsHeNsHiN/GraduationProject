using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractScale : MonoBehaviour
{
    [SerializeField] private LeftPart leftPart;
    [SerializeField] private Transform playerCameraTransform;

    [Header("GoldInformation")]
    [SerializeField] private ItemSO _14KGoldItemSO;
    [SerializeField] private ItemSO _18KGoldItemSO;

    [Header("Weight")]
    [SerializeField] private ItemSO weightItemSO;

    private KeyCode keyIncrease = KeyCode.Q;
    private KeyCode keyDecrease = KeyCode.E;

    private ITheChosenScale theChosenScale1;

    private void Start(){
        // Debug.Log(); 
    }

    private void Update(){
        ITheChosenScale theChosenScale = InteractWhichScale();

        // limited. player need to choose the gold first so they can add the weight. it's kind comfirmity the experience prograss (maybe…)

        if(theChosenScale != null){
            if(Input.GetKeyDown(keyIncrease)){
                if(_14KGoldItemSO.inInventory && _18KGoldItemSO.inInventory){
                    if(leftPart.GetGoldTransform() == null){
                        theChosenScale.ShowChooseGold();
                    }
                    else{
                        if(weightItemSO.inInventory){
                            // give right Part to execute , not the left part
                            theChosenScale.InstantiateSth();
                            theChosenScale.WeightIncrease();
                        }
                        else{
                            // player doesn't pick up the weight
                            NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.NoWeight , .2f);
                        }
                    }
                }

                else{
                    // player doesn't pick up any gold!!
                    NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.NoGold , .2f);
                }
            }
            if(Input.GetKeyDown(keyDecrease)){
                if(theChosenScale != null && theChosenScale.GetWeight() != 0 && leftPart.GetGoldTransform() != null){
                    theChosenScale.DestroySth();
                    theChosenScale.WeightDecrease();
                    theChosenScale.ClearGoldTransform();
                }
            }
        }
        

        // free , don't need to choose gold previous that player can add the weight , but it's kind not comfirmity the experience progress
        // if(Input.GetKeyDown(keyIncrease)){
        //     if(theChosenScale != null){
                
        //         // give right Part to execute , not the left part
        //         theChosenScale.InstantiateSth();
        //         theChosenScale.WeightIncrease();

        //         if(chooseGoldUI.GetGoldTransform() == null){
        //             theChosenScale.ShowChooseGold();
        //         }
        //     }
        // }
        // if(Input.GetKeyDown(keyDecrease)){
        //     if(theChosenScale != null && theChosenScale.GetWeight() != 0){
                    
        //         theChosenScale.DestroySth();
        //         theChosenScale.WeightDecrease();

        //         if(chooseGoldUI.GetGoldTransform() != null){
        //             theChosenScale.ClearGoldTransform();
        //         }
        //     }
        // }
    }

    // Interact Part
    public ITheChosenScale InteractWhichScale(){
        List<ITheChosenScale> tiltList = new List<ITheChosenScale>();
        float interactRange = 2f;
        Collider [] colliders = Physics.OverlapSphere(transform.position , interactRange);
        foreach(Collider obj in colliders){
            if(obj.TryGetComponent(out ITheChosenScale theChosenScale)){
                tiltList.Add(theChosenScale);
            }
        }
        
        ITheChosenScale closetPart = null;
        foreach(ITheChosenScale theChosenScale in tiltList){
            if(closetPart == null){
                closetPart = theChosenScale;
            }
            else{
                if(Vector3.Distance(transform.position , theChosenScale.GetTransform().position) < Vector3.Distance(transform.position , closetPart.GetTransform().position)){
                    // closer
                    closetPart = theChosenScale;
                }
            }
        }

        return closetPart;
    }

    // interact part with ray
    public ITheChosenScale InteractWhichScaleWithRay(){
        float interactDistance = 2f;
        if(Physics.Raycast(playerCameraTransform.position , playerCameraTransform.forward , out RaycastHit raycastHit , interactDistance)){
            if(raycastHit.transform.TryGetComponent<ITheChosenScale>(out theChosenScale1)){
                return theChosenScale1;
            }
        }
        
        return null;
    }

    public KeyCode GetIncreaseCode(){
        return keyIncrease;
    }

    public KeyCode GetDecreaseCode(){
        return keyDecrease;
    }
}
