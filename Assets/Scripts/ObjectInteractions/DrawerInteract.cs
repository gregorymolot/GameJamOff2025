using UnityEngine;

public class InteractDrawer : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => value = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    [SerializeField]
    private bool interactable;

    Animator animator;

    bool open;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }


    public void Interact()
    {
        if (open)
        {
            animator.SetTrigger("Close");
            open = false;
        }
        else
        {
            animator.SetTrigger("Open");
            open = true;
        }
    }

    public void Return()
    {
    }
}
