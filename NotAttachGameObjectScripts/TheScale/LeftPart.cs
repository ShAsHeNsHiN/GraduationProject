using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftPart : MonoBehaviour , ITheChosenScale , IChooseGold
{
    public static LeftPart Instance{get ; private set;}

    [Header("14GoldInformation")]
    [SerializeField] private TheScaleItemSO _14KGoldSO;
    [SerializeField] private ItemSO _14KGoldItemSO;

    [Header("18GoldInformation")]
    [SerializeField] private TheScaleItemSO _18KGoldSO;
    [SerializeField] private ItemSO _18KGoldItemSO;
    [SerializeField] private Transform _14KGoldNoItemSOTransform;
    [SerializeField] private Transform _18KGoldNoItemSOTransform;

    [Space]
    [SerializeField] private ChooseGoldUI chooseGoldUI;

    [Space]
    [SerializeField] private RightPart rightPart;

    [Space]
    [SerializeField] private Button chooseGoldButton;

    [SerializeField] private float _weight;
    private Transform goldTransform;
    private Vector3 goldSpawnPos;

    // private enum KeyAlphbet{
    //     a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z
    // }

    // [SerializeField] private KeyAlphbet keyAlphbet;

    private void Awake(){
        Instance = this;

        _14KGoldItemSO.inInventory = false;
        _18KGoldItemSO.inInventory = false;
    }

    private void Update(){
        
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
        if(_weight == rightPart.GetWeight() && _weight != 0){
            return _weight.ToString();
        }
        else{
            return "?";
        }
    }

    public void ShowChooseGold(){
        chooseGoldUI.Show();
    }

    public void InstantiateSth(){

    }

    public void WeightIncrease(){

    }

    public void DestroySth(){
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        foreach(Transform child in rightPart.transform){
            Destroy(child.gameObject);
        }
    }

    public void WeightDecrease(){
        _weight = 0;

        rightPart.WeightDataDelete();
    }

    public void ClearGoldTransform(){
        goldTransform = null;
    }

    public Transform GetGoldTransform(){
        return goldTransform;
    }

    // IChooseGold
    public Transform GetTheTransform(){
        return transform;
    }

    public void InstaGold1(){
        if(_14KGoldItemSO.inInventory){
            _14KGoldItemSO.itemTransform = _14KGoldNoItemSOTransform;

            Transform _14KGoldTransform = Instantiate(_14KGoldItemSO.itemTransform , transform);

            goldSpawnPos = new Vector3(UnityEngine.Random.Range(-.22f , .25f) , -.5f , UnityEngine.Random.Range(-.29f , .21f));

            _14KGoldTransform.localPosition = goldSpawnPos;

            _weight = _14KGoldSO.Weight;
            
            goldTransform = _14KGoldTransform;
        }
        else{
            // player doesn't pick up the 14KGold
        }
    }

    public void InstaGold2(){
        if(_18KGoldItemSO.inInventory){
            _18KGoldItemSO.itemTransform = _18KGoldNoItemSOTransform;

            Transform _18KGoldTransform = Instantiate(_18KGoldItemSO.itemTransform , transform);

            goldSpawnPos = new Vector3(UnityEngine.Random.Range(-.22f , .25f) , -.5f , UnityEngine.Random.Range(-.29f , .21f));

            _18KGoldTransform.localPosition = goldSpawnPos;

            _weight = _18KGoldSO.Weight;

            goldTransform = _18KGoldTransform;
        }
        else{
            // player doesn't pick up the 18KGold
        }
    }

    public void ClearAllGold(){
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
    }

    // For VR
    public float _14KGoldWeight(){
        _weight = _14KGoldSO.Weight;

        return _weight;
    }

    public float _18KGoldWeight(){
        _weight = _18KGoldSO.Weight;

        return _weight;
    }
}
