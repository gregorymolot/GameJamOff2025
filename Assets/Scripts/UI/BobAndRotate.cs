using System.Collections;
using UnityEngine;

public class BobAndRotate : MonoBehaviour
{

    public float speed;

    public float height;

    public float rotationSpeed;

    float originalY;

    void Start()
    {
        originalY = transform.position.y;
        StartCoroutine(Bobbing());
        StartCoroutine(Rotating());
    }

    IEnumerator Bobbing()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x,
                originalY + ((float)Mathf.Sin(Time.unscaledTime * speed) * height),
                transform.position.z);
            yield return null;
        }
    }

    IEnumerator Rotating()
    {
        while (true)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.unscaledDeltaTime);
            yield return null;
        }
    }
}
