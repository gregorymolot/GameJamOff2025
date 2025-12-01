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

    NavMeshController controller;

    public int returningLineIndex;

    public int alreadyInteractedLineIndex;

    int dialogueIndex = 0;

    public bool Returnable { get { return false; } set { } }

    bool isTyping;

    public bool isDialogueActive { get; private set; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable = true;

    static bool liedBefore = false;

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
        TryGetComponent<NavMeshController>(out controller);
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
            NewMethod();
            return;
        }

        //Clear chocies
        dialogueManager.ClearChoices();

        if (piecesOfDialogue[dialogueIndex].lineBehaviour == NextLineBehaviour.ReturningLine)
        {
            dialogueIndex = returningLineIndex;
        }
        else if (piecesOfDialogue[dialogueIndex].lineBehaviour == NextLineBehaviour.EndLine)
        {
            Return();
            return;
        }
        else if (piecesOfDialogue[dialogueIndex].lineBehaviour == NextLineBehaviour.AccusingLine)
        {
            dialogueManager.ShowDialogueUI(false);
            UIManager.Instance.ShowEndScreen(piecesOfDialogue[dialogueIndex].accusingName);
            ControllerManager.Instance.SwapCurrentController(ControllerType.None);
            return;
        }

        bool flowControl = NewMethod();
        if (!flowControl)
        {
            return;
        }

        if (++dialogueIndex < piecesOfDialogue.Length)
        {
            StartCoroutine(TypeSentence());
        }
        else
        {
            Return();
        }
        animator.Play("Talk", -1, 0);

    }

    private bool NewMethod()
    {
        //Check if there are choices and display
        foreach (DialogueChoices dialogueChoice in dialogueChoices)
        {
            if (dialogueChoice.choiceIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return false;
            }
        }

        return true;
    }

    void StartDialogue()
    {
        hasClue = false;
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
        if (alreadyInteracted)
        {
            dialogueIndex = alreadyInteractedLineIndex;
        }
        alreadyInteracted = true;
        StartCoroutine(TypeSentence());
        if (controller != null)
        {
            controller.Stop();
        }
    }

    bool hasClue;

    IEnumerator TypeSentence()
    {
        string dialogueText = "";
        isTyping = true;
        dialogueManager.SetDialogueText(dialogueText);

        if (piecesOfDialogue[dialogueIndex].hasClue)
        {
            hasClue = true;
        }

        if (piecesOfDialogue[dialogueIndex].isLying)
        {
            if (liedBefore == false)
            {
                liedBefore = true;
                //Show lying text thing
            }
            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 8f, 0.5f, Room.Person, 1f);
            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.heartBeat, transform.position);

        }

        foreach (char letter in piecesOfDialogue[dialogueIndex].dialogueLine)
        {
            dialogueText += letter;
            dialogueManager.SetDialogueText(dialogueText);
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        //Check if there are choices and display
        NewMethod();

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
        //Destroy sound emitter if there is one
        animator.SetBool("Talking", false);
        StopAllCoroutines();
        npcCamera.enabled = false;
        dialogueIndex = 0;
        isDialogueActive = false;
        dialogueManager.SetDialogueText("");
        dialogueManager.SetNameText("");
        dialogueManager.ShowDialogueUI(false);
        ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
        if (controller != null)
        {
            controller.Resume();
        }
        if (hasClue)
        {
            EventManager.Unlocks.Unlock?.Invoke(piecesOfDialogue[dialogueIndex].clue);
        }
        hasClue = false;
    }
}
