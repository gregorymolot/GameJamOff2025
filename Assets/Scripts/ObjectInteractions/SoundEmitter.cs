using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SoundEmitter : MonoBehaviour
{
    SphereCollider sphere;


    [SerializeField]
    public bool constant;

    [NonSerialized]
    public float startingSize;

    [NonSerialized]
    public float maxSize;

    [NonSerialized]
    public float timeToReachMaxSize;

    [NonSerialized]
    public float timeAtMaxSize;

    void Start()
    {
        sphere = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Findable"))
        {
            other.GetComponent<Dissolve>().TryStartOutline(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Findable"))
        {
            other.GetComponent<Dissolve>().TryStartDissolve(transform);
        }
    }
        

    void OnDrawGizmos()
    {
        if (sphere != null)
            Gizmos.DrawSphere(transform.position, sphere.radius);
    }
}

[CustomEditor(typeof(SoundEmitter))]
public class EmitterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SoundEmitter emitter = (SoundEmitter)target;

        DrawDefaultInspector();

        if (emitter.constant)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Constant Size Options", EditorStyles.boldLabel);

            emitter.startingSize = EditorGUILayout.FloatField("Constant Start Size", emitter.startingSize);
        }
        else
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Growing Size Options", EditorStyles.boldLabel);

            emitter.maxSize = EditorGUILayout.FloatField("Growing Max Size", emitter.maxSize);
            emitter.timeToReachMaxSize = EditorGUILayout.FloatField("Time To Reach Max", emitter.timeToReachMaxSize);
            emitter.timeAtMaxSize = EditorGUILayout.FloatField("Time At Max", emitter.timeAtMaxSize);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(emitter);
        }
    }
}
