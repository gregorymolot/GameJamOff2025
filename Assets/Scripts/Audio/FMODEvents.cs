using UnityEngine;
using FMODUnity;

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

    [field: Header("Music")]
    [field: SerializeField]
    public EventReference pauseMenuMusic { get; private set; }
    [field: SerializeField]
    public EventReference mainMenuMusic { get; private set; }
    [field: SerializeField]
    public EventReference recordMusic { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField]
    public EventReference wind { get; private set; }
    [field: SerializeField]
    public EventReference airVent { get; private set; }
    [field: SerializeField]
    public EventReference fire { get; private set; }
    

    [field: Header("Door SFX")]
    [field: SerializeField]
    public EventReference doorOpen { get; private set; }    
    [field: SerializeField]
    public EventReference doorClose { get; private set; }
        [field: SerializeField]
    public EventReference cabinetOpen { get; private set; }
        [field: SerializeField]
    public EventReference cabinetClose { get; private set; }
        [field: SerializeField]
    public EventReference drawerOpen { get; private set; }
        [field: SerializeField]
    public EventReference drawerClose { get; private set; }
    [field: SerializeField]
    public EventReference garageDoor { get; private set; }

    [field: Header("Player")]
    [field: SerializeField]
    public EventReference bump { get; private set; }
    [field: SerializeField]
    public EventReference footsteps { get; private set; }

    [field: Header("Bathroom")]
    [field: SerializeField]
    public EventReference bath { get; private set; }
    [field: SerializeField]
    public EventReference hairDryer { get; private set; }
    [field: SerializeField]
    public EventReference toiletFlush { get; private set; }
    [field: SerializeField]
    public EventReference waterDrip { get; private set; }
    [field: SerializeField]
    public EventReference alarmClock { get; private set; }
    [field: SerializeField]
    public EventReference toiletBowlOpen { get; private set; }

    [field: Header("MainRoom")]
    [field: SerializeField]
    public EventReference clock { get; private set; }
    [field: SerializeField]
    public EventReference newtonsCradle { get; private set; }

    [field: Header("Other SFX")]

    [field: SerializeField]
    public EventReference heartBeat { get; private set; }
    [field: SerializeField]
    public EventReference flickSwitch { get; private set; }
    [field: SerializeField]
    public EventReference paintingMove { get; private set; }
    [field: SerializeField]
    public EventReference windowBang { get; private set; }
    [field: SerializeField]
    public EventReference whoosh { get; private set; }
    [field: SerializeField]
    public EventReference discover { get; private set; }
    [field: SerializeField]
    public EventReference cutsceneWhoosh { get; private set; }

    [field: Header("Emitters")]
    [field: SerializeField]
    public EventReference flickerLight { get; private set; }
    [field: SerializeField]
    public EventReference microwave { get; private set; }
    [field: SerializeField]
    public EventReference roomba { get; private set; }
    [field: SerializeField]
    public EventReference waterHeater { get; private set; }

    [field: Header("Officers")]
    [field: SerializeField]
    public EventReference officer1Whistle { get; private set; }
    [field: SerializeField]
    public EventReference officer2Whistle { get; private set; }

    void Awake()
    {
        _instance = this;
    }
}
