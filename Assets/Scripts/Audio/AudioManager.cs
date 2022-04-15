using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
  
    [Header("ManageSound")]                     // För att kalla in ett ljud från vartsomhelst kör: FindObjectOfType<AudioManager>().playSound(rättstavatnamn);
    public global::Sound[] soundArray;

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
            soundClip.source = gameObject.AddComponent<AudioSource>();
            soundClip.source.clip = soundClip.audioClip;
            soundClip.source.volume = soundClip.volume;
            soundClip.source.pitch = soundClip.pitch;
            soundClip.source.loop = soundClip.loop;
            soundClip.source.mute = soundClip.mute;

        }
    }


    public void playSound(string soundToPlay)
    {
        getClip(soundToPlay);
       
        //Sound s = Array.Find(soundArray, sound => sound.name == soundToPlay);
        //s.source.Play();


    }

    public void stopSound(string soundToStop)
    {
        stopClip(soundToStop);
    }


    private AudioClip stopClip(string soundName)
    {

        foreach (Sound soundAudioClip in soundArray)
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


    private AudioClip getClip(string soundName)
    {

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
    // public AudioManager.Sound sound;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;
    public bool mute;
    public bool random;



    [HideInInspector]
    public AudioSource source;

}


