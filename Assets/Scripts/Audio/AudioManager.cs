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

    private EventInstance ambience;

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
        eventInstances = new Dictionary<Room, List<EventInstance>>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    void Start()
    {
        InitializeAmbience(FMODEvents.Instance.wind);
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

    public void PlayOneShot(EventReference sound, Vector3 position)
    {
        RuntimeManager.PlayOneShot(sound, position);
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
    }

    void OnDestroy()
    {
        CleanUp();
    }
}
