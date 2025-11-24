using UnityEngine;

public class RoomAssigner : MonoBehaviour
{
    [SerializeField]
    Room room;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Emitter"))
        {
            other.GetComponent<BaseSoundEmitter>().assignedRoom = room;
        }
        if (other.CompareTag("Findable"))
        {
            other.GetComponentInParent<Dissolve>().room = room;
        }
    }
}
