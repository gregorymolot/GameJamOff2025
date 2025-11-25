using UnityEngine;

public class InterackableLockedDrawer : InteractableLock
{
    bool open;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected override void UnlockedAction()
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

    protected override void UnlockAction()
    {
        UnlockedAction();
    }
}
