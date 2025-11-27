using UnityEngine;

public class MoveTutorialText : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;
    void OnTriggerEnter(Collider other)
    {
        canvas.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        canvas.enabled = false;
    }
}
