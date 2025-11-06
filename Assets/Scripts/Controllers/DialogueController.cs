using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
    CharacterDialogue currentDialogue;

    [SerializeField]
    LayerMask buttonMask;
    [SerializeField]
    GraphicRaycaster graphicRaycaster;

    void OnEnable()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    void OnDisable()
    {
        GetComponent<PlayerInput>().enabled = false;
    }

    public void SetCharacterDialogue(CharacterDialogue dialogue)
    {
        currentDialogue = dialogue;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Mouse.current.position.ReadValue();
            var results = new List<RaycastResult>();

            graphicRaycaster.Raycast(eventData, results);
            foreach(RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Button>() != null)
                {
                    return;
                }
            }
            if (currentDialogue != null && currentDialogue.isDialogueActive)
            {
                currentDialogue.Interact();
                return;
            }
        }
    }
}
