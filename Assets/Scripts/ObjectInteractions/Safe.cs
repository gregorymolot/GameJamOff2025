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
    GameObject canvas;

    [SerializeField]
    CinemachineCamera safeCamera;

    Animator animator;

    SafeState safeState = SafeState.FirstNum;

    bool playedSound;

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
        canvas.SetActive(true);
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
        canvas.SetActive(true);
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
        if (returning == true)
        {
            return;
        }
        this.direction = direction;
    }

    IEnumerator RotateDial()
    {
        float deltaDirection = 0;
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
            deltaDirection += direction * Time.deltaTime * speed;
            if (Mathf.Abs(deltaDirection) >=3)
            {
                GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.safeSmallClicks);
                deltaDirection = 0;
            }
            dial.transform.Rotate(-transform.up, direction * Time.deltaTime * speed);
            yield return null;
            switch(safeState)
            {
                case SafeState.FirstNum:
                if (Approximately(dial.transform.localRotation.eulerAngles.x, firstRotation, 5f) && direction >= 0f)
                    {
                        if (playedSound==false)
                        {
                            playedSound = true;
                            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 2f, 0.5f, Room.Safe, 0.5f);
                            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.safeBigClicks);
                        }
                        if (direction == 0)
                        {
                            Debug.Log("Locked In!");
                            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.safeBigClicks);
                            safeState = SafeState.SecondNum;
                            playedSound = false;
                        }
                    }
                break;
                case SafeState.SecondNum:
                if (Approximately(dial.transform.localRotation.eulerAngles.x, secondRotation, 5f) && direction <= 0f)
                    {
                        if (playedSound==false)
                        {
                            playedSound = true;
                            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 2f, 0.5f, Room.Safe, 0.5f);
                            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.safeBigClicks);
                        }
                        if (direction == 0)
                        {
                            Debug.Log("Locked In!");
                            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.safeBigClicks);
                            safeState = SafeState.ThirdNum;
                            playedSound = false;
                        }
                    }
                break;
                case SafeState.ThirdNum:
                if (Approximately(dial.transform.localRotation.eulerAngles.x, thirdRotation, 5f) && direction >= 0f)
                    {
                        if (playedSound==false)
                        {
                            playedSound = true;
                            SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 2f, 0.5f, Room.Safe, 0.5f);
                            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.safeBigClicks);
                        }
                        if (direction == 0)
                        {
                            GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.safeOpen);
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
        playedSound = false;
        safeState = SafeState.FirstNum;
        direction = 0;
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
