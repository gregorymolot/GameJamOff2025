using FMOD.Studio;
using UnityEngine;

public class CradleSound : MonoBehaviour
{
    EventInstance instance;

    [SerializeField]
    Room room;

    void Start()
    {
        instance = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.newtonsCradle, gameObject, room, true);
    }

    public void PlayHit()
    {
        instance.start();
    }
}
