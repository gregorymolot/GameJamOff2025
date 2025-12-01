using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public enum Type
{
    TV,
    Bath,
    Light
}

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
    Type type;

    [SerializeField]
    Room room;

    public void Interact()
    {
        if (!soundOn)
        {
            emitter = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(tvTransform.position, 15f, 3f, room);
            soundOn = true;
        }
        else
        {
            emitter.EndSound();
            emitter = null;
            soundOn = false;
        }
    }

    public void Return()
    {
    }
}
