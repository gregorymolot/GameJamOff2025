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

    void Start()
    {
        animator = GetComponent<Animator>();
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
    }

    public void PlaySound()
    {
        emitter = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 15f, 3f, room);
    }
}
