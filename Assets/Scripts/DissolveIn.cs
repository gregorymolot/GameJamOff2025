using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DissolveIn : MonoBehaviour
{
    Renderer dissolveRenderer;
    MaterialPropertyBlock propertyBlock;

    [SerializeField]
    [Range(-2f, 2f)]
    float dissolveAmount;
    void Start()
    {
        dissolveRenderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        dissolveRenderer.GetPropertyBlock(propertyBlock);
    }

    // Update is called once per frame
    void Update()
    {
        propertyBlock.SetFloat("_CutoffHeight", dissolveAmount);
        dissolveRenderer.SetPropertyBlock(propertyBlock);
    }
}
