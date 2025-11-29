using UnityEngine;

public class InteractableOutlet : InteractableLock
{
    [SerializeField]
    GameObject hairDryer;

    protected override void UnlockAction()
    {
        hairDryer.SetActive(true);
        SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 15f, 2f, Room.MasterBathroom);
    }

    protected override void FailedAction()
    {
        base.FailedAction();
        EventManager.Unlocks.Unlock(Clues.Outlet);
    }
}
