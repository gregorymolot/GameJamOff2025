using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class GameplayPlayerController : MonoBehaviour
{
    //PlayerInput input;

    [SerializeField]
    Player player;

    [SerializeField]
    CinemachineInputAxisController tilt;

    void OnEnable()
    {
        tilt.enabled = true;
        GetComponent<PlayerInput>().enabled = true;
            float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1);
        foreach(var controller in tilt.Controllers)
        {
            Debug.Log(controller.Name);
            if (controller.Name == "Look X (Pan)")
            {
                controller.Input.Gain = sensitivity;
            }
            if (controller.Name == "Look Y (Tilt)")
            {
                controller.Input.Gain = PlayerPrefs.GetInt("Invert", 0) == 0 ? -sensitivity : sensitivity;
            }
        }
    }

    void OnDisable()
    {
        if (tilt)
        {
            tilt.enabled = false;
        }
        GetComponent<PlayerInput>().enabled = false;
        UIManager.Instance.TurnOffInteractText();
    }

    public void Walk(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        player.SetWalkDirection(movement);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.Interact();
        }
    }

    public void OpenProfileTab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UIManager.Instance.InitializeProfiles();
            ControllerManager.Instance.SwapCurrentController(ControllerType.Profile);
        }
    }

    void Update()
    {
        CheckText();
    }

    void CheckText()
    {
        if (player.interactable == null || player.interactable.Interactable == false)
        {
            UIManager.Instance.TurnOffInteractText();
        }
        else if (player.interactable is CharacterDialogue)
        {
            UIManager.Instance.ShowTalkText();
        }
        else
        {
            UIManager.Instance.ShowInteractText();
        }
    }


}
