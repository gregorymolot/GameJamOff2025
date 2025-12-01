using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    MeshFilter itemSlot;
    Dictionary<ItemType, Mesh> inventoryMeshes = new Dictionary<ItemType, Mesh>();
    ItemType currentItem;
    int itemIndex;

    bool pickedUpItem = false;

    [SerializeField]
    GameObject canvas;

    List<ItemType> inventoryItems = new List<ItemType>();

    void OnEnable()
    {
        EventManager.Items.ShowInInventory += ShowNewItem;
    }

    void OnDisable()
    {
        EventManager.Items.ShowInInventory -= ShowNewItem;
    }

    void Start()
    {
        itemIndex = 0;
        inventoryItems.Add(ItemType.None);
        currentItem = inventoryItems[itemIndex];
        inventoryMeshes.Add(ItemType.None, null);
        itemSlot.mesh = inventoryMeshes[ItemType.None];
    }

    void ShowNewItem()
    {
        itemIndex = inventoryItems.Count - 1;
        currentItem = inventoryItems[itemIndex];
        itemSlot.mesh = inventoryMeshes[currentItem];
        if (pickedUpItem == false)
        {
            pickedUpItem = true;
            //Show item inventory thing
            canvas.SetActive(true);
            StartCoroutine(TurnOffCanvas());
        }
    }

    public void AddItemToInventory(InventoryItem item)
    {
        inventoryItems.Add(item.ItemType);
        inventoryMeshes.Add(item.ItemType, item.GetComponent<MeshFilter>().sharedMesh);
    }

    IEnumerator TurnOffCanvas()
    {
        yield return new WaitForSeconds(5f);
        canvas.SetActive(false);
    }

    public void RemoveFromInventory(ItemType item)
    {
        if (item == ItemType.None)
        {
            return;
        }
        inventoryItems.Remove(item);
        inventoryMeshes.Remove(item);
        CycleInventory(1);
    }

    public void CycleInventory(float direction)
    {
        itemIndex = (int)((itemIndex + direction + inventoryItems.Count) % inventoryItems.Count);
        currentItem = inventoryItems[itemIndex];
        itemSlot.mesh = inventoryMeshes[currentItem];
    }
    public bool CheckCurrentlyEquippedItem(ItemType item)
    {
        Debug.Log(currentItem);
        return item == currentItem;
    }
}
