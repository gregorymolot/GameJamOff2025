using System.Collections;
using TMPro;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public enum Name
{
    
}

public class CharacterDialogue : MonoBehaviour, IInteractable
{
    public Name characterName;

    int startingIndex = 0;

    bool alreadyInteracted;

    public int returningLineIndex;

    [SerializeField]
    public DialogueChoices[] dialogueChoices;

    public DialoguePiece[] piecesOfDialogue;

    int dialogueIndex = 0;

    bool isInteracting;

    public bool IsInteracting { get => isInteracting; set => isInteracting = value; }

    bool isTyping;

    bool isDialogueActive;

    DialogueManager dialogueManager;

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
        else if (++dialogueIndex < piecesOfDialogue.Length)
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
        dialogueIndex++;
    }

    public void Return()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueManager.SetDialogueText("");
        dialogueManager.SetNameText("");
        dialogueManager.ShowDialogueUI(false);
    }
}
