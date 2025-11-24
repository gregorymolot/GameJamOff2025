using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Room
{
    OpenSpace,
    MasterBedroom,
    WalkInCloset,
    MasterBathroom,
    Office,
    TVRoom,
    Bedroom,
    Bathroom,
    Garage,
    MechanicalRoom,
    LaundryRoom,
    Safe
}

public class Dissolve : MonoBehaviour
{
    Renderer[] dissolveRenderers;
    MaterialPropertyBlock propertyBlock;

    List<Transform> SoundLocations = new List<Transform>();

    IInteractable interactable;

    [SerializeField]
    [Range(-2f, 2f)]
    public float dissolveAmount;

    [SerializeField]
    public Room room;

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Material dissolveMaterial = Resources.Load<Material>("DissolveMaterial");
        interactable = GetComponentInChildren<IInteractable>() != null ? GetComponentInChildren<IInteractable>() : null;
        dissolveRenderers = GetComponentsInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        foreach(Renderer dissolveRenderer in dissolveRenderers)
        {
            dissolveRenderer.material = dissolveMaterial;
            dissolveRenderer.GetPropertyBlock(propertyBlock);
            dissolveAmount = -2f;
            propertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
            dissolveRenderer.SetPropertyBlock(propertyBlock);
        }
        if (interactable != null)
        {
            interactable.Interactable = false;
        }
        gameObject.tag = "Findable";
        if (!TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    void OnApplicationQuit()
    {
        if (dissolveRenderers == null)
        {
            return;
        }
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
        if (SoundLocations.Contains(direction))
        {
            return;
        }
        if (propertyBlock == null)
        {
            return;
        }
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
                if (propertyBlock == null)
        {
            return;
        }
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
        if (interactable != null)
        {
            interactable.Interactable = false;
        }    
    }
}
