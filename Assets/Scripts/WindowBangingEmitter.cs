using FMOD.Studio;
using UnityEngine;

public class WindowBangingEmitter : MonoBehaviour
{
    bool playing;

    void OnEnable()
    {
        EventManager.Game.BeginGame += StartPlaying;
    }

    void OnDisable()
    {
        EventManager.Game.BeginGame -= StartPlaying;
    }

    void StartPlaying()
    {
        playing = true;
    }


    public void SpawnEmitter()
    {
        if (playing)
        {
            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.windowBang, transform.position);
        }
        SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 2f, 0.75f, Room.TVRoom, 0.75f);
    }
}
