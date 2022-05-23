using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SettingsControls : MonoBehaviour
{

    [SerializeField] private AudioMixer MusicMixer;
    [SerializeField] private AudioMixer EffectsMixer;
    [SerializeField] private AudioMixer AmbienceMixer;

    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    [SerializeField] private Image _musicToggleImage;
    [SerializeField] private Slider _musicSlider;

    [SerializeField] private Image _effectsToggleImage;
    [SerializeField] private Slider _effectsSlider;

    [SerializeField] private Image _ambienceToggleImage;
    [SerializeField] private Slider _ambienceSlider;

    private float _lastEffectsVolume;
    private float _lastMusicVolume;
    private float _lastAmbienceVolume;

    private Settings _settings;

    private bool _musicIsMuted;
    private bool _effectsIsMuted;
    private bool _ambienceIsMuted;

    [SerializeField] Text _toggleImageXText;
    [SerializeField] Text _toggleText;


    private void Start()
    {
        SetUpControls();
    }

    private void SetUpControls()
    {
        _settings = Player.Instance.Settings;

        _effectsSlider.value = _settings.EffectsVolume;
        _lastEffectsVolume = _settings.EffectsVolume;

        _musicSlider.value = _settings.MusicVolume;
        _lastMusicVolume = _settings.MusicVolume;

        _ambienceSlider.value = _settings.AmbienceVolume;
        _lastAmbienceVolume = _settings.AmbienceVolume;

        if (_lastEffectsVolume < 0.0015f)
        {
            _effectsIsMuted = true;
        }
        else
        {
            _effectsIsMuted = false;
        }
        if (_lastMusicVolume < 0.0015f)
        {
            _musicIsMuted = true;
        }
        else
        {
            _musicIsMuted = false;
        }
        if (_lastAmbienceVolume < 0.0015f)
        {
            _ambienceIsMuted = true;
        }
        else
        {
            _ambienceIsMuted = false;
        }
        SetMusicLevel(_settings.MusicVolume);
        SetEffectsLevel(_settings.EffectsVolume);
        SetAmbienceLevel(_settings.EffectsVolume);
        SetNotificationControls();
    }

    public void SetMusicLevel(float sliderValue)
    {
        MusicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        _settings.MusicVolume = sliderValue;
        if (sliderValue > 0.0015f)
        {
            _musicToggleImage.sprite = _soundOnSprite;
            _musicIsMuted = false;
        }
        else
        {
            _musicToggleImage.sprite = _soundOffSprite;
            _musicSlider.value = 0.0001f;
            _musicIsMuted = true;
        }
    }

    public void SetEffectsLevel(float sliderValue)
    {
        EffectsMixer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
        _settings.EffectsVolume = sliderValue;
        if (sliderValue > 0.0015f)
        {
            _effectsToggleImage.sprite = _soundOnSprite;
            _effectsIsMuted = false;
        }
        else
        {
            _effectsToggleImage.sprite = _soundOffSprite;
            _effectsSlider.value = 0.0001f;
            _effectsIsMuted = true;
        }
    }

    public void SetAmbienceLevel(float sliderValue)
    {
        AmbienceMixer.SetFloat("AmbienceVolume", Mathf.Log10(sliderValue) * 20);
        _settings.AmbienceVolume = sliderValue;
        if (sliderValue > 0.0015f)
        {
            _ambienceToggleImage.sprite = _soundOnSprite;
            _ambienceIsMuted = false;
        }
        else
        {
            _ambienceToggleImage.sprite = _soundOffSprite;
            _ambienceSlider.value = 0.0001f;
            _ambienceIsMuted = true;
        }
    }

    //@TODO Clean and optimize the mute scripts
    public void MuteEffectsToggle()
    {
        if (_effectsIsMuted)
        {
            _effectsToggleImage.sprite = _soundOnSprite;
            _effectsSlider.value = _lastEffectsVolume;
            SetEffectsLevel(_lastEffectsVolume);
            _effectsIsMuted = false;
        }
        else
        {
            _lastEffectsVolume = _settings.EffectsVolume;
            SetEffectsLevel(0.0001f);
            _effectsIsMuted = true;
        }
    }

    public void MuteMusicToggle()
    {
        if (_musicIsMuted)
        {
            _musicToggleImage.sprite = _soundOnSprite;
            _musicSlider.value = _lastMusicVolume;
            SetMusicLevel(_lastMusicVolume);
            _musicIsMuted = false;
        }
        else
        {
            _lastMusicVolume = _settings.MusicVolume;
            SetMusicLevel(0.0001f);
            _musicIsMuted = true;
        }
    }

    public void MuteAmbienceToggle()
    {
        if (_ambienceIsMuted)
        {
            _ambienceToggleImage.sprite = _soundOnSprite;
            _ambienceSlider.value = _lastAmbienceVolume;
            SetAmbienceLevel(_lastMusicVolume);
            _ambienceIsMuted = false;
        }
        else
        {
            _lastAmbienceVolume = _settings.AmbienceVolume;
            SetAmbienceLevel(0.0001f);
            _ambienceIsMuted = true;
        }
    }

    public void ToggleNotifications()
    {
        _settings.NotificationsToggle = !_settings.NotificationsToggle;

        SetNotificationControls();
    }

    private void SetNotificationControls()
    {
        if (_settings.NotificationsToggle)
        {
            _toggleImageXText.text = "X";
            _toggleText.text = "Notifications ON";
        }
        else
        {
            _toggleImageXText.text = "";
            _toggleText.text = "Notifications OFF";
        }
    }

}
