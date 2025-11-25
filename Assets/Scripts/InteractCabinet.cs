using System.Collections;
using UnityEngine;

public class InteractCabinet : MonoBehaviour, IInteractable
{
    [SerializeField]
    float targetX;

    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    [SerializeField]
    private bool interactable;
    private bool isOpen = false;
    Vector3 target;

    public void Interact()
    {
        StopAllCoroutines();
        if (isOpen)
        {
            StartCoroutine(Close());
            isOpen = false;
        }
        else
        {
            StartCoroutine(Open());
            isOpen = true;
        }
    }

    public void Return()
    {
    }

    IEnumerator Open()
    {
        while(transform.localPosition.x != targetX)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(targetX,0,0), Time.deltaTime * 5f);
            yield return null;
        }
    }
    IEnumerator Close()
    {
        while(transform.localPosition.x != 0)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0,0,0), Time.deltaTime * 5f);
            yield return null;
        }
    }
}
