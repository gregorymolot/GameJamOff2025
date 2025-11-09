using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    InfoPopup infoPopup;

    [SerializeField]
    GameObject discoverCanvas;

    [SerializeField]
    GameObject profileCanvas;

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
    }

    void OnDisable()
    {
        EventManager.Items.ShowItem -= InitializeScreen;
        EventManager.Items.Return -= DisappearScreen;
    }

    void InitializeScreen(InteractableItem item)
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

    public void InitializeProfiles()
    {
        profileCanvas.SetActive(true);
    }

    public void DeactivateProfiles()
    {
        profileCanvas.SetActive(false);
    }
}
