using FMOD.Studio;
using UnityEngine;

public class InteractableOutlet : InteractableLock
{
    [SerializeField]
    GameObject hairDryer;
    EventInstance instance;

    protected override void UnlockAction()
    {
        hairDryer.SetActive(true);
        SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 15f, 2f, Room.MasterBathroom);
        instance = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.hairDryer, hairDryer, Room.MasterBathroom, false);
        instance.start();
    }

    protected override void FailedAction()
    {
        base.FailedAction();
        EventManager.Unlocks.Unlock(Clues.Outlet);
    }
}
