using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float musicVolume = 1;
    [Range(0, 1)]
    public float sfxVolume = 1;
    [Range(0, 1)]

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;


    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance ambientEventInstance;
    private EventInstance musicEventInstance;
    public static AudioManager Instance { get; private set; }

    private void Start()
    {
        // InitializeAmbient(FMODEvents.Instance.ambient);
        InitializeMusic(FMODEvents.Instance.music);
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        // masterBus = RuntimeManager.GetBus("bus:/");
        // musicBus = RuntimeManager.GetBus("bus:/Music");
        // sfxBus = RuntimeManager.GetBus("bus:/SFX");

    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void InitializeAmbient(EventReference ambientEventReference)
    {
        ambientEventInstance = CreateEventInstance(ambientEventReference);
        ambientEventInstance.start();
    }
    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void SetMusicScene(float index)
    {
        musicEventInstance.setParameterByName("scenes", (float)index);
    }
    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    private void Cleanup()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }


    private void OnDestroy()
    {
        Cleanup();
    }


}
