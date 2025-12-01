using System.Collections;
using UnityEngine;

public class AlarmEmitter : MonoBehaviour
{
    [SerializeField]
    float timerAmount;

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
        GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.alarmClock, gameObject, Room.Bathroom, true);
        StartCoroutine(AlarmSound());
    }

    IEnumerator AlarmSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerAmount);
            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 1f, 0.5f, Room.Bathroom, 4f);
        }
    }
}
