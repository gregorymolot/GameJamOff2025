using UnityEngine;
using UnityEngine.InputSystem;

public class CutsceneController : MonoBehaviour
{

    void OnEnable()
    {
        GetComponent<PlayerInput>().enabled = true;
    }

    void OnDisable()
    {
        GetComponent<PlayerInput>().enabled = false;
    }

    public void EndCutscene(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.Game.EndCutscene?.Invoke();
        }
    }
}
