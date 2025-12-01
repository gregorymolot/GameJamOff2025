using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        // RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        LevelLoader.Instance.LoadNextScene("GameScene");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
