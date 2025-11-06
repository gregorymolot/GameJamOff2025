using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public static ControllerManager Instance
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
    private static ControllerManager _instance;


    [SerializeField]
    GameplayPlayerController playerController;

    [SerializeField]
    InteractablePlayerController interactableController;

    [SerializeField]
    DialogueController dialogueController;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            SwapCurrentController(ControllerType.Gameplay);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SwapCurrentController(ControllerType controllerType)
    {
        DeactivateAllControllers();
        switch (controllerType)
        {
            case ControllerType.Gameplay:
                playerController.enabled = true;
                Time.timeScale = 1f;
                break;
            case ControllerType.Interactable:
                interactableController.enabled = true;
                Time.timeScale = 0f;
                break;
            case ControllerType.Dialogue:
                dialogueController.enabled = true;
                //Time.timeScale = 0f;
                break;
        }
    }
    
    public void DeactivateAllControllers()
    {
        playerController.enabled = false;
        interactableController.enabled = false;
        dialogueController.enabled = false;
    }
}

public enum ControllerType
{
    Gameplay,
    Interactable,
    Dialogue,
    Menu
}