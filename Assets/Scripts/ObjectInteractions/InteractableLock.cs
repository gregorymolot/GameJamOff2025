using UnityEngine;

public class InteractableLock : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    [SerializeField]
    private bool interactable;

    [SerializeField]
    string failedString;

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
                if (isUnlocked == false)
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
                UIManager.Instance.ShowFailedText(failedString);
                FailedAction();
            }
        }
        else
        {
            UnlockedAction();
        }
    }

    protected virtual void FailedAction()
    {
        
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
