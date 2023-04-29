using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private AudioMixer _audioMixer;

    private float _defaultVolume = -20f;

    private const string MusicVolume = "MusicVolume";
    private const string EffectsVolume = "EffectsVolume";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(MusicVolume) && PlayerPrefs.HasKey(EffectsVolume))
        {
            _musicSlider.value = PlayerPrefs.GetFloat(MusicVolume);
            _effectsSlider.value = PlayerPrefs.GetFloat(EffectsVolume);

            _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(-80, 0, _musicSlider.value));
            _audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(-80, 0, _effectsSlider.value));
        }
        else
        {
            _musicSlider.value = _defaultVolume;
            _effectsSlider.value = _defaultVolume;

            _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(-80, 0, _defaultVolume));
            _audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(-80, 0, _defaultVolume));

            PlayerPrefs.SetFloat(MusicVolume, _defaultVolume);
            PlayerPrefs.SetFloat(EffectsVolume, _defaultVolume);
            PlayerPrefs.Save();
        }
    }


    private void OnEnable()
    {
        _musicSlider.onValueChanged.AddListener(SetVolumeMusic);
        _effectsSlider.onValueChanged.AddListener(SetVolumeEffects);
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(SetVolumeMusic);
        _effectsSlider.onValueChanged.RemoveListener(SetVolumeEffects);
    }

    public void SetMusicSlider(float volume)
    {
        _musicSlider.value = volume;
    }

    public void SetEffectsSlider(float volume)
    {
        _effectsSlider.value = volume;
    }

    private void SetVolumeMusic(float volume)
    {
        _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(-80, 0, volume));
        PlayerPrefs.SetFloat(MusicVolume, volume);
        PlayerPrefs.Save();
    }

    private void SetVolumeEffects(float volume)
    {
        _audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(-80, 0, volume));
        PlayerPrefs.SetFloat(EffectsVolume, volume);
        PlayerPrefs.Save();
    }
}
