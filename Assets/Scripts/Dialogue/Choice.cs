using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Choice
{
    [TextArea(3,10)]
    public string choiceText;
    public int nextLineIndex;
    public List<ItemKey> itemKeys;

    Dictionary<ItemKey, bool> lockedItems = new Dictionary<ItemKey, bool>();

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
        foreach(ItemKey key in itemKeys)
        {
            lockedItems.Add(key, false);
        }
    }

    public void Unlock(ItemKey key)
    {
        lockedItems[key] = true;
    }
}


[System.Serializable]
public class DialogueChoices
{
    public int choiceIndex;
    public List<Choice> choices = new List<Choice>();

    public void InitializeList()
    {
        foreach(Choice choice in choices)
        {
            choice.InitializeList();
        }
    }
}