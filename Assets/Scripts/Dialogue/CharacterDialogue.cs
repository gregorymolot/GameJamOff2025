using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Name
{
    Tobias,
    Charlie,
    Daniel,
    Elizabeth,
    Jared,
    Natasha,
    Anthony,
    Jonathan
}

public class CharacterDialogue : MonoBehaviour, IInteractable
{
    public Name characterName;

    int startingIndex = 0;

    bool alreadyInteracted;


    [SerializeField]
    DialoguePiece[] piecesOfDialogue;

    [SerializeField]
    DialogueChoices[] dialogueChoices;

    public int returningLineIndex;

    int dialogueIndex = 0;

    bool returnable = false;

    public bool Returnable { get { return false; } set { } }

    bool isTyping;

    public bool isDialogueActive { get; private set; }

    DialogueManager dialogueManager;
    [SerializeField]
    CinemachineCamera npcCamera;

    void Awake()
    {
        npcCamera.enabled = false;
    }

    void Start()
    {
        dialogueManager = DialogueManager.Instance;
    }

    public void Interact()
    {
        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueManager.SetDialogueText(piecesOfDialogue[dialogueIndex].dialogueLine);
            isTyping = false;
        }

        //Clear chocies
        dialogueManager.ClearChoices();

        //Check if an end line and return line maybe?
        if (piecesOfDialogue[dialogueIndex].lineBehaviour == NextLineBehaviour.ReturningLine)
        {
            dialogueIndex = returningLineIndex;
        }
        else if(piecesOfDialogue[dialogueIndex].lineBehaviour == NextLineBehaviour.EndLine)
        {
            Return();
        }

        //Check if there are choices and display
        foreach(DialogueChoices dialogueChoice in dialogueChoices)
        {
            if (dialogueChoice.choiceIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }


        if (++dialogueIndex < piecesOfDialogue.Length)
        {
            StartCoroutine(TypeSentence());
        }
        else
        {
            Return();
        }
    }

    void StartDialogue()
    {
        DialogueManager.Instance.SetDialogue(this);
        ControllerManager.Instance.SwapCurrentController(ControllerType.Dialogue);
        npcCamera.enabled = true;
        isDialogueActive = true;
        dialogueIndex = 0;
        dialogueManager.SetNameText(characterName.ToString());
        dialogueManager.ShowDialogueUI(true);
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        string dialogueText = "";
        isTyping = true;
        dialogueManager.SetDialogueText(dialogueText);


        foreach (char letter in piecesOfDialogue[dialogueIndex].dialogueLine)
        {
            if (Time.timeScale > 0f)
            {
                dialogueText += letter;
                dialogueManager.SetDialogueText(dialogueText);
            }
            yield return null;
        }
        isTyping = false;
        //dialogueIndex++;
    }

    void DisplayChoices(DialogueChoices choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.choices[i].nextLineIndex;
            //Add a delay here
            dialogueManager.CreateChoiceButton(choice.choices[i].choiceText, () => ChooseOption(nextIndex));
        }
    }
    
    void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        dialogueManager.ClearChoices();
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }

    public void Return()
    {
        StopAllCoroutines();
        npcCamera.enabled = false;
        isDialogueActive = false;
        dialogueManager.SetDialogueText("");
        dialogueManager.SetNameText("");
        dialogueManager.ShowDialogueUI(false);
        ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
    }
}
