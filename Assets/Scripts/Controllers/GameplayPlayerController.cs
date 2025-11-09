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
    }

    void OnDisable()
    {
        if (tilt)
        {
            tilt.enabled = false;
        }
        GetComponent<PlayerInput>().enabled = false;
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
        }
    }
    

}
