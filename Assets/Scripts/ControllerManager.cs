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
                break;
            case ControllerType.Interactable:
                interactableController.enabled = true;
                break;
        }
    }
    
    public void DeactivateAllControllers()
    {
        playerController.enabled = false;
        interactableController.enabled = false;
    }
}

public enum ControllerType
{
    Gameplay,
    Interactable,
    Talking,
    Menu
}