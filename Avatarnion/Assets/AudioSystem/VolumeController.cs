using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER, MUSIC, SFX
    }

    [Header("Type")]
    [SerializeField] VolumeType volumeType;
    private Slider volumeSlider;


    void Awake()
    {
        volumeSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.Instance.masterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.Instance.musicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.Instance.sfxVolume;
                break;
            default:
                Debug.LogWarning("VolumeType not supporter" + volumeType);
                break;

        }
    }

    public void OnSliderValueChange()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.Instance.masterVolume = volumeSlider.value;
                break;
            case VolumeType.MUSIC:
                AudioManager.Instance.musicVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.Instance.sfxVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("VolumeType not supporter" + volumeType);
                break;

        }
    }
}
