using UnityEngine;
using UnityEngine.InputSystem;


public class ProfileController : MonoBehaviour
{

    void OnEnable()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    void OnDisable()
    {
        GetComponent<PlayerInput>().enabled = false;
    }

    public void DeactivateProfiler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Play animation and then when finished
            ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
        }

    }
}
