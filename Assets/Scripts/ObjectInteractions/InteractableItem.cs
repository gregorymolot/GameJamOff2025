using UnityEngine;

public class DiscoverableItem : MonoBehaviour, IInteractable
{
    private bool returnable;

    [SerializeField]
    private Clues key;
    public Clues Key {get => key; }

    [Header("Text")]
    [SerializeField]
    private string itemName;
    public string ItemName { get => itemName; }

    [SerializeField]
    [TextArea(3, 10)]
    private string description;
    public string Description { get => description; }

    [SerializeField]
    string itemOwner;
    public string ItemOwner { get => itemOwner; }

    // [SerializeField]
    // ParticleSystem particles;


    public bool Returnable { get => returnable; set => returnable = value; }
    public bool Interactable { get => interactable; set => interactable = value; }

    [SerializeField]
    private bool interactable;

    public void Interact()
    {
        returnable = false;
        EventManager.Items.ShowItem?.Invoke(this);
    }

    public void Return()
    {
        // var main = particles.main;
        // var lifetime = main.startLifetime;
        // lifetime.constantMin = 0.5f;
        // lifetime.constantMax = 0.75f;
        // main.startLifetime = lifetime;
        // var speed = main.startSpeed;
        // speed.constantMin = 0.1f;
        // speed.constantMax = 0.2f;
        // main.startSpeed = speed;
        // var size = main.startSize;
        // size.constantMin = 0.1f;
        // size.constantMax = 0.2f;
        // main.startSize = size;
        // var emission = particles.emission;
        // emission.rateOverTime = 10f;

        EventManager.Unlocks.Unlock?.Invoke(key);
        ControllerManager.Instance.SwapCurrentController(ControllerType.Gameplay);
        EventManager.Items.Return?.Invoke();

    }
}
