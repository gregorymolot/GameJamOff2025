using UnityEngine;

public class ClosetChecker : MonoBehaviour
{
    [SerializeField]
    Dissolve ventDissolve;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ventDissolve.dissolveAmount >= 2f)
        {
            EventManager.Unlocks.Unlock(Clues.EmptyCloset);
        }
    }
}
