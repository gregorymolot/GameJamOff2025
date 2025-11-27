using UnityEngine;

public class HitChecker : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello!");
    }
}
