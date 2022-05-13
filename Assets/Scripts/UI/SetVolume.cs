using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class SetVolume : MonoBehaviour
{

    [SerializeField] private AudioMixer MusicMixer;
    [SerializeField] private AudioMixer EffectsMixer;

    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    [SerializeField] private Image musicToggleImage;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private Image effectsToggleImage;
    [SerializeField] private Slider effectsSlider;

    private float lastEffectsVolume;
    private float lastMusicVolume;

    private Settings settings;

    private bool _musicIsMuted;
    private bool _effectsIsMuted;

    private void Start()
    {
        SetUpSoundControls();
    }

    private void SetUpSoundControls()
    {
        settings = Player.Instance.Settings;
        effectsSlider.value = settings.EffectsVolume;
        musicSlider.value = settings.MusicVolume;
        lastEffectsVolume = settings.EffectsVolume;
        if (lastEffectsVolume < 0.0015f)
        {
            _effectsIsMuted = true;
        }
        else
        {
            _effectsIsMuted = false;
        }
        if (lastMusicVolume < 0.0015f)
        {
            _musicIsMuted = true;
        }
        else
        {
            _musicIsMuted = false;
        }
        lastMusicVolume = settings.MusicVolume;
        SetMusicLevel(settings.MusicVolume);
        SetEffectsLevel(settings.EffectsVolume);
    }

    public void SetMusicLevel(float sliderValue)
    {
        MusicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        settings.MusicVolume = sliderValue;
        if (sliderValue > 0.0015f)
        {
            musicToggleImage.sprite = soundOnSprite;
            _musicIsMuted = false;
        }
        else
        {
            musicToggleImage.sprite = soundOffSprite;
            musicSlider.value = 0.0001f;
            _musicIsMuted = true;
        }
    }

    public void SetEffectsLevel(float sliderValue)
    {
        EffectsMixer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
        settings.EffectsVolume = sliderValue;
        if (sliderValue > 0.0015f)
        {
            effectsToggleImage.sprite = soundOnSprite;
            _effectsIsMuted = false;
        }
        else
        {
            effectsToggleImage.sprite = soundOffSprite;
            effectsSlider.value = 0.0001f;
            _effectsIsMuted = true;
        }
    }

    //@TODO Clean and optimize the mute scripts
    public void MuteEffectsToggle()
    {
        if (_effectsIsMuted)
        {
            effectsToggleImage.sprite = soundOnSprite;
            effectsSlider.value = lastEffectsVolume;
            SetEffectsLevel(lastEffectsVolume);
            _effectsIsMuted = false;
        }
        else
        {
            lastEffectsVolume = settings.EffectsVolume;
            SetEffectsLevel(0.0001f);
            _effectsIsMuted = true;
        }
    }

    public void MuteMusicToggle()
    {
        if (_musicIsMuted)
        {
            musicToggleImage.sprite = soundOnSprite;
            musicSlider.value = lastMusicVolume;
            SetMusicLevel(lastMusicVolume);
            _musicIsMuted = false;
        }
        else
        {
            lastMusicVolume = settings.MusicVolume;
            SetMusicLevel(0.0001f);
            _musicIsMuted = true;
        }
    }

}
