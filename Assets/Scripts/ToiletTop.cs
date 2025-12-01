using System.Collections;
using UnityEngine;

public class ToiletTop : MonoBehaviour, IInteractable
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
        GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.toiletBowlOpen, transform.position);
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
        while(transform.localEulerAngles.x != targetZ)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(targetZ,0, 0), Time.deltaTime * 90f);
            yield return null;
        }
    }
    IEnumerator Close()
    {
        while(transform.localEulerAngles.x != 0)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0,0, 0), Time.deltaTime * 90f);
            yield return null;
        }
    }
}
