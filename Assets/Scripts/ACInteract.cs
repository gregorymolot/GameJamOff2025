using FMOD.Studio;
using UnityEngine;

public class ACInteract : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    [SerializeField]
    private bool interactable;

    [SerializeField]
    Transform acTransform;

    bool soundOn = false;

    SphereSoundEmitter emitter;

    [SerializeField]
    Room room;

    EventInstance acInstance;
    EventInstance switchInstance;

    void Start()
    {
        acInstance = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.acVent, acTransform.gameObject, Room.WalkInCloset, true);
        switchInstance = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.acSwitch, gameObject, Room.MechanicalRoom, false);
    }

    public void Interact()
    {
        if (!soundOn)
        {
            acInstance.start();
            switchInstance.start();
            emitter = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(acTransform.position, 15f, 3f, room);
            soundOn = true;
        }
        else
        {
            emitter.EndSound();
            switchInstance.stop(STOP_MODE.IMMEDIATE);
            acInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            emitter = null;
            soundOn = false;
        }
    }

    public void Return()
    {
    }
}
