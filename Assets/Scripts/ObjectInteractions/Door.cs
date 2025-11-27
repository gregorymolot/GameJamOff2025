using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable = true;
    [SerializeField]
    Room room;

    Animator animator;

    bool opened = false;

    [SerializeField]
    bool startingDoor;

    void Start()
    {
        animator = transform.parent.parent.GetComponent<Animator>();
    }

    public void Interact()
    {
        SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 2f, 0.5f, room, 3f);
        animator.ResetTrigger("Open");
        animator.ResetTrigger("Close");
        if (opened)
        {
            animator.SetTrigger("Close");
            opened = false;
        }
        else
        {
            animator.SetTrigger("Open");
            opened = true;
        }
    }

    public void Return()
    {
    }
}
