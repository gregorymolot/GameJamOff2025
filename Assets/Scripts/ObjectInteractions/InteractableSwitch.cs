using UnityEngine;

public class InteractableSwitch : MonoBehaviour, IInteractable
{
    [SerializeField]
    Vector3 onPosition;
    [SerializeField]
    Vector3 offPosition;
    bool isOn = false;

    public bool Returnable { get => false; set{} }
    public bool Interactable { get => true; set{} }

    public void Interact()
    {
        isOn = !isOn;
        EventManager.Items.ToggleSwitch?.Invoke(isOn);
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
