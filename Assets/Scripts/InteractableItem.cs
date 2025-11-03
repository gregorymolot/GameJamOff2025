using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableItem : MonoBehaviour, IInteractable
{

    Vector3 initialPosition;
    Vector3 targetPosition;
    Quaternion initialRotation;
    Quaternion spinningRotation;
    private bool isInteracting;

    public bool IsInteracting { get => isInteracting; set => isInteracting = value; }

    public void Interact()
    {
        IsInteracting = true;
        ControllerManager.Instance.DeactivateAllControllers();
        StartCoroutine(RotateAndBob());
        Debug.Log("Interacted with: " + gameObject.name);

        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane + 1.5f);

        // Convert to world point
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(viewportCenter);
        targetPosition = worldCenter;

        StartCoroutine(GoToMiddle(worldCenter));
    }

    public void Return()
    {
        ControllerManager.Instance.DeactivateAllControllers();
        StartCoroutine(ReturnObject());
    }
    
    IEnumerator ReturnObject()
    {
        float timer = 0f;
        while (timer < 1f)
        {
            transform.rotation = Quaternion.Slerp(spinningRotation, initialRotation, timer);
            transform.position = Vector3.Lerp(targetPosition, initialPosition, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        StopAllCoroutines();
        ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
    }

    IEnumerator GoToMiddle(Vector3 worldPosition)
    {
        initialPosition = transform.position;
        float timer = 0f;
        while (timer < 1f)
        {
            transform.position = Vector3.Lerp(initialPosition, worldPosition, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        ControllerManager.Instance.SwapCurrentController(ControllerType.Interactable);
        IsInteracting = false;
    }

    IEnumerator RotateAndBob()
    {
        initialRotation = transform.rotation;
        while (true)
        {
            transform.Rotate(Vector3.up * 3f * Time.deltaTime);
            spinningRotation = transform.rotation;
            yield return null;
        }
    }
}
