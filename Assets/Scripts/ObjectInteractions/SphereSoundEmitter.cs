using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SphereSoundEmitter : BaseSoundEmitter
{
    SphereCollider sphere;
    float timeToMax;

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    public void StartSoundGrowth(float maxSize, float timeToMax)
    {
        this.timeToMax = timeToMax;
        StartCoroutine(GrowToMax(maxSize));
    }

    public void StartSoundGrowth(float maxSize, float timeToMax, Room room, float timeAtMax)
    {
        this.assignedRoom = room;
        this.timeToMax = timeToMax;
        StartCoroutine(GrowToMax(maxSize, timeAtMax));
    }

    public void EndSound()
    {
        StopAllCoroutines();
        StartCoroutine(Shrink());
    }

    IEnumerator GrowToMax(float max)
    {
        yield return null;
        float timer = 0f;
        while(timer < timeToMax)
        {
            sphere.radius = Mathf.Lerp(0, max, timer/timeToMax);
            timer+=Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator GrowToMax(float max, float timeAtMax)
    {
        yield return null;
        float timer = 0f;
        while(timer < timeToMax)
        {
            sphere.radius = Mathf.Lerp(0, max, timer/timeToMax);
            timer+=Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(timeAtMax);
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        float timer = 0f;
        float currentSize = sphere.radius;
        while(timer < timeToMax)
        {
            sphere.radius = Mathf.Lerp(currentSize, 0, timer/timeToMax);
            timer+=Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
        

    void OnDrawGizmos()
    {
        if (sphere != null)
            Gizmos.DrawSphere(transform.position, sphere.radius);
    }
}

