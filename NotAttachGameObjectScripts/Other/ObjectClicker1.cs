using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectClicker1 : MonoBehaviour
{
    [SerializeField] private List<GameObject> clickableGameObjectList;

    private Vector2 mouseDelta;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach(GameObject target in clickableGameObjectList){
                if(target == GetClickedObject(out RaycastHit hit)){
                    // if(target.transform.TryGetComponent(out Item item)){
                    //     InventoryUI.Instance.AddItem(item.GetItemSO());
                    //     item.PickUpToInventory();
                    //     Destroy(target);
                    // }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            // print("Mouse is off!");
        }

        Cursor.visible = true;
        // mouseDelta = Mouse.current.delta.ReadValue();
    }

    private GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }
        }
        return target;
    }

    private bool isPointerOverUIObject()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }
}