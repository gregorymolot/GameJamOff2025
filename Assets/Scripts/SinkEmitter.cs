using UnityEngine;

public class SinkEmitter : MonoBehaviour
{
    [SerializeField]
    AreaSoundEmitter areaSound;
    private void OnParticleCollision(GameObject other)
    {
        SphereSoundEmitterManager.Instance.SpawnSoundEmitter(other.transform.position, 2f, 0.5f, Room.MasterBathroom,  1f);
    }
}
