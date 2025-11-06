using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    TextMeshProUGUI dialogueText;
    TextMeshProUGUI nameText;

    GameObject choiceButtonPrefab;

    Transform choiceContainer;

    Animator dialogueAnimator;

    public GameObject dialoguePanel;

    public static DialogueManager Instance
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
    private static DialogueManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void SetNameText(string name)
    {
        nameText.text = name;
    }

    public void SetDialogueText(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
