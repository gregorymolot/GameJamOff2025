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
        isOpen = !isOpen;
        if (isOpen)
        {
            animator.SetTrigger("Close");
            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(garageDoor.position, 20f, 5f, Room.Garage, 5f);
        }
        else
        {
            animator.SetTrigger("Open");
            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(garageDoor.position, 20f, 5f, Room.Garage, 5f);
        }
    }
}
