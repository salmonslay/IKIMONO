using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{

    [Header("ManageSound")]                     // För att kalla in ljud skriv : FindObjectOfType<AudioManager>().playSound("LjudetsKey", "LjudetsNamn");
    public List<Sound> soundArray;              // För att randomizea en ljudlista skriv : FindObjectOfType<AudioManager>().randomizeSound("LjudetsKey");
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
    public SoundDictionary<String, List<Sound>> MinigameMusic;
    [Header("Ambiance")]
    public SoundDictionary<String, List<Sound>> NightAmb;
    public SoundDictionary<String, List<Sound>> DayAmb;

   

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


        foreach (Sound soundClip in soundArray)
        {
            Debug.Log(soundClip.source);
            soundClip.source = gameObject.AddComponent<AudioSource>();
            soundClip.source.clip = soundClip.audioClip;
            soundClip.source.volume = soundClip.volume;
            soundClip.source.pitch = soundClip.pitch;
            soundClip.source.loop = soundClip.loop;
            soundClip.source.mute = soundClip.mute;

        }
    }


    public void randomizeSound(string list)
    {
       

        getList(list);
        
        int clipIndex = Random.Range(0, soundArray.Count);
        soundArray[clipIndex].source.Play();

    }



    public void playSound(string getList, string soundToPlay)
    {


        getClip(getList, soundToPlay);

      
        //Sound s = Array.Find(soundArray, sound => sound.name == soundToPlay);
        //s.source.Play();


    }


    public void getList(String a)
    {
        if (MainMusic.containsKey(a))
        {

            foreach (Sound soundClip in MainMusic.sound)
            {

                soundClip.source = gameObject.AddComponent<AudioSource>();
                soundClip.source.clip = soundClip.audioClip;
                soundClip.source.volume = soundClip.volume;
                soundClip.source.pitch = soundClip.pitch;
                soundClip.source.loop = soundClip.loop;
                soundClip.source.mute = soundClip.mute;
                Debug.Log(soundClip.source);
            }


            soundArray = MainMusic.returnSound();



        }
        if (PetHappy.containsKey(a))
        {

            foreach (Sound soundClip in PetHappy.sound)
            {

                soundClip.source = gameObject.AddComponent<AudioSource>();
                soundClip.source.clip = soundClip.audioClip;
                soundClip.source.volume = soundClip.volume;
                soundClip.source.pitch = soundClip.pitch;
                soundClip.source.loop = soundClip.loop;
                soundClip.source.mute = soundClip.mute;
                Debug.Log(soundClip.source);
            }

           
            soundArray = PetHappy.returnSound();
            Debug.Log(PetHappy.sound[0].source);
            Debug.Log(PetHappy.sound[0].volume);


        }
        if (PetEating.containsKey(a))
        {

            foreach (Sound soundClip in PetEating.sound)
            {

                soundClip.source = gameObject.AddComponent<AudioSource>();
                soundClip.source.clip = soundClip.audioClip;
                soundClip.source.volume = soundClip.volume;
                soundClip.source.pitch = soundClip.pitch;
                soundClip.source.loop = soundClip.loop;
                soundClip.source.mute = soundClip.mute;
                Debug.Log(soundClip.source);
            }


            soundArray = PetEating.returnSound();
          


        }
        else { Debug.Log("getList method failure"); }

    }

    private AudioClip getClip(string soundList, string soundName)
    {
        getList(soundList);
        foreach (Sound soundAudioClip in soundArray)
        {
            if (soundAudioClip.name == soundName)
            {

                if (soundAudioClip.random == true)
                {
                    soundAudioClip.source.pitch = Random.Range(0.8f, 1.4f);
                    soundAudioClip.source.Play();
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
    public bool random;



    //[HideInInspector]
   public AudioSource source;

}

[System.Serializable]
public class SoundDictionary<String, List>
{


    public List<Sound> sound;
    public String key;
    

    public SoundDictionary(String nyckel, List<Sound> ljud)
    {

        this.sound = ljud;
        this.key = nyckel;
    }

    public bool containsKey(String a)
    {
        if (a.Equals(key)) {

            return true;
        }
        return false;
    }

    public List<Sound> returnSound()
    {
       
        Debug.Log(sound[0].name);
        Debug.Log(sound[0].source);  
        return sound;
        
    }
}




