using FMOD.Studio;
using UnityEngine;

public class Microwave : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable;
    SphereSoundEmitter sphereSound;

    EventInstance instance;

    void Start()
    {
        instance = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.microwave, gameObject, Room.OpenSpace, false);
        instance.stop(STOP_MODE.IMMEDIATE);
    }

    public void Interact()
    {
        instance.start();
        if (sphereSound == null)
        {
            sphereSound = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 8f, 1f, Room.OpenSpace, 30f);
        }
        else
        {
            sphereSound.EndSound();
            sphereSound = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 8f, 1f, Room.OpenSpace, 30f);
        }
    }

    public void Return()
    {
    }
}
