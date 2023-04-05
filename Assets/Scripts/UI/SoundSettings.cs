using System.Collections;
using System.Collections.Generic;
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
        if (PlayerPrefs.HasKey(MusicVolume))
        {
            _musicSlider.value = PlayerPrefs.GetFloat(MusicVolume);
            _effectsSlider.value = PlayerPrefs.GetFloat(EffectsVolume);
            _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(-80, 0, _musicSlider.value));
            _audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(-80, 0, _effectsSlider.value));
            //_musicToggle.isOn = _musicSlider.value != -80;
            //_effectsToggle.isOn = _effectsSlider.value != -80;
        }
        else
        {
            _musicSlider.value = _defaultVolume;
            _effectsSlider.value = _defaultVolume;
            _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(-80, 0, _defaultVolume));
            _audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(-80, 0, _defaultVolume));
            //_musicToggle.isOn = false;
            //_effectsToggle.isOn = false;
        }
    }

    private void OnEnable()
    {
        _musicSlider.onValueChanged.AddListener(SetVolumeMusic);
        //_musicToggle.onValueChanged.AddListener(MuteMusic);
        _effectsSlider.onValueChanged.AddListener(SetVolumeEffects);
        //_effectsToggle.onValueChanged.AddListener(MuteEffect);
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(SetVolumeMusic);
        //_musicToggle.onValueChanged.RemoveListener(MuteMusic);
        _effectsSlider.onValueChanged.RemoveListener(SetVolumeEffects);
        //_effectsToggle.onValueChanged.RemoveListener(MuteEffect);
    }

    //public void MuteMusic(bool isntMute)
    //{
    //    if (isntMute == false)
    //    {
    //        if (PlayerPrefs.HasKey(MusicVolume))
    //        {
    //            _musicSlider.value = PlayerPrefs.GetFloat(MusicVolume);
    //        }
    //        else
    //        {
    //            _musicSlider.value = _defaultVolume;
    //        }
    //    }
    //    else
    //    {
    //        _musicSlider.value =  -80;
    //    }
    //}

    //public void MuteEffect(bool isntMute)
    //{
    //    if (isntMute == false)
    //    {
    //        if (PlayerPrefs.HasKey(EffectsVolume))
    //        {
    //            _effectsSlider.value = PlayerPrefs.GetFloat(EffectsVolume);
    //        }
    //        else
    //        {
    //            _effectsSlider.value = _defaultVolume;
    //        }
    //    }
    //    else
    //    {
    //        _effectsSlider.value = -80;
    //    }
    //}


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

        //if (_musicSlider.value == -80)
        //    _musicToggle.isOn = true;

        //if (_musicSlider.value > -80)
        //    _musicToggle.isOn = false;
    }

    private void SetVolumeEffects(float volume)
    {
        _audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(-80, 0, volume));
        PlayerPrefs.SetFloat(EffectsVolume, volume);
        PlayerPrefs.Save();

        //if (_effectsSlider.value == -80)
        //    _effectsToggle.isOn = true;

        //if (_effectsSlider.value > -80)
        //    _effectsToggle.isOn = false;
    }
}
