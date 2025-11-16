using UnityEngine;
using UnityEngine.InputSystem;

public class SafeController : MonoBehaviour
{
    [SerializeField]
    Safe safe;
    [SerializeField] PlayerInput input;
    void OnEnable()
    {
        input.enabled = true;
    }

    void OnDisable()
    {
        input.enabled = false;
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            safe.Rotate(context.ReadValue<float>());
        }
    }

    public void Return(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            safe.Return();
        }
    }

    public void Restart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            safe.Restart();
        }
    }
}
