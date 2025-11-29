using System.Collections.Generic;
using UnityEngine;

public enum Clues
{
    None,
    NewSponges,
    ThrownOutSponges,
    PaymentFight,
    StolenMoney,
    LyingAboutMoney,
    Phonebook,
    Tired,
    NoOffice,
    FightForPromotion,
    Rivalry,
    Promotion,
    File,
    StartUp,
    Divorce,
    EmptyCloset,
    Stress,
    BadStartUp,
    All
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