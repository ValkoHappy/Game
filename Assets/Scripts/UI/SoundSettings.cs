using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    private const string MusicVolume = "MusicVolume";
    private const string EffectsVolume = "EffectsVolume";

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private AudioMixer _audioMixer;

    private float _defaultVolume = 0.74f;
    private float _minVolume = -80f;
    private float _maxVolume = 0f;

    private void Start()
    {
        if (PlayerPrefs.HasKey(MusicVolume) && PlayerPrefs.HasKey(EffectsVolume))
        {
            _musicSlider.value = PlayerPrefs.GetFloat(MusicVolume);
            _effectsSlider.value = PlayerPrefs.GetFloat(EffectsVolume);

            SetVolumeMusic(_musicSlider.value);
            SetVolumeEffects(_effectsSlider.value);
        }
        else
        {
            _musicSlider.value = _defaultVolume;
            _effectsSlider.value = _defaultVolume;

            SetVolumeMusic(_defaultVolume);
            SetVolumeEffects(_defaultVolume);

            PlayerPrefs.SetFloat(MusicVolume, _defaultVolume);
            PlayerPrefs.SetFloat(EffectsVolume, _defaultVolume);
            PlayerPrefs.Save();
        }
    }

    private void OnEnable()
    {
        _musicSlider.onValueChanged.AddListener(OnSetMusicSlider);
        _effectsSlider.onValueChanged.AddListener(OnSetEffectsSlider);
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(OnSetMusicSlider);
        _effectsSlider.onValueChanged.RemoveListener(OnSetEffectsSlider);
    }

    public void Mute()
    {
        _audioMixer.SetFloat(MusicVolume, _minVolume);
        _audioMixer.SetFloat(EffectsVolume, _minVolume);
    }

    public void Load()
    {
        _musicSlider.value = PlayerPrefs.GetFloat(MusicVolume);
        _effectsSlider.value = PlayerPrefs.GetFloat(EffectsVolume);

        SetVolumeMusic(_musicSlider.value);
        SetVolumeEffects(_effectsSlider.value);
    }

    public void OnSetMusicSlider(float volume)
    {
        _musicSlider.value = volume;
        PlayerPrefs.SetFloat(MusicVolume, volume);
        PlayerPrefs.Save();
        SetVolumeMusic(volume);
    }

    public void OnSetEffectsSlider(float volume)
    {
        _effectsSlider.value = volume;
        PlayerPrefs.SetFloat(EffectsVolume, volume);
        PlayerPrefs.Save();
        SetVolumeEffects(volume);
    }

    private void SetVolumeMusic(float volume)
    {
        _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(_minVolume, _maxVolume, volume));
    }

    private void SetVolumeEffects(float volume)
    {
        _audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(_minVolume, _maxVolume, volume));
    }
}
