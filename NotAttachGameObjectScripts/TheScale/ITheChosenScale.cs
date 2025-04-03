using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITheChosenScale
{
    void WeightIncrease();
    void WeightDecrease();
    Transform GetTransform();
    float GetWeight();
    string GetWeightText();
    void InstantiateSth();
    void DestroySth();
    
    // only ScaleLeft
    void ShowChooseGold();
    void ClearGoldTransform();
}
