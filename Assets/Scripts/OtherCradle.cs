using UnityEngine;
using UnityEngine.Analytics;

public class OtherCradle : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator.SetTrigger("Interact");
    }
}
