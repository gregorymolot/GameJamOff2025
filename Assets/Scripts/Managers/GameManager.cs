using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public Material dissolveMaterial;
    public GameObject player;

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
    }

    private void Start() {

        dissolveMaterial.SetFloat("_DissolveAmount", 2f);


        inventory = FindAnyObjectByType<Inventory>();

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

    public void StartDissolve()
    {
        StartCoroutine(DissolveCutscene());
    }

    IEnumerator DissolveCutscene()
    {
        float timer = 0f;
        dissolveMaterial.SetVector("_SoundOrigin", player.transform.position);
        while (timer < 2f)
        {
            dissolveMaterial.SetFloat("_DissolveAmount", Mathf.Lerp(2f, -2f, timer/2f));
            timer += Time.deltaTime;
            yield return null;
        }
        ApplyRandomColorsToRenderers();
    }

    void Update()
    {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            DEBUGONLY_UNLOCKALL();
        }
    }

    void DEBUGONLY_UNLOCKALL()
    {
        Clues[] keys = unlockedClues.Keys.ToArray();
        for(int i=0; i<keys.Length; i++)
        {
            unlockedClues[keys[i]] = true;
        }
    }

    void ApplyRandomColorsToRenderers()
    {
        // Get all active MeshRenderers and SpriteRenderers in the scene
        Dissolve[] allDissolves = FindObjectsByType<Dissolve>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        for (int i = 0; i < allDissolves.Length; i++)
        {
            Dissolve currentDissolve = allDissolves[i];
            if (currentDissolve.randomizeColor)
            {
                foreach(Renderer currentRenderer in currentDissolve.GetComponentsInChildren<Renderer>())
                {
                    if (currentRenderer.material.name.Contains("Dissolve"))
                    {
                        Color assignedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                        currentRenderer.GetPropertyBlock(block);
                        block.SetColor("_Base", assignedColor);
                        currentRenderer.SetPropertyBlock(block);
                    }
                }
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
        dissolveMaterial.SetFloat("_DissolveAmount", 2f);
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
        if (clue == Clues.None || clue == Clues.All)
        {
            return;
        }
        if (unlockedClues[clue] == false)
        {
            EventManager.Unlocks.NewUnlock?.Invoke();
            Debug.Log(clue);
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
