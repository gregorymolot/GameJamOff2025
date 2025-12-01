using UnityEngine;

public class MoveTutorialText : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            canvas.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            canvas.enabled = false;
    }
}
