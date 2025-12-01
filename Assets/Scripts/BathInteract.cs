using FMOD.Studio;
using UnityEngine;

public class BathInteract : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable;

    [SerializeField]
    Transform waterHeaterTransform;

    bool soundOn = false;

    SphereSoundEmitter emitter;

    [SerializeField]
    Room room;

    EventInstance heaterSound;
    EventInstance bathSound;

    void Start()
    {
        heaterSound = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.waterHeater, waterHeaterTransform.gameObject, Room.LaundryRoom, true);
        bathSound = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.bath, gameObject, Room.MasterBathroom, false);
    }

    public void Interact()
    {
        if (!soundOn)
        {
            emitter = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(waterHeaterTransform.position, 15f, 3f, room);
            heaterSound.start();
            bathSound.start();
            soundOn = true;
        }
        else
        {
            heaterSound.stop(STOP_MODE.IMMEDIATE);
            bathSound.stop(STOP_MODE.IMMEDIATE);
            emitter.EndSound();
            emitter = null;
            soundOn = false;
        }
    }

    public void Return()
    {
    }
}
