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

    public void TurnOffCanvas()
    {
        discoverCanvas.SetActive(false);
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

    IEnumerator TextOut()
    {
        yield return new WaitForSeconds(3f);
        discoverText.SetTrigger("PopOut");
        alreadytext = false;
    }
}
