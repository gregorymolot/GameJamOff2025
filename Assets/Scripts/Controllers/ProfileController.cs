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
            UIManager.Instance.DeactivateProfiles();
            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.whoosh);
        }

    }
}
