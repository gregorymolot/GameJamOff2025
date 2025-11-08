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

    [SerializeField]
    ProfileController profileController;

    ControllerType previousType;
    ControllerType currentType;

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
        previousType = ControllerType.Menu;
    }

    public void SwapCurrentController(ControllerType controllerType)
    {
        previousType = currentType;
        currentType = controllerType;
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
            case ControllerType.Profile:
                profileController.enabled = true;
                break;
        }
    }

    public void ReturnToPrevious()
    {
        SwapCurrentController(previousType);
    }
    
    public void DeactivateAllControllers()
    {
        playerController.enabled = false;
        interactableController.enabled = false;
        dialogueController.enabled = false;
        profileController.enabled = false;
    }
}

public enum ControllerType
{
    Gameplay,
    Interactable,
    Dialogue,
Profile,
    Menu
}