using UnityEngine;

public class ClockTick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.clock, gameObject, Room.OpenSpace, false).start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
