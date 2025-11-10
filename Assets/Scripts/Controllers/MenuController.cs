using UnityEngine;
using UnityEngine.InputSystem;


public class MenuController : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    void OnDisable()
    {
        GetComponent<PlayerInput>().enabled = false;
    }

    // public void BackToGame(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
            
    //     }
    // }
}
