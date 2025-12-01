using FMOD.Studio;
using FMODUnity;
using UnityEngine;


public class RemoteInteract : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable;

    [SerializeField]
    Transform tvTransform;

    bool soundOn = false;

    SphereSoundEmitter emitter;

    [SerializeField]
    Room room;

    EventInstance tvInstance;

    void Start()
    {
        tvInstance = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.tvEffect, tvTransform.gameObject);
    }

    public void Interact()
    {
        GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.tvOn);
        if (!soundOn)
        {
            tvInstance.start();
            emitter = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(tvTransform.position, 15f, 3f, room);
            soundOn = true;
        }
        else
        {
            emitter.EndSound();
            tvInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            emitter = null;
            soundOn = false;
        }
    }

    public void Return()
    {
    }
}
