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

    bool isInMindMap = true;
    

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
            clueText.text += line.CheckUnlock();
            clueText.text += "\n\n";
        }
        animator.SetTrigger("SwapIn");
        isInMindMap = false;
    }

    public void CloseProfile()
    {
        StartCoroutine(ToMindMap());
    }

    public void CloseTab()
    {
        if (isInMindMap)
        { 
            StartCoroutine(ClosingProfileTab());
        }
    }

    IEnumerator ClosingProfileTab()
    {
        animator.SetTrigger("ProfileOut");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("ProfileOut"));

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f);
        ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        yield return null;


        UIManager.Instance.TurnOffProfileCanvas();
    }
    
    IEnumerator ToMindMap()
    {
        animator.SetTrigger("SwapOut");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("ProfileSwapOut"));

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        isInMindMap = true;
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
public class ProfileLine
{
    public List<Clues> neededClues;
    [SerializeField]
    [TextArea(3, 10)]
    public string profileDescription;

    public string CheckUnlock()
    {
        foreach (Clues clue in neededClues)
        {
            if (GameManager.Instance.CheckUnlock(clue) == false)
            {
                return profileDescription;
            }
        }
        return "";
    }
}