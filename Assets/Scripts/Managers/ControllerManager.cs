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

    [SerializeField]
    MenuController menuController;
    [SerializeField]
    SafeController safeController;

    [SerializeField]
    CutsceneController cutsceneController;

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
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                break;
            case ControllerType.Interactable:
                interactableController.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 0f;
                break;
            case ControllerType.Dialogue:
                dialogueController.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1f;
                break;
            case ControllerType.Profile:
                profileController.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                break;
            case ControllerType.Menu:
                menuController.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                break;
            case ControllerType.Safe:
                safeController.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                break;
            case ControllerType.Cutscene:
                cutsceneController.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                break;
            case ControllerType.None:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
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
        menuController.enabled = false;
        safeController.enabled = false;
        cutsceneController.enabled = false;
    }
}

public enum ControllerType
{
    Gameplay,
    Interactable,
    Dialogue,
    Profile,
    Safe,
    Menu,
    Cutscene,
    None
}