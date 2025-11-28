using UnityEngine;

public class Vent : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable = true;

    Animator animator;

    bool opened = false;

    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    public void Interact()
    {
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
