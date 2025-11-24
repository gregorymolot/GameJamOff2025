using UnityEditor.Rendering;
using UnityEngine;

public class SphereSoundEmitterManager : MonoBehaviour
{
    [SerializeField]
    GameObject soundEmitter;

    public static SphereSoundEmitterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No Controller");
            }
            return _instance;
        }
    }
    private static SphereSoundEmitterManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public SphereSoundEmitter SpawnSoundEmitter(Vector3 position, float maxSize, float timeToMax)
    {
        GameObject emitter =  Instantiate(soundEmitter, position, Quaternion.identity);
        emitter.GetComponent<SphereSoundEmitter>().StartSoundGrowth(maxSize, timeToMax);
        return emitter.GetComponent<SphereSoundEmitter>();
    }

    public SphereSoundEmitter SpawnSoundEmitter(Vector3 position, float maxSize, float timeToMax, Room room, float timeAtMax)
    {
        GameObject emitter =  Instantiate(soundEmitter, position, Quaternion.identity);
        emitter.GetComponent<SphereSoundEmitter>().StartSoundGrowth(maxSize, timeToMax, room, timeAtMax);
        return emitter.GetComponent<SphereSoundEmitter>();
    }
}
