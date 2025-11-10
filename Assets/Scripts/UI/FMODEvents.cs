using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class FMODEvents : MonoBehaviour
{
    private static FMODEvents _instance;
    public static FMODEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No manager :()");
            }
            return _instance;
        }
    }

    [field: Header("Pickup SFX")]
    [field: SerializeField]
    public EventReference coinCollected { get; private set; }
        [field: SerializeField]
    public EventReference armorPickup { get; private set; }
        [field: SerializeField]
    public EventReference bombPickup { get; private set; }
        [field: SerializeField]
    public EventReference healthPickup { get; private set; }
        [field: SerializeField]
    public EventReference speedPickup { get; private set; }
        [field: SerializeField]
    public EventReference attackPickup { get; private set; }
        [field: SerializeField]
    public EventReference keyPickup { get; private set; }

    [Header("Use Item SFX")]
    [field: SerializeField]
    public EventReference armorUse { get; private set; }
        [field: SerializeField]
    public EventReference bombUse { get; private set; }
        [field: SerializeField]
    public EventReference healthUse { get; private set; }
        [field: SerializeField]
    public EventReference speedUse { get; private set; }
        [field: SerializeField]
    public EventReference attackUse { get; private set; }
        [field: SerializeField]
    public EventReference keyUse { get; private set; }

    [Header("Block SFX")]
    [field: SerializeField]
    public EventReference blockPlace { get; private set; }

    [Header("Skeleton SFX")]
    [field: SerializeField]
    public EventReference skeletonFootsteps { get; private set; }
    [field: SerializeField]
    public EventReference skeletonHit{ get; private set; }


    [field: Header("Player SFX")]
    [field: SerializeField]
    public EventReference playerFootsteps { get; private set; }
    [field: SerializeField]
    public EventReference playerHit { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField]
    public EventReference ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField]
    public EventReference music { get; private set; }

    [field: Header("Fire")]
    [field: SerializeField]
    public EventReference fire { get; private set; }

    [field: Header("Shop SFX")]
    [field: SerializeField]
    public EventReference buy { get; private set; }
    [field: SerializeField]
    public EventReference cantBuy { get; private set; }

    void Awake()
    {
        _instance = this;
    }
}
