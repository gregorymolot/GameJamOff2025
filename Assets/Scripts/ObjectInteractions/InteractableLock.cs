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
    protected bool isUnlocked = false;

    public virtual void Interact()
    {
        if (!isUnlocked)
        {
            if (GameManager.Instance.CheckCurrentlyEquippedItem(requiredItem))
            {
                isUnlocked = true;
                UnlockAction();
                if (removesItem)
                {
                    GameManager.Instance.RemoveFromInventory(requiredItem);
                }
            }
        }
        else
        {
            UnlockedAction();
        }
    }

    protected virtual void UnlockedAction()
    {
        
    }

    protected virtual void UnlockAction()
    {
        
    }

    public void Return()
    {
    }
}
