using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class GameAudioManager : MonoBehaviour
{
    List<EventInstance> eventInstances;
    private static GameAudioManager _instance;
    public static GameAudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No manager :(");
            }
            return _instance;
        }
    }

    private EventInstance ambience;
    private EventInstance music;
    private EventInstance fire;

    [field: Header("Volume")]
    [Range(0,1)]
    public float masterVolume = 1f;
        [Range(0,1)]
    public float musicVolume = 1f;
        [Range(0,1)]
    public float sfxVolume = 1f;
        [Range(0,1)]
    public float ambienceVolume = 1f;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private Bus ambienceBus;

    [SerializeField]
    private bool ambientNoise;

    void Awake()
    {
        _instance = this;
        eventInstances = new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    void Start()
    {
        InitializeMusic(FMODEvents.Instance.music);
        if (ambientNoise)
        {
            InitializeAmbience(FMODEvents.Instance.ambience);
            InitializeFire(FMODEvents.Instance.fire);
        }
    }

    void Update()
    {
        masterBus.setVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
        musicBus.setVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
        ambienceBus.setVolume(PlayerPrefs.GetFloat("AmbienceVolume", 1f));
        sfxBus.setVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));
    }

    void OnDisable()
    {

    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambience = CreateInstance(ambienceEventReference, Camera.main.gameObject);
        ambience.start();
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        music = CreateInstance(musicEventReference, Camera.main.gameObject);
        music.start();
    }

    private void InitializeFire(EventReference musicEventReference)
    {
        fire = CreateInstance(musicEventReference, Camera.main.gameObject);
        fire.start();
    }

    public void PlayOneShot(EventReference sound, Vector3 position)
    {
        RuntimeManager.PlayOneShot(sound, position);
    }

    public EventInstance CreateInstance(EventReference eventReference, GameObject gameObject)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    void CleanUp()
    {
        foreach (EventInstance instance in eventInstances)
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    void OnDestroy()
    {
        CleanUp();
    }
}
