using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No Controller");
            }
            return _instance;
        }
    }

    private static GameManager _instance;

    public Dictionary<Clues, bool> unlockedClues = new Dictionary<Clues, bool>();
    public Dictionary<Name, bool> interactedCharacters = new Dictionary<Name, bool>();



    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        var values = (Clues[])System.Enum.GetValues(typeof(Clues));
        // Iterate through the array and add each value to the list
        foreach (Clues value in values)
        {
            unlockedClues.Add(value, false);
        }

        var names = (Name[])System.Enum.GetValues(typeof(Name));
        // Iterate through the array and add each value to the list
        foreach (Name name in names)
        {
            interactedCharacters.Add(name, false);
        }
    }

    void OnEnable()
    {
        EventManager.Unlocks.Unlock += Unlock;
        EventManager.Unlocks.Interacted += Interacted;
    }

    void OnDisable()
    {
        EventManager.Unlocks.Unlock -= Unlock;
        EventManager.Unlocks.Interacted -= Interacted;
    }

    void Interacted(Name name)
    {
        interactedCharacters[name] = true;
    }
    
    public bool CheckInteract(Name name)
    {
        return interactedCharacters[name];
    }

    void Unlock(Clues clue)
    {
        unlockedClues[clue] = true;
    }
    
    public bool CheckUnlock(Clues clue)
    {
        return unlockedClues[clue];
    }
}
