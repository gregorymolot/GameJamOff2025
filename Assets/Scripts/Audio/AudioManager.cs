using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class GameAudioManager : MonoBehaviour
{
    //List<EventInstance> eventInstances;
    Dictionary<Room, List<EventInstance>> eventInstances;
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

    [SerializeField]
    bool mainMenu;

    private EventInstance ambience;
    private EventInstance pauseMusic;
    private EventInstance mainMenuMusic;

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

    void Awake()
    {
        _instance = this;
        eventInstances = new Dictionary<Room, List<EventInstance>>();
                
        var values = (Room[])System.Enum.GetValues(typeof(Room));
        // Iterate through the array and add each value to the list
        foreach (Room value in values)
        {
            eventInstances[value] = new List<EventInstance>();
        }

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    void Start()
    {
        if (mainMenu)
        {
            mainMenuMusic = CreateInstance(FMODEvents.Instance.mainMenuMusic, Camera.main.gameObject);
            mainMenuMusic.start();
        }
        else
        {
            InitializeAmbience(FMODEvents.Instance.wind);
            InitializePauseMusic(FMODEvents.Instance.pauseMenuMusic);   
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

    public void Pause(bool value)
    {
        ambienceBus.setPaused(value);
        sfxBus.setPaused(value);
        if (value)
        {
            pauseMusic.start();
        }
        else
        {
            pauseMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambience = CreateInstance(ambienceEventReference, Camera.main.gameObject);
        ambience.start();
    }

    private void InitializePauseMusic(EventReference ambienceEventReference)
    {
        pauseMusic = CreateInstance(ambienceEventReference, Camera.main.gameObject);
    }

    public void PlayOneShot(EventReference sound, Vector3 position)
    {
        RuntimeManager.PlayOneShot(sound, position);
    }

    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public void StopWind()
    {
        ambience.setParameterByName("Occlusion", 1);
    }

    public EventInstance CreateInstance(EventReference eventReference, GameObject gameObject, Room room, bool startsOccluded)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject);
        if (startsOccluded)
        {
            eventInstance.setParameterByName("Occlusion", 1);
        }
        eventInstances[room].Add(eventInstance);
        return eventInstance;
    }

    public EventInstance CreateInstance(EventReference eventReference, GameObject gameObject)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject);
        return eventInstance;
    }

    public EventInstance CreateInstance(EventReference eventReference, GameObject gameObject, Rigidbody value)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject, value);
        return eventInstance;

    }

    public void OccludeSounds(Room room)
    {
        foreach(EventInstance instance in eventInstances[room])
        {
            instance.setParameterByName("Occlusion", 1);
        }
    }

    public void NoLongerOccludeSounds(Room room)
    {
        foreach(EventInstance instance in eventInstances[room])
        {
            instance.setParameterByName("Occlusion", 0);
        }
    }

    public void EndInstance(EventInstance instance, Room room)
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstances[room].Remove(instance);
    }

    void CleanUp()
    {
        foreach(Room room in eventInstances.Keys)
        {
            foreach(EventInstance instance in eventInstances[room])
            {
                instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }
        ambience.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        pauseMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        mainMenuMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void OnDestroy()
    {
        CleanUp();
    }
}
