using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour , IInteractable
{
    public static Item Instance{get ; private set;}

    [SerializeField] private ItemSO itemSO;

    // [SerializeField] private bool firstTimePickupItem;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        itemSO.inInventory = false;
        // firstTimePickupItem = false;
        // Debug.Log(itemSO);
    }

    private bool PickUpToInventory(){
        itemSO.inInventory = true;

        return itemSO.inInventory;
    }

    private void PickUpSth(){
        InventoryUI.Instance.AddItem(itemSO);

        itemSO.inInventory = true;

        Destroy(gameObject , .1f);
    }

    private void UseSth(){
        InventoryUI.Instance.RemoveItem(itemSO);

        itemSO.inInventory = false;

        // Instantiate(itemSO.itemTransform , transform);
    }

    public void Interact(Transform interactTransform)
    {
        NPCUI_LabScene.Instance.NpcTalking(EPlayingProgress.PickUpItem , .2f);

        PickUpSth();

        // if(itemSO.inInventory){
        //     UseSth();
        // }
        // else{
        //     PickUpSth();
        // }
    }

    public string GetInteractText()
    {
        return null;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
