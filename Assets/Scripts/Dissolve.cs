using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Dissolve : MonoBehaviour
{
    Renderer dissolveRenderer;
    MaterialPropertyBlock propertyBlock;

    List<Transform> SoundLocations = new List<Transform>();

    [SerializeField]
    [Range(-2f, 2f)]
    public float dissolveAmount;
    void Start()
    {
        dissolveRenderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        dissolveRenderer.GetPropertyBlock(propertyBlock);
        dissolveAmount = -2f;
        propertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
        dissolveRenderer.SetPropertyBlock(propertyBlock);
    }

    void OnApplicationQuit()
    {
        dissolveRenderer.material.SetFloat("_DissolveAmount", 2f);
    }

    public void TryStartOutline(Transform direction)
    {
        if (SoundLocations.Count == 0)
        {
            propertyBlock.SetVector("_SoundOrigin", direction.position);
            dissolveRenderer.SetPropertyBlock(propertyBlock);
            StopAllCoroutines();
            StartCoroutine(DissolveIn());
        }
        SoundLocations.Add(direction);
    }

    public void TryStartDissolve(Transform direction)
    {
        SoundLocations.Remove(direction);
        if (SoundLocations.Count == 0)
        {
            propertyBlock.SetVector("_SoundOrigin", direction.position);
            dissolveRenderer.SetPropertyBlock(propertyBlock);
            StopAllCoroutines();
            StartCoroutine(DissolveOut());
        }
    }

    IEnumerator DissolveIn()
    {
        while (dissolveAmount < 2f)
        {
            dissolveAmount += Time.deltaTime * 3f;
            propertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
            dissolveRenderer.SetPropertyBlock(propertyBlock);
            yield return null;
        }
    }

    IEnumerator DissolveOut()
    {
        while (dissolveAmount > -2f)
        {
            dissolveAmount -= Time.deltaTime * 3f;
            propertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
            dissolveRenderer.SetPropertyBlock(propertyBlock);
            yield return null;
        }
    }
}
