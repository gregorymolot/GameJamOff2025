using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    InfoPopup infoPopup;

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
        infoPopup.Activate(item);
    }

    void DisappearScreen()
    {
        infoPopup.Deactivate();
    }
}
