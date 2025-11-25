using System.Collections;
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

    [SerializeField]
    TextMeshProUGUI houseProfileText;
    [SerializeField]
    HouseProfile houseProfile;
    bool inHouse;

    void Awake()
    {
        if (_instance == null)
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
        houseProfile.SetButton();
    }

    void OnEnable()
    {
        foreach (CharacterProfile profile in characterProfiles)
        {
            profile.CheckInteracted();
        }
        animator.SetTrigger("ProfileIn");
        animator.ResetTrigger("ProfileIn");
    }

    public void InitializeProfile(CharacterProfile profile)
    {
        clueText.text = "";
        nameText.text = profile.characterName.ToString();
        descriptionText.text = profile.characterDescription;

        foreach (ProfileLine line in profile.profilePieces)
        {
            if (line.CheckUnlock())
            {
                clueText.text += line.profileDescription;
            }
            else
            {
                clueText.text += "???";
            }
            clueText.text += "\n\n";
        }
        animator.SetTrigger("SwapIn");
    }

    public void InitializeHouseProfile()
    {
        houseProfileText.text = "";
        foreach (ProfileLine line in houseProfile.profileLines)
        {
            if (line.CheckUnlock())
            {
                houseProfileText.text += line.profileDescription;
            }
            houseProfileText.text += "\n\n";
        }
        animator.SetTrigger("HouseSwapIn");
        inHouse = true;
    }

    public void CloseProfile()
    {
        StartCoroutine(ToMindMap());
    }

    public void CloseTab()
    {
            StartCoroutine(ClosingProfileTab());
    }

    IEnumerator ClosingProfileTab()
    {
        animator.SetTrigger("ProfileOut");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("ProfileOut"));

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        yield return null;


        ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
        UIManager.Instance.TurnOffProfileCanvas();
    }
    
    IEnumerator ToMindMap()
    {
        if (inHouse)
        {
            animator.SetTrigger("HouseSwapOut");
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("HouseSwapOut"));
            inHouse = false;
        }
        else
        {
            animator.SetTrigger("SwapOut");
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("ProfileSwapOut"));
        }
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
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
}

[System.Serializable]
public class HouseProfile
{
    public List<ProfileLine> profileLines;

    [SerializeField]
    Button button;

        public void SetButton()
    {
        button.onClick.AddListener(()=>CharacterManager.Instance.InitializeHouseProfile());
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