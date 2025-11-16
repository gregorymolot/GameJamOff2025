using System.Collections;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Timeline;

public class Safe : MonoBehaviour, IInteractable
{
    enum SafeState
    {
        FirstNum,
        SecondNum,
        ThirdNum
    }
    [SerializeField]
    float firstRotation;

    [SerializeField]
    float secondRotation;

    [SerializeField]
    float thirdRotation;

    [SerializeField]
    GameObject dial;

    [SerializeField]
    CinemachineCamera safeCamera;

    Animator animator;

    SafeState safeState = SafeState.FirstNum;

    public bool Returnable { get => returnable; set => returnable = value; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable = true;
    private bool returnable = false;

    bool returning;

    float direction;

    bool solved = false;

    float speed = 1f;



    void Awake()
    {
        safeCamera.enabled = false;
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        ControllerManager.Instance.SwapCurrentController(ControllerType.Safe);
        safeCamera.enabled = true;
        StopAllCoroutines();
        dial.transform.localRotation = Quaternion.identity;
        StartCoroutine(RotateDial());
    }

    public void Restart()
    {
        StartCoroutine(ReturnDial());
    }

    public void Return()
    {
        ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
        safeCamera.enabled = false;
        StopCoroutine(RotateDial());
        safeState = SafeState.FirstNum;
        StartCoroutine(ReturnDial());
    }

    void UnlockSafe()
    {
        safeCamera.transform.parent = null;
        animator.SetTrigger("Open");
        interactable = false;
        Return();
    }

    public void Rotate(float direction)
    {
        this.direction = direction;
    }

    IEnumerator RotateDial()
    {
        float previousDirection = 0;
        while (solved == false)
        {
            while(returning)
            {
                yield return null;
            }
            if (previousDirection == direction && direction != 0)
            {
                speed*= 1.008f;
                speed = Mathf.Clamp(speed, 1f, 30f);
            }
            else
            {
                speed = 1f;
            }
            dial.transform.Rotate(-transform.right, direction * Time.deltaTime * speed);
            yield return null;
            switch(safeState)
            {
                case SafeState.FirstNum:
                if (Approximately(dial.transform.localRotation.eulerAngles.x, firstRotation, 5f) && direction >= 0f)
                    {
                        Debug.Log("In range!");
                        if (direction == 0)
                        {
                            Debug.Log("Locked In!");
                            safeState = SafeState.SecondNum;
                        }
                    }
                break;
                case SafeState.SecondNum:
                if (Approximately(dial.transform.localRotation.eulerAngles.x, secondRotation, 5f) && direction <= 0f)
                    {
                        Debug.Log("In range!");
                        if (direction == 0)
                        {
                            Debug.Log("Locked In!");
                            safeState = SafeState.ThirdNum;
                        }
                    }
                break;
                case SafeState.ThirdNum:
                if (Approximately(dial.transform.localRotation.eulerAngles.x, thirdRotation, 5f) && direction >= 0f)
                    {
                        Debug.Log("In range!");
                        if (direction == 0)
                        {
                            UnlockSafe();
                        }
                    }
                break;

            }
            previousDirection = direction;
            yield return null;
        }
    }

    bool Approximately(float a, float b, float difference)
    {
        return Mathf.Abs(a) > Mathf.Abs(b)-difference && Mathf.Abs(a)<Mathf.Abs(b)+difference;
    }

    IEnumerator ReturnDial()
    {
        returning = true;
        float timer = 0f;
        Quaternion startingRotation = dial.transform.localRotation;
        while (timer < 1f)
        {
            dial.transform.localRotation = Quaternion.Slerp(startingRotation, Quaternion.identity, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        returning = false;
    }
}
