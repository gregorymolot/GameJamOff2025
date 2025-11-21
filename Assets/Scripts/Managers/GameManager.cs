using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
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

    public Material baseMaterial;

    Inventory inventory;



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

        inventory = GameObject.FindAnyObjectByType(typeof(Inventory)).GetComponent<Inventory>();

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
        //ApplyRandomColorsToRenderers();
    }

    void ApplyRandomColorsToRenderers()
    {
        // Get all active MeshRenderers and SpriteRenderers in the scene
        Renderer[] allRenderers = FindObjectsByType<Renderer>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        for (int i = 0; i < allRenderers.Length; i++)
        {
            Renderer currentRenderer = allRenderers[i];
            Color assignedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            if (currentRenderer.material.name.Contains("Dissolve"))
            {
                currentRenderer.GetPropertyBlock(block);
                block.SetColor("_Base", assignedColor);
                currentRenderer.SetPropertyBlock(block);
            }
        }
    }

    public void AddToInventory(InventoryItem item)
    {
        inventory.AddItemToInventory(item);
    }

    public bool CheckCurrentlyEquippedItem(ItemType item)
    {
        return inventory.CheckCurrentlyEquippedItem(item);
    }

    public void RemoveFromInventory(ItemType item)
    {
        inventory.RemoveFromInventory(item);
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
        if (unlockedClues[clue] == false)
        {
            EventManager.Unlocks.NewUnlock?.Invoke();
        }
        unlockedClues[clue] = true;
        CheckAllUnlock();
    }
    
    public bool CheckUnlock(Clues clue)
    {
        return unlockedClues[clue];
    }

    void CheckAllUnlock()
    {
        foreach(Clues clue in unlockedClues.Keys)
        {
            if (clue == Clues.All)
            {
                break;
            }
            if (unlockedClues[clue] == false)
            {
                return;
            }
        }
        unlockedClues[Clues.All] = true;
    }
}
