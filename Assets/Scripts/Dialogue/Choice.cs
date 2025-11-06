using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Choice
{
    [TextArea(3,10)]
    public string choiceText;
    public int nextLineIndex;
    public List<ItemKey> itemKeys = new List<ItemKey>();

    Dictionary<ItemKey, bool> lockedItems = new Dictionary<ItemKey, bool>();

    public Choice()
    {
        foreach(ItemKey items in itemKeys)
        {
            lockedItems.Add(items, false);
        }
    }

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

    public void Unlock(ItemKey key)
    {
        lockedItems[key] = true;
    }
}


[System.Serializable]
public class DialogueChoices
{
    public int choiceIndex;
    public Choice[] choices;
}