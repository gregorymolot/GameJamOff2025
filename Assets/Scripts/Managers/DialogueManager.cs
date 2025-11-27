using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    DialogueController dialogueController;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    public GameObject choiceButtonPrefab;

    public Transform choiceContainer;

    public GameObject dialoguePanel;

    bool firstTimeTalking = true;

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

    public void SetDialogue(CharacterDialogue dialogue)
    {
        if (firstTimeTalking)
        {
            //Show tutorial talking thing
        }
        dialogueController.SetCharacterDialogue(dialogue);
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

    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer) Destroy(child.gameObject);
    }

    public void CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        //Add typing in here
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
    }
}
