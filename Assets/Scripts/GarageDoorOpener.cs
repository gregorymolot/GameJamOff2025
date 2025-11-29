using UnityEngine;

public class GarageDoorOpener : InteractableLock
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    Transform garageDoor;

    bool isOpen = false;


    protected override void UnlockAction()
    {
        UnlockedAction();
    }

    protected override void UnlockedAction()
    {
        if (isOpen)
        {
            animator.SetTrigger("Close");
            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(garageDoor.position, 20f, 10f, Room.Garage, 10f);
        }
        else
        {
            animator.SetTrigger("Open");
            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(garageDoor.position, 20f, 10f, Room.Garage, 10f);
        }
        isOpen = !isOpen;
    }

    protected override void FailedAction()
    {
        base.FailedAction();
        EventManager.Unlocks.Unlock(Clues.GarageDoorOpener);
    }
}
