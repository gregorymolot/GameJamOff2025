using UnityEngine;
using UnityEngine.InputSystem;

public class InteractablePlayerController : MonoBehaviour
{
    [SerializeField]
    Player player;

    void OnEnable()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    void OnDisable()
    {
        GetComponent<PlayerInput>().enabled = false;
    }

    public void ReturnItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.ReturnItem();
        }
    }
}
