using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable = true;

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
