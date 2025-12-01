using UnityEngine;

public class AudioAssigner : MonoBehaviour
{
    [SerializeField]
    Room room;

    int amountIn = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (amountIn == 0)
            {
                GameAudioManager.Instance.NoLongerOccludeSounds(room);
            }
            amountIn+=1;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            amountIn -=1;
            if (amountIn == 0)
            {
                GameAudioManager.Instance.OccludeSounds(room);
            }
        }    
    }
}
