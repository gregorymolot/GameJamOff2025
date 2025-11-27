using UnityEngine;

public class StartingDoor : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = false; }
    [SerializeField]
    private bool interactable = true;

    public void Interact()
    {
        CutsceneManager.Instance.StartCutscene();
        interactable=false;
    }

    public void Return()
    {
    }
}
