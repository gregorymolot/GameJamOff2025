using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Clues
{
    Sponges,
    Receipt
}

[System.Serializable]
public class Choice
{
    [TextArea(3,10)]
    public string choiceText;
    public int nextLineIndex;
    public Unlockable unlockable;
}

[System.Serializable]
public class DialogueChoices
{
    public int choiceIndex;
    public List<Choice> choices = new List<Choice>();

    public void InitializeList()
    {
        foreach (Choice choice in choices)
        {
            choice.unlockable.InitializeList();
        }
    }
}

[System.Serializable]
public class Unlockable
{
    public List<Clues> itemKeys;

    Dictionary<Clues, bool> lockedItems = new Dictionary<Clues, bool>();

    public bool unlocked
    {
        get
        {
            foreach (bool unlocked in lockedItems.Values)
            {
                if (unlocked == false)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public void InitializeList()
    {
        foreach (Clues key in itemKeys)
        {
            lockedItems.Add(key, false);
            EventManager.Unlocks.OnUnlockEvent(key).unlockAction += Unlock;
        }
    }

    public void Unlock(Clues key)
    {
        lockedItems[key] = true;
    }
}