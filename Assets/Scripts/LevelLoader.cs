using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader instance;
    public static LevelLoader Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject manager = Instantiate(Resources.Load<GameObject>("LevelLoader"));
            }
            return instance;
        }
        private set => instance = value;
    }

    [SerializeField] private GameObject loadingCanvas;

    [SerializeField] private Image image;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        loadingCanvas.SetActive(true);
        yield return null;
        float dissolveAmount = -2f;
        float timer = 0f;

        while (dissolveAmount < 2f)
        {
            dissolveAmount = Mathf.Lerp(-2f, 2f, timer);
            image.material.SetFloat("_DissolveAmount", dissolveAmount);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        timer = 0f;

        Time.timeScale = 1f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        do
        {
            //Loading bar in here?
            yield return null;
        } while (!operation.isDone);


        while (dissolveAmount > -2f)
        {
            dissolveAmount = Mathf.Lerp(2f, -2f, timer);
            image.material.SetFloat("_DissolveAmount", dissolveAmount);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        //Turn off canvas
        loadingCanvas.SetActive(false);
    }
}
