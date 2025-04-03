using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerLiquid : MonoBehaviour
{
    [Space]
    [SerializeField] private GraduationTextUI graduationTextUI;

    private bool animeFinish;

    [SerializeField] private bool sthBePickUp;

    private bool openHand;

    private void Awake(){
        animeFinish = false;
    }

    private void Start(){

    }

    private void OnTriggerEnter(){
        if(sthBePickUp){
            Debug.Log("bababa");
        }
        else{
            if(graduationTextUI.GetAddGold()){
                // interactGraduatedCylinderForXR.GetAnimator().SetBool("RemoveGold" , false);
                graduationTextUI.GraduationTextForBiggerMl();
            }
            else{
                // interactGraduatedCylinderForXR.GetAnimator().SetTrigger("AddGoldFirstTime");
                graduationTextUI.GraduationTextForBiggerMl();
            }

            Debug.Log("jajaja");
        }
    }

    public bool PlayerOpenHand(){
        openHand = true;
        return openHand;
    }

    public bool PlayerCloseHand(){
        openHand = false;
        return openHand;
    }
}
