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
        }
        else
        {
            musicToggleImage.sprite = soundOffSprite;
            musicSlider.value = 0.0001f;
        }
    }

    public void SetEffectsLevel(float sliderValue)
    {
        EffectsMixer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
        settings.EffectsVolume = sliderValue;
        if (sliderValue > 0.0015f)
        {
            effectsToggleImage.sprite = soundOnSprite;
        }
        else
        {
            effectsToggleImage.sprite = soundOffSprite;
            effectsSlider.value = 0.0001f;
        }
    }

    //@TODO Clean and optimize the mute scripts
    public void MuteEffectsToggle()
    {
        if (settings.EffectsVolume < 0.0015f)
        {
            effectsToggleImage.sprite = soundOnSprite;
            effectsSlider.value = lastEffectsVolume;
            SetEffectsLevel(lastEffectsVolume);
        }
        else
        {
            lastEffectsVolume = settings.EffectsVolume;
            SetEffectsLevel(0.0001f);
        }
    }

    public void MuteMusicToggle()
    {
        if (settings.MusicVolume < 0.0015f)
        {
            musicToggleImage.sprite = soundOnSprite;
            musicSlider.value = lastEffectsVolume;
            SetMusicLevel(lastMusicVolume);
        }
        else
        {
            lastMusicVolume = settings.MusicVolume;
            SetMusicLevel(0.0001f);
        }
    }

}
