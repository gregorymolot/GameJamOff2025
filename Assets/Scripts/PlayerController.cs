using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //PlayerInput input;

    [SerializeField]
    Player player;

    void OnEnable()
    {
        // input = GetComponent<PlayerInput>();
        // input.actions["Move"] += 
    }

    public void Walk(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        player.SetWalkDirection(movement);
    }
    

}
