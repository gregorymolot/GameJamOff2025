using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

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
    //public Name characterName;

    bool alreadyInteracted;
    [SerializeField]
    Name characterName;

    [SerializeField]
    DialoguePiece[] piecesOfDialogue;

    [SerializeField]
    List<DialogueChoices> dialogueChoices;

    public int returningLineIndex;

    int dialogueIndex = 0;

    public bool Returnable { get { return false; } set { } }

    bool isTyping;

    public bool isDialogueActive { get; private set; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable = true;

    DialogueManager dialogueManager;

    [SerializeField]
    CinemachineCamera npcCamera;


    [SerializeField]
    TextMeshProUGUI nameLabel;

    Animator animator;

    void Awake()
    {
        npcCamera.enabled = false;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        dialogueManager = DialogueManager.Instance;
    }

    public void Interact()
    {
        if (isDialogueActive)
        {
            animator.Play("Talk", -1, 0);
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void NextLine()
    {
        //Destroy sound emitter if there is one
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
        //nameLabel.text = characterName.ToString();
        animator.SetBool("Talking", true);
        nameLabel.text = characterName.ToString();
        DialogueManager.Instance.SetDialogue(this);
        ControllerManager.Instance.SwapCurrentController(ControllerType.Dialogue);
        npcCamera.enabled = true;
        isDialogueActive = true;
        dialogueIndex = alreadyInteracted ? returningLineIndex : 0;
        EventManager.Unlocks.Interacted?.Invoke(characterName);
        dialogueManager.SetNameText(characterName.ToString());
        dialogueManager.ShowDialogueUI(true);
        alreadyInteracted = true;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        string dialogueText = "";
        isTyping = true;
        dialogueManager.SetDialogueText(dialogueText);

        if (piecesOfDialogue[dialogueIndex].hasClue)
        {
            EventManager.Unlocks.Unlock?.Invoke(piecesOfDialogue[dialogueIndex].clue);
        }

        if (piecesOfDialogue[dialogueIndex].isLying)
        {
            //TODO: Spawn temporary sound emitter
        }

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
        //Check if there are choices and display
        foreach(DialogueChoices dialogueChoice in dialogueChoices)
        {
            if (dialogueChoice.choiceIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
            }
        }

    }

    void DisplayChoices(DialogueChoices choice)
    {
        for (int i = 0; i < choice.choices.Count; i++)
        {
            if (choice.choices[i].Unlocked())
            {
                int nextIndex = choice.choices[i].nextLineIndex;
                //Add a delay here
                dialogueManager.CreateChoiceButton(choice.choices[i].choiceText, () => ChooseOption(nextIndex));
            }
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
        animator.SetBool("Talking", false);
        StopAllCoroutines();
        npcCamera.enabled = false;
        isDialogueActive = false;
        dialogueManager.SetDialogueText("");
        dialogueManager.SetNameText("");
        dialogueManager.ShowDialogueUI(false);
        ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
    }
}
