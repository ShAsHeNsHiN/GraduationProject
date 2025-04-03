using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FileName?")]
public class ItemSO : ScriptableObject
{
    public string _name;
    public bool inInventory;
    public Transform itemTransform;
    public Sprite icon;
}
