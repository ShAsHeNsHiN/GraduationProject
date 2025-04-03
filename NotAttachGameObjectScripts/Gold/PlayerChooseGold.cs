using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooseGold : MonoBehaviour
{

    public IChooseGold WhichItemAddGold(){
        List<IChooseGold> needGoldItemList = new List<IChooseGold>();
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position , interactRange);
        foreach(Collider collider in colliderArray){
            if(collider.TryGetComponent(out IChooseGold chooseGold)){
                needGoldItemList.Add(chooseGold);
            }
        }   

        IChooseGold closetNeedGoldItem = null;
        foreach(IChooseGold chooseGold in needGoldItemList){
            if(closetNeedGoldItem == null){
                closetNeedGoldItem = chooseGold;
            }
            else{
                if(Vector3.Distance(transform.position , chooseGold.GetTheTransform().position) < Vector3.Distance(transform.position , closetNeedGoldItem.GetTheTransform().position)){
                    // closer
                    closetNeedGoldItem = chooseGold;    
                }
            }
        }

        return closetNeedGoldItem;
    }
}
