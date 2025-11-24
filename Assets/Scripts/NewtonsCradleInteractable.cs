using UnityEngine;

public class NewtonsCradleInteractable : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    [SerializeField]
    private bool interactable;

    bool soundOn = false;

    SphereSoundEmitter emitter;

    [SerializeField]
    Room room;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!soundOn)
        {
            animator.SetTrigger("Interact");
            emitter = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 10f, 3f, room);
            soundOn = true;
        }
        else
        {
            animator.SetTrigger("Stop");
            emitter.EndSound();
            emitter = null;
            soundOn = false;
        }
    }

    public void Return()
    {
    }
}
