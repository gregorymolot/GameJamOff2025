using FMOD.Studio;
using UnityEngine;

public class InteractableSwitch : MonoBehaviour, IInteractable
{
    [SerializeField]
    Vector3 onPosition;
    [SerializeField]
    Vector3 offPosition;
    bool isOn = false;

    public bool Returnable { get => false; set{} }
    public bool Interactable { get => interactable; set=>interactable = value; }
    [SerializeField]
    private bool interactable;
    [SerializeField]
    Room room;
    [SerializeField]
    Transform bulb;
    SphereSoundEmitter emitter;
    EventInstance instance;

    private void Start() {
        transform.localRotation = Quaternion.Euler(offPosition);
        instance = GameAudioManager.Instance.CreateInstance(FMODEvents.Instance.flickerLight, bulb.gameObject, Room.MechanicalRoom, true);
        instance.stop(STOP_MODE.IMMEDIATE);
    }

    public void Interact()
    {
        GameAudioManager.Instance.PlayOneShot(FMODEvents.Instance.flickSwitch, transform.position);
        isOn = !isOn;
        if (isOn)
        {
            emitter = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(bulb.transform.position, 5f, 2f, room);
            instance.start();
        }
        else
        {
            instance.stop(STOP_MODE.IMMEDIATE);
            emitter.EndSound();
            emitter = null;
        }
        transform.localRotation = isOn ? Quaternion.Euler(onPosition) : Quaternion.Euler(offPosition);
    }

    public void Return()
    {
    }

}
