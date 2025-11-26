using System.Collections;
using UnityEngine;

public class AlarmEmitter : MonoBehaviour
{
    [SerializeField]
    float timerAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AlarmSound());
    }

    IEnumerator AlarmSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerAmount);
            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 5f, 0.5f, Room.Bathroom, 5f);
        }
    }
}
