using UnityEngine;

public class SinkEmitter : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.waterDrip, transform.position);
        SphereSoundEmitterManager.Instance.SpawnSoundEmitter(other.transform.position, 2f, 0.5f, Room.MasterBathroom,  1f);
    }
}
