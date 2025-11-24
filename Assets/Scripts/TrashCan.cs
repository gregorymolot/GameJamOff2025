using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public void Kick()
    {
        SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 5f, 0.5f, Room.OpenSpace, 3f);
    }
}
