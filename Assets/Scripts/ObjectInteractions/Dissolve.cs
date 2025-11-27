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
    Safe,
    Person
}

public class Dissolve : MonoBehaviour
{
    Renderer[] dissolveRenderers;
    MaterialPropertyBlock propertyBlock;

    List<Transform> SoundLocations = new List<Transform>();

    IInteractable[] interactables;

    [SerializeField]
    bool startIn;

    [SerializeField]
    [Range(-2f, 2f)]
    public float dissolveAmount;

    [SerializeField]
    public Room room;

    void Awake()
    {
        InitializeMaterial();
    }

    void OnEnable()
    {
        EventManager.Game.BeginGame += InitializeDissolve;
    }

    void OnDisable()
    {
        EventManager.Game.BeginGame -= InitializeDissolve;
    }

    void InitializeMaterial()
    {
        Material dissolveMaterial = Resources.Load<Material>("DissolveMaterial");
        dissolveRenderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer dissolveRenderer in dissolveRenderers)
        {
            if (!dissolveRenderer.sharedMaterial.name.Contains("Dissolve"))
            {
                dissolveRenderer.sharedMaterial = dissolveMaterial;
            }
        }
    }

    public void InitializeDissolve()
    {
        interactables = GetComponentsInChildren<IInteractable>() != null ? GetComponentsInChildren<IInteractable>() : null;
        propertyBlock = new MaterialPropertyBlock();
        foreach(Renderer dissolveRenderer in dissolveRenderers)
        {
            dissolveRenderer.GetPropertyBlock(propertyBlock);
            dissolveAmount = startIn ? 2f : -2f;
            propertyBlock.SetFloat("_DissolveAmount", dissolveAmount);
            dissolveRenderer.SetPropertyBlock(propertyBlock);
        }
        if (interactables != null)
        {
            foreach(IInteractable interactable in interactables)
            {
                interactable.Interactable = startIn;
            }
        }
        if (gameObject.tag != "Door")
        {
            gameObject.tag = "Findable";
        }
        if (!TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    void OnApplicationQuit()
    {
        if (dissolveRenderers == null || propertyBlock == null)
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
        if (interactables != null)
        {
            foreach(IInteractable interactable in interactables)
            {
                interactable.Interactable = true;
            }
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
        if (interactables != null)
        {
            foreach(IInteractable interactable in interactables)
            {
                interactable.Interactable = false;
            }
        }
    }
}
