using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance
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

    Animator animator;
    private static CharacterManager _instance;

    [SerializeField]
    List<CharacterProfile> characterProfiles;

    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI descriptionText;
    [SerializeField]
    TextMeshProUGUI clueText;
    

    void Awake()
    {
        if (_instance = null)
        {
            _instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        animator = GetComponent<Animator>();

        foreach (CharacterProfile profile in characterProfiles)
        {
            profile.SetButton();
        }
    }

    void OnEnable()
    {
        foreach (CharacterProfile profile in characterProfiles)
        {
            profile.CheckInteracted();
        }
        animator.SetTrigger("ProfileIn");
    }

    public void InitializeProfile(CharacterProfile profile)
    {
        clueText.text = "";
        nameText.text = profile.characterName.ToString();
        descriptionText.text = profile.characterDescription;

        foreach (ProfileLine line in profile.profilePieces)
        {
            clueText.text += line.CheckUnlock();
            clueText.text += "\n\n";
        }

        animator.SetTrigger("SwapIn");

    }

    public void CloseProfile()
    {
        animator.SetTrigger("SwapOut");
        //Play animator and when finished, go to the other image and play that animator
    }
    
    public void CloseTab()
    {
        animator.SetTrigger("ProfileOut");
    }
}

[System.Serializable]
public class CharacterProfile
{
    public Name characterName;
    [SerializeField]
    [TextArea(3, 10)]
    public string characterDescription;

    [SerializeField]
    public List<ProfileLine> profilePieces;
    [SerializeField]
    TextMeshProUGUI buttonText;
    [SerializeField]
    Button button;


    public void SetButton()
    {
        button.onClick.AddListener(()=>CharacterManager.Instance.InitializeProfile(this));
    }

    public void CheckInteracted()
    {
        if (GameManager.Instance.CheckInteract(characterName))
        {
            buttonText.text = characterName.ToString();
            button.enabled = true;
        }
        else
        {
            buttonText.text = "???";
            button.enabled = false;
        }
    }

    public string CheckUnlocked()
    {
        foreach (ProfileLine line in profilePieces)
        {
            if (line.CheckUnlock())
            {
                return line.profileDescription;
            }
        }
        return "";
    }
}

[System.Serializable]
public class ProfileLine
{
    public List<Clues> neededClues;
    [SerializeField]
    [TextArea(3, 10)]
    public string profileDescription;

    public bool CheckUnlock()
    {
        foreach (Clues clue in neededClues)
        {
            if (GameManager.Instance.CheckUnlock(clue) == false)
            {
                return false;
            }
        }
        return true;
    }
}