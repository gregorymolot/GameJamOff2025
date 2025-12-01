using UnityEngine;

public class SafeSwitch : MonoBehaviour, IInteractable
{
    [SerializeField]
    Safe safe;
    [SerializeField]
    Animator paintingAnimator;
    bool isOpen;

    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    [SerializeField]
    private bool interactable;

    public void Interact()
    {
        GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.flickSwitch, transform.position);
        if (isOpen)
        {
            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.paintingMove, paintingAnimator.transform.position);
            paintingAnimator.SetTrigger("Close");
            safe.Interactable = false;
        }
        else
        {
            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.paintingMove, paintingAnimator.transform.position);
            paintingAnimator.SetTrigger("Open");
            safe.Interactable = true;

        }
        isOpen = !isOpen;
    }

    public void Return()
    {
    }

    void Start()
    {
        safe.Interactable = false;
    }
}
