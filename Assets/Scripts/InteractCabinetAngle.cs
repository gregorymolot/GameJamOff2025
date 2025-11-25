using System.Collections;
using UnityEngine;

public class InteractCabinetAngle : MonoBehaviour, IInteractable
{
    [SerializeField]
    float targetZ;

    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    [SerializeField]
    private bool interactable;
    private bool isOpen = false;

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
        while(transform.localEulerAngles.z != targetZ)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0,0, targetZ), Time.deltaTime * 60f);
            yield return null;
        }
    }
    IEnumerator Close()
    {
        while(transform.localEulerAngles.z != 0)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0,0, 0), Time.deltaTime * 60f);
            yield return null;
        }
    }
}
