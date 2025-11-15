using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    Renderer[] dissolveRenderers;
    MaterialPropertyBlock propertyBlock;

    List<Transform> SoundLocations = new List<Transform>();

    IInteractable interactable;

    [SerializeField]
    [Range(-2f, 2f)]
    public float dissolveAmount;
    void Start()
    {
        interactable = GetComponentInChildren<IInteractable>() != null ? GetComponentInChildren<IInteractable>() : null;
        dissolveRenderers = GetComponentsInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        foreach(Renderer dissolveRenderer in dissolveRenderers)
        {
        
        dissolveRenderer.GetPropertyBlock(propertyBlock);
        dissolveAmount = -2f;
        propertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
        dissolveRenderer.SetPropertyBlock(propertyBlock);
        }
    }

    void OnApplicationQuit()
    {
        foreach(Renderer dissolveRenderer in dissolveRenderers)
        {
        
        dissolveRenderer.GetPropertyBlock(propertyBlock);
        dissolveAmount = 2f;
        propertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
        dissolveRenderer.SetPropertyBlock(propertyBlock);
        }
    }

    public void TryStartOutline(Transform direction)
    {
        if (SoundLocations.Count == 0)
        {
            propertyBlock.SetVector("_SoundOrigin", direction.position);
            foreach(Renderer dissolveRenderer in dissolveRenderers)
            {
                dissolveRenderer.SetPropertyBlock(propertyBlock);
            }
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
            foreach(Renderer dissolveRenderer in dissolveRenderers)
            {
                dissolveRenderer.SetPropertyBlock(propertyBlock);
            }
            StopAllCoroutines();
            StartCoroutine(DissolveOut());
        }
    }

    IEnumerator DissolveIn()
    {
        if (interactable != null)
        {
            interactable.Interactable = true;
        }
        while (dissolveAmount < 2f)
        {
            dissolveAmount += Time.deltaTime * 3f;
            propertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
            foreach(Renderer dissolveRenderer in dissolveRenderers)
            {
                dissolveRenderer.SetPropertyBlock(propertyBlock);
            }
            yield return null;
        }
    }

    IEnumerator DissolveOut()
    {
        if (interactable != null)
        {
            interactable.Interactable = false;
        }    
        while (dissolveAmount > -2f)
        {
            dissolveAmount -= Time.deltaTime * 3f;
            propertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
            foreach(Renderer dissolveRenderer in dissolveRenderers)
            {
                dissolveRenderer.SetPropertyBlock(propertyBlock);
            }
            yield return null;
        }
    }
}
