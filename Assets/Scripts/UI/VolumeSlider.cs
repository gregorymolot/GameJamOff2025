using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        Master,
        Music,
        Ambience,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = this.GetComponent<Slider>();
    }

    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
                break;
            case VolumeType.Music:
                volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
                break;
            case VolumeType.Ambience:
                volumeSlider.value = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
                break;
            case VolumeType.SFX:
                volumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
                break;
            default:
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                PlayerPrefs.SetFloat("MasterVolume", volumeSlider.value);
                break;
            case VolumeType.Music:
                PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
                break;
            case VolumeType.Ambience:
                PlayerPrefs.SetFloat("AmbienceVolume", volumeSlider.value);
                break;
            case VolumeType.SFX:
                PlayerPrefs.SetFloat("SFXVolume", volumeSlider.value);
                break;
            default:
                break;
        }
    }
}
