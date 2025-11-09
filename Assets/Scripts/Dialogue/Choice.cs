using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Clues
{
    None,
    Sponges,
    Receipt
}

[System.Serializable]
public class Choice
{
    [TextArea(3,10)]
    public string choiceText;
    public int nextLineIndex;
    [SerializeField]
    List<Clues> unlockable;

    public bool Unlocked()
    {
        foreach(Clues clues in unlockable)
        {
            if (GameManager.Instance.CheckUnlock(clues) == false)
            {
                return false;
            }
        }
        return true;
    }
}

[System.Serializable]
public class DialogueChoices
{
    public int choiceIndex;
    public List<Choice> choices = new List<Choice>();
}

// [System.Serializable]
// public class Unlockable
// {
//     public List<Clues> itemKeys;

//     Dictionary<Clues, bool> lockedItems = new Dictionary<Clues, bool>();

//     public bool unlocked
//     {
//         get
//         {
//             foreach (bool unlocked in lockedItems.Values)
//             {
//                 if (unlocked == false)
//                 {
//                     return false;
//                 }
//             }
//             return true;
//         }
//     }

//     public void InitializeList()
//     {
//         foreach (Clues key in itemKeys)
//         {
//             lockedItems.Add(key, false);
//             EventManager.Unlocks.OnUnlockEvent(key).unlockAction += Unlock;
//         }
//     }

//     public void Unlock(Clues key)
//     {
//         lockedItems[key] = true;
//     }
//}