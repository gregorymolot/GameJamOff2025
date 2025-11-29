using UnityEngine;

public class EmptyInteractCabinet : InteractCabinet
{
    public override void Interact()
    {
        base.Interact();
        EventManager.Unlocks.Unlock(Clues.EmptyCloset);
    }
}
