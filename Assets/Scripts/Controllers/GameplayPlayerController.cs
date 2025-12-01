using FMOD.Studio;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayPlayerController : MonoBehaviour
{
    //PlayerInput input;

    [SerializeField]
    Player player;

    [SerializeField]
    CinemachineInputAxisController tilt;

    EventInstance playerFootsteps;

    void Start()
    {
        playerFootsteps = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.footsteps, gameObject);
    }

    void OnEnable()
    {
        tilt.enabled = true;
        GetComponent<PlayerInput>().enabled = true;
            float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 1);
        foreach(var controller in tilt.Controllers)
        {
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
        UpdateSound(movement);
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
            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.whoosh);
        }
    }

    public void CycleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.CycleInventory(context.ReadValue<float>());
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

    void UpdateSound(Vector2 movementDirection)
    {
        PLAYBACK_STATE playbackState;
        playerFootsteps.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED) && movementDirection != Vector2.zero)
        {
            playerFootsteps.start();
        }
        else if (playbackState.Equals(PLAYBACK_STATE.PLAYING) && movementDirection == Vector2.zero)
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }


}
