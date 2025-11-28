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
    }

    public void AddItemToInventory(InventoryItem item)
    {
        if (pickedUpItem == false)
        {
            pickedUpItem = true;
            Debug.Log("PickedUpItem!");
            //Show item inventory thing
        }
        inventoryItems.Add(item.ItemType);
        inventoryMeshes.Add(item.ItemType, item.GetComponent<MeshFilter>().sharedMesh);
        // itemIndex = inventoryItems.Count - 1;
        // currentItem = inventoryItems[itemIndex];
        // itemSlot.mesh = inventoryMeshes[currentItem];
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
