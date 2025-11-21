using UnityEngine;

public enum ItemType
{
    None,
    HairDryer
}

public class InventoryItem : DiscoverableItem
{
    public ItemType ItemType { get{ return itemType;} }

    [SerializeField]
    private ItemType itemType;

    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.AddToInventory(this);
    }

    public override void Return()
    {
        base.Return();
        Destroy(gameObject);
    }
}
