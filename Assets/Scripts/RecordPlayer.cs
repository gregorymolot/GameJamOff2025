using FMOD.Studio;
using UnityEngine;

public class RecordPlayer : InteractableLock
{
    [SerializeField]
    GameObject record;
    Animator animator;
    [SerializeField]
    GameObject recordItem;
    [SerializeField]
    Room room;

    SphereSoundEmitter emitter; 

    EventInstance instance;

    void Start()
    {
        animator = GetComponent<Animator>();
        instance = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.recordMusic, gameObject, room, false);
        instance.stop(STOP_MODE.IMMEDIATE);
    }

    protected override void UnlockAction()
    {
        record.SetActive(true);
        animator.SetTrigger("Play");
    }

    protected override void UnlockedAction()
    {
        isUnlocked = false;
        emitter.EndSound();
        emitter = null;
        animator.SetTrigger("Stop");
    }

    public void ReturnRecord()
    {
        record.SetActive(false);
        GameManager.Instance.AddToInventory(recordItem.GetComponent<InventoryItem>());
        instance.stop(STOP_MODE.IMMEDIATE);
    }

    public void PlaySound()
    {
        emitter = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 15f, 3f, room);
        instance.start();
    }

    protected override void FailedAction()
    {
        base.FailedAction();
        EventManager.Unlocks.Unlock(Clues.RecordPlayer);
    }
}
