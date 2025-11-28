using System.Collections;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    bool canMakeSound = true;
    void OnTriggerEnter(Collider other)
    {
        if (canMakeSound)
        {
            canMakeSound = false;
            Debug.Log("Ow!");
        }
    }

    IEnumerator AllowHits()
    {
        yield return new WaitForSeconds(0.5f);
        canMakeSound = true;
    }
}
