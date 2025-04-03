using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance{get ; private set;}

    [SerializeField] private GameObject container;

    [Space]
    [SerializeField] private Transform inventoryBgTransform;

    [Space]
    [SerializeField] private GameObject inventoryItem;

    [Space]
    [SerializeField] List<ItemSO> inventoryList = new List<ItemSO>();

    private KeyCode refreshCode = KeyCode.R;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        // Hide();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            Hide();
        }
        if(Input.GetKeyDown(KeyCode.I)){
            Show();
        }
        if(Input.GetKeyDown(refreshCode)){
            // ListItem();
        }

        ListItem();
    }

    public void AddItem(ItemSO item){
        inventoryList.Add(item);
    }

    public void RemoveItem(ItemSO item){
        inventoryList.Remove(item);
    }

    private void ListItem(){
        foreach(Transform child in inventoryBgTransform){
            if(child == inventoryItem.transform){
                continue;
            }

            Destroy(child.gameObject);
        }

        foreach(var item in inventoryList){
            GameObject obj = Instantiate(inventoryItem , inventoryBgTransform);

            var itemName = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("Icon").GetComponent<Image>();

            itemName.SetText(item._name);
            itemIcon.sprite = item.icon;
        }
    }

    private void Hide(){
        container.SetActive(false);
    }

    private void Show(){
        container.SetActive(true);
    }
}
