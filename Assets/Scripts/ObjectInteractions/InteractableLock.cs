using UnityEngine;

public class InteractableLock : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    [SerializeField]
    private bool interactable;

    [SerializeField]
    ItemType requiredItem;
    [SerializeField]
    bool removesItem;

    public void Interact()
    {
        if (GameManager.Instance.CheckCurrentlyEquippedItem(requiredItem))
        {
            interactable = false;
            if (removesItem)
            {
                GameManager.Instance.RemoveFromInventory(requiredItem);
            }
        }
    }

    public void Return()
    {
    }
}
