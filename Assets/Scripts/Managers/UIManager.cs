using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    InfoPopup infoPopup;

    [SerializeField]
    GameObject discoverCanvas;

    [SerializeField]
    GameObject profileCanvas;
    [SerializeField]
    Animator discoverText;

    [SerializeField]
    TextMeshProUGUI interactText;

    [SerializeField]
    EndgamePopup endGame;

    [SerializeField]
    TextMeshProUGUI failText;
    string currentFailText;

    [SerializeField]
    TextMeshProUGUI tutorialText;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No Controller");
            }
            return _instance;
        }
    }
    private static UIManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        EventManager.Items.ShowItem += InitializeScreen;
        EventManager.Items.Return += DisappearScreen;
        EventManager.Unlocks.NewUnlock += DisplayProfileText;
    }

    void OnDisable()
    {
        EventManager.Items.ShowItem -= InitializeScreen;
        EventManager.Items.Return -= DisappearScreen;
        EventManager.Unlocks.NewUnlock -= DisplayProfileText;
    }

    void InitializeScreen(DiscoverableItem item)
    {
        discoverCanvas.SetActive(true);
        infoPopup.Activate(item);
    }

    void DisappearScreen()
    {
        infoPopup.Deactivate();
    }

    public void TurnOffDiscoverCanvas()
    {
        discoverCanvas.SetActive(false);
    }

    public void ShowFailedText(string text)
    {
        StartCoroutine(DisplayFailedText(text));
    }

    public void ShowLyingTutorialText()
    {
        tutorialText.gameObject.SetActive(true);
        StartCoroutine(DisplayLyingTutorial());
    }

    IEnumerator DisplayLyingTutorial()
    {
        yield return new WaitForSeconds(2f);
        tutorialText.gameObject.SetActive(false);
    }

    IEnumerator DisplayFailedText(string text)
    {
        currentFailText = text;
        failText.text = text;
        yield return new WaitForSeconds(3f);
        failText.text = "";
    }

    public void ShowInteractText()
    {
        interactText.text = "Left Click to Interact";
    }

    public void ShowTalkText()
    {
        interactText.text = "Left Click to Talk";
    }

    public void TurnOffInteractText()
    {
        interactText.text = "";
    }

    public void InitializeProfiles()
    {
        profileCanvas.SetActive(true);
    }

    public void DeactivateProfiles()
    {
        CharacterManager.Instance.CloseTab();
    }

    public void TurnOffProfileCanvas()
    {
        profileCanvas.SetActive(false);
    }

    bool alreadytext;

    void DisplayProfileText()
    {
        if (alreadytext == false)
        {
            alreadytext = true;
            discoverText.SetTrigger("PopIn");
            StartCoroutine(TextOut());
        }
    }

    public void ShowEndScreen(Name name)
    {
        endGame.gameObject.SetActive(true);
        endGame.Activate(name);
    }

    IEnumerator TextOut()
    {
        yield return new WaitForSeconds(3f);
        discoverText.SetTrigger("PopOut");
        alreadytext = false;
    }
}
