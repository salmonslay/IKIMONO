using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("AudioMixers")]
    [SerializeField] AudioMixerGroup effectMixer;
    [SerializeField] AudioMixerGroup musicMixer;
    [SerializeField] AudioMixerGroup ambianceMixer;

    [Header("ManageSound")]          // För att kalla in ljud skriv : AurioManager.Instance.playSound("LjudetsKey", "LjudetsNamn");
                                     // För att ramdomizea bland alla ljuden i listan skriv: AudioManager.Instance.randomizeSound("LjudetsKey");

    public List<Sound> SoundListPlaying;
    [Header("Pet")]
    public SoundDictionary<String, List<Sound>> PetHappy;
    public SoundDictionary<String, List<Sound>> PetEating;
    public SoundDictionary<String, List<Sound>> PetSleeping;
    public SoundDictionary<String, List<Sound>> PetSad;
    public SoundDictionary<String, List<Sound>> Scratches;
    public SoundDictionary<String, List<Sound>> Bubbles;
    [Header("Shop")]
    public SoundDictionary<String, List<Sound>> ShopSound;
    public SoundDictionary<String, List<Sound>> Swoosh;
    [Header("UI")]
    public SoundDictionary<String, List<Sound>> ButtonClick;
    [Header("MiniGame")]
    public SoundDictionary<String, List<Sound>> GameOver;
    public SoundDictionary<String, List<Sound>> Jump;
    public SoundDictionary<String, List<Sound>> Bounce;
    public SoundDictionary<String, List<Sound>> PlatformBreak;
    public SoundDictionary<String, List<Sound>> PickupCoin;
    [Header("Music")]
    public SoundDictionary<String, List<Sound>> MainMusic;
    public SoundDictionary<String, List<Sound>> SleepMusic;
    public SoundDictionary<String, List<Sound>> MinigameMusic;
    [Header("Ambiance")]
    public SoundDictionary<String, List<Sound>> NightAmb;
    public SoundDictionary<String, List<Sound>> DayAmb;
    public SoundDictionary<String, List<Sound>> GameAmb;


    [Header("AudioSources")]
    public AudioSource effectsource;
    public AudioSource musicSource;
    public AudioSource ambianceSource;


    public static AudioManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        effectsource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        ambianceSource = gameObject.AddComponent<AudioSource>();

        effectsource.outputAudioMixerGroup = effectMixer;
        musicSource.outputAudioMixerGroup = musicMixer;
        ambianceSource.outputAudioMixerGroup = ambianceMixer;

    }

    public void RandomizeSound(string list)
    {


        tryListKey(list);
        int clipIndex = Random.Range(0, SoundListPlaying.Count);
        SoundListPlaying[clipIndex].source.PlayOneShot(SoundListPlaying[clipIndex].audioClip);

    }

    public void PlaySound(string getList, string soundToPlay)
    {

        getClip(getList, soundToPlay);

    }

    public void StopSound(string getList, string soundToStop)
    {
        stopClip(getList, soundToStop);
    }


    private AudioClip stopClip(string soundList, string soundName)
    {
        tryListKey(soundList);
        foreach (Sound soundAudioClip in SoundListPlaying)
        {
            if (soundAudioClip.name == soundName)
            {

                soundAudioClip.source.Stop();
                return soundAudioClip.audioClip;

            }
        }
        Debug.Log("Sound" + soundName + "Not Found!");
        return null;


    }

    public void setSourceInfo(List<Sound> listInfo)
    {
        foreach (Sound soundClip in listInfo)
        {
            if (listInfo == MinigameMusic.sound || listInfo == MainMusic.sound || listInfo == SleepMusic.sound)
            {
                soundClip.source = musicSource;

            }
            else if (listInfo == DayAmb.sound || listInfo == NightAmb.sound || listInfo == GameAmb.sound)
            {
                soundClip.source = ambianceSource;
            }
            else
            {
                soundClip.source = effectsource;
            }


            soundClip.source.clip = soundClip.audioClip;
            soundClip.source.volume = soundClip.volume;
            soundClip.source.pitch = soundClip.pitch;
            soundClip.source.loop = soundClip.loop;
            soundClip.source.mute = soundClip.mute;

        }
    }


    public void tryListKey(String a)
    {

        if (PetHappy.containsKey(a))
        {
            setSourceInfo(PetHappy.sound);
            SoundListPlaying = PetHappy.returnSound();
        }

        if (PetEating.containsKey(a))
        {
            setSourceInfo(PetEating.sound);
            SoundListPlaying = PetEating.returnSound();

        }

        if (PetSleeping.containsKey(a))
        {
            setSourceInfo(PetSleeping.sound);
            SoundListPlaying = PetSleeping.returnSound();

        }

        if (PetSad.containsKey(a))
        {
            setSourceInfo(PetSad.sound);
            SoundListPlaying = PetSad.returnSound();

        }

        if (Scratches.containsKey(a))
        {
            setSourceInfo(Scratches.sound);
            SoundListPlaying = Scratches.returnSound();

        }

        if (Bubbles.containsKey(a))
        {
            setSourceInfo(Bubbles.sound);
            SoundListPlaying = Bubbles.returnSound();

        }

        if (ShopSound.containsKey(a))
        {
            setSourceInfo(ShopSound.sound);
            SoundListPlaying = ShopSound.returnSound();

        }

        if (Swoosh.containsKey(a))
        {
            setSourceInfo(Swoosh.sound);
            SoundListPlaying = Swoosh.returnSound();

        }

        if (ButtonClick.containsKey(a))
        {
            setSourceInfo(ButtonClick.sound);
            SoundListPlaying = ButtonClick.returnSound();

        }

        if (GameOver.containsKey(a))
        {
            setSourceInfo(GameOver.sound);
            SoundListPlaying = GameOver.returnSound();

        }

        if (Jump.containsKey(a))
        {
            setSourceInfo(Jump.sound);
            SoundListPlaying = Jump.returnSound();

        }

        if (Bounce.containsKey(a))
        {
            setSourceInfo(Bounce.sound);
            SoundListPlaying = Bounce.returnSound();

        }

        if (PlatformBreak.containsKey(a))
        {
            setSourceInfo(PlatformBreak.sound);
            SoundListPlaying = PlatformBreak.returnSound();

        }

        if (PickupCoin.containsKey(a))
        {
            setSourceInfo(PickupCoin.sound);
            SoundListPlaying = PickupCoin.returnSound();

        }

        if (MainMusic.containsKey(a))
        {
            setSourceInfo(MainMusic.sound);
            SoundListPlaying = MainMusic.returnSound();

        }

        if (SleepMusic.containsKey(a))
        {
            setSourceInfo(SleepMusic.sound);
            SoundListPlaying = SleepMusic.returnSound();

        }

        if (MinigameMusic.containsKey(a))
        {
            setSourceInfo(MinigameMusic.sound);
            SoundListPlaying = MinigameMusic.returnSound();

        }

        if (DayAmb.containsKey(a))
        {

            setSourceInfo(DayAmb.sound);
            SoundListPlaying = DayAmb.returnSound();
        }

        if (NightAmb.containsKey(a))
        {

            setSourceInfo(NightAmb.sound);
            SoundListPlaying = NightAmb.returnSound();

        }

        if (GameAmb.containsKey(a))
        {

            setSourceInfo(GameAmb.sound);
            SoundListPlaying = GameAmb.returnSound();

        }

        else { Debug.Log(a + " getList method failure"); 
        }

    }

    private AudioClip getClip(string soundList, string soundName)
    {
        tryListKey(soundList);
        foreach (Sound soundAudioClip in SoundListPlaying)
        {
            if (soundAudioClip.name == soundName)
            {

                if (soundAudioClip.randomPitch == true)
                {
                    soundAudioClip.source.pitch = Random.Range(0.8f, 1.4f);
                    soundAudioClip.source.PlayOneShot(soundAudioClip.audioClip);
                    return soundAudioClip.audioClip;
                }
                else
                {
                    soundAudioClip.source.Play();

                }

                return soundAudioClip.audioClip;

            }
        }
        Debug.Log("Sound" + soundName + "Not Found!");
        return null;

    }

}








[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;
    public bool mute;
    public bool randomPitch;



    //[HideInInspector]
    public AudioSource source;


}


[System.Serializable]
public class SoundDictionary<String, List>
{

    public List<Sound> sound;
    public String key;

    public SoundDictionary(String key, List<Sound> sound)
    {

        this.sound = sound;
        this.key = key;
    }

    public bool containsKey(String a)
    {
        if (a.Equals(key))
        {
            return true;
        }
        return false;
    }

    public List<Sound> returnSound()
    {
        return sound;
    }
}




