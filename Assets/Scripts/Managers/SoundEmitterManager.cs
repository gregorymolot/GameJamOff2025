using UnityEditor.Rendering;
using UnityEngine;

public class SoundEmitterManager : MonoBehaviour
{
    [SerializeField]
    GameObject soundEmitter;

    public static SoundEmitterManager Instance
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
    private static SoundEmitterManager _instance;

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

    public SoundEmitter SpawnSoundEmitter(Vector3 position, float maxSize, float timeToMax)
    {
        GameObject emitter =  Instantiate(soundEmitter, position, Quaternion.identity);
        emitter.GetComponent<SoundEmitter>().StartSoundGrowth(maxSize, timeToMax);
        return emitter.GetComponent<SoundEmitter>();
    }

    public SoundEmitter SpawnSoundEmitter(Vector3 position, float maxSize, float timeToMax, float timeAtMax)
    {
        GameObject emitter =  Instantiate(soundEmitter, position, Quaternion.identity);
        emitter.GetComponent<SoundEmitter>().StartSoundGrowth(maxSize, timeToMax, timeAtMax);
        return emitter.GetComponent<SoundEmitter>();
    }
}
