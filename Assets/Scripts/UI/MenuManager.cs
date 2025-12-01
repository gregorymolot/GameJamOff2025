using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;

    void OnEnable()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (pauseMenu.activeInHierarchy)
        {
            GameAudioManager.Instance.Pause(false);
            if (optionsMenu.activeInHierarchy)
            {
                optionsMenu.SetActive(false);
            }
            pauseMenu.SetActive(false);
            ControllerManager.Instance.ReturnToPrevious();
        }
        else
        {
            GameAudioManager.Instance.Pause(true);
            pauseMenu.SetActive(true);
            ControllerManager.Instance.SwapCurrentController(ControllerType.Menu);
        }
    }

    public void ToggleOptions()
    {
        if (optionsMenu.activeInHierarchy)
        {
            optionsMenu.SetActive(false);
        }
        else
        {
            optionsMenu.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        LevelLoader.Instance.LoadNextScene("MainMenu");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
