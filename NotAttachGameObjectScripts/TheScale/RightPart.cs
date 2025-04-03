using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPart : MonoBehaviour , ITheChosenScale
{
    public static RightPart Instance{get ; private set;}

    [Header("Weight")]
    [SerializeField] private TheScaleItemSO weightSO;
    [SerializeField] private ItemSO weightItemSO;

    [SerializeField] private float _weight;

    // private enum KeyAlphbet{
    //     a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z
    // }

    // [SerializeField] private KeyAlphbet keyAlphbet;

    private void Awake(){
        Instance = this;

        weightItemSO.inInventory = false;
    }

    // ITheChosenScale
    public Transform GetTransform(){
        return transform;
    }

    public float GetWeight(){
        return _weight;
    }

    public string GetWeightText()
    {
        return _weight.ToString();
    }

    public void ShowChooseGold(){

    }

    public void InstantiateSth(){
        if(weightItemSO.inInventory){
            Transform weightTransform = Instantiate(weightItemSO.itemTransform , transform);

            weightTransform.localPosition = new Vector3(UnityEngine.Random.Range(-.22f , .25f) , -.5f , UnityEngine.Random.Range(-.29f , .21f));
        }
        else{
            // player isn't pick up the weight
        }
    }

    public void WeightIncrease(){
        _weight += weightSO.Weight;
    }

    public void DestroySth(){
        Destroy(transform.GetChild(0).gameObject);
    }

    public void WeightDecrease(){
        if(_weight == 0){
            Debug.Log("The weight can't be negative");
        }
        else{
            _weight -= weightSO.Weight;
        }
    }

    public void ClearGoldTransform(){

    }

    public void WeightDataDelete(){
        _weight = 0;
    }
}
