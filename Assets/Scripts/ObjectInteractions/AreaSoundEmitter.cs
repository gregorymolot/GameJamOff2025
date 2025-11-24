using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AreaSoundEmitter : BaseSoundEmitter
{
    BoxCollider box;

    Vector3 target;

    float timeToMax;

    void Awake()
    {
        box = GetComponent<BoxCollider>();
        box.enabled = false;
        target = box.size;
        box.size = Vector3.zero;
    }

    public void StartSoundGrowth(float timeToMax)
    {
        box.enabled = true;
        this.timeToMax = timeToMax;
        StartCoroutine(GrowToMax());
    }

    public void StartSoundGrowth(float timeToMax, float timeAtMax)
    {
        box.enabled = true;
        this.timeToMax = timeToMax;
        StartCoroutine(GrowToMax(timeAtMax));
    }

    IEnumerator GrowToMax()
    {
        yield return null;
        float timer = 0f;
        Vector3 currentSize = Vector3.zero;
        while(timer < timeToMax)
        {
            box.size = Vector3.Lerp(currentSize, target, timer/timeToMax);
            timer+=Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator GrowToMax(float timeAtMax)
    {
        yield return null;
        float timer = 0f;
        Vector3 currentSize = Vector3.zero;
        while(timer < timeToMax)
        {
            box.size = Vector3.Lerp(currentSize, target, timer/timeToMax);
            timer+=Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(timeAtMax);
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        float timer = 0f;
        while(timer < timeToMax)
        {
            box.size = Vector3.Lerp(target, Vector3.zero, timer/timeToMax);
            timer+=Time.deltaTime;
            yield return null;
        }
        box.enabled = false;
                Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, box.size/2f, Quaternion.identity, LayerMask.NameToLayer("Findable"));
        foreach(Collider collider in colliders)
        {
            collider.GetComponentInParent<Dissolve>().TryStartDissolve(transform);
        }
    }
}

