using UnityEngine;

public class AudioAssigner : MonoBehaviour
{
    [SerializeField]
    Room room;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameAudioManager.Instance.NoLongerOccludeSounds(room);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameAudioManager.Instance.OccludeSounds(room);
        }    
    }
}
