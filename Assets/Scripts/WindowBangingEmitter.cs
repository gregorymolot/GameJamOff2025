using UnityEngine;

public class WindowBangingEmitter : MonoBehaviour
{
    public void SpawnEmitter()
    {
        SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 2f, 0.75f, Room.TVRoom, 0.75f);
    }
}
