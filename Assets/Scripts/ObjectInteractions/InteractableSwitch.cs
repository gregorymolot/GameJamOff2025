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
    private bool interactable;
    [SerializeField]
    Room room;
    [SerializeField]
    Transform bulb;
    SphereSoundEmitter emitter;

    public void Interact()
    {
        isOn = !isOn;
        if (isOn)
        {
            emitter = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(bulb.transform.position, 5f, 2f, room);
        }
        else
        {
            emitter.EndSound();
            emitter = null;
        }
        transform.localRotation = isOn ? Quaternion.Euler(onPosition) : Quaternion.Euler(offPosition);
    }

    public void Return()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localRotation = Quaternion.Euler(offPosition);
    }
}
