using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{

    [Header("ManageSound")]                     // F�r att kalla in ett ljud fr�n vartsomhelst k�r: FindObjectOfType<AudioManager>().playSound(r�ttstavatnamn);
    public List<Sound> soundArray;
    public SoundDictionary<String, List<Sound>> PetHappy;
    public SoundDictionary<String, List<Sound>> CoinSound;
    //public SoundDictionary<String, List<Sound>> PetSad;




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


 
    public void randomizeSound()
    {

    }


    public void getList(String a)
    {


        if (PetHappy.containsKey(a))
        {

            foreach (Sound soundClip in PetHappy.ljud)
            {
                
                soundClip.source = gameObject.AddComponent<AudioSource>();
                soundClip.source.clip = soundClip.audioClip;
                soundClip.source.volume = soundClip.volume;
                soundClip.source.pitch = soundClip.pitch;
                soundClip.source.loop = soundClip.loop;
                soundClip.source.mute = soundClip.mute;
                Debug.Log(soundClip.source);
            }

            Debug.Log("N�r listan och nyckeln fungerar");
            
            soundArray = PetHappy.returnSound();
            Debug.Log(PetHappy.ljud[0].source);
            Debug.Log(PetHappy.ljud[0].volume);


        }
        else { Debug.Log("getList metoden �r kaputt"); }
       

    }


    public void playSound(string getList, string soundToPlay)
    {
      

        getClip(getList, soundToPlay);


        //Sound s = Array.Find(soundArray, sound => sound.name == soundToPlay);
        //s.source.Play();


    }

     
    private AudioClip getClip(string soundList,string soundName)
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

                    //GameObject go = new GameObject();
                    //AudioSource source = go.AddComponent<AudioSource>();
                    //source.clip = soundContainer.audioClip;
                    //source.Play();
                    //print("hej");
                    //Destroy(go, soundContainer.audioClip.length);

                }



                return soundAudioClip.audioClip;            

            }
        }
        Debug.Log("Sound" + soundName + "Not Found!");              
        return null;


    }



    //public void stopSound(string soundToStop)
    //{
    //    stopClip(soundToStop);
    //}


    //private AudioClip stopClip(string soundName)
    //{

    //    foreach (Sound soundAudioClip in soundArray)
    //    {
    //        if (soundAudioClip.name == soundName)
    //        {

    //            soundAudioClip.source.Stop();
    //            return soundAudioClip.audioClip;

    //        }
    //    }
    //    Debug.Log("Sound" + soundName + "Not Found!");
    //    return null;


    //}
    //public static AudioSource PlayAudio(AudioClip[] clips)
    //{
    //    if (clips.Length == 0)
    //        return null;
    //    return PlayAudio(clips[Random.Range(0, clips.Length)]);
    //}
    
    //public static AudioSource PlayAudio(AudioClip clip)
    //{
    //    if (clip == null) return null;
    //    GameObject obj = new GameObject();
    //    AudioSource source = obj.AddComponent<AudioSource>();

    //    source.clip = clip;
    //    source.Play();

    //    Destroy(obj, clip.length + 0.5f);
    //    return source;
    //}

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


    public List<Sound> ljud;
    public String nyckel;
    

    public SoundDictionary(String nyckel, List<Sound> ljud)
    {

        this.ljud = ljud;
        this.nyckel = nyckel;
    }

    public bool containsKey(String a)
    {
        if (a.Equals(nyckel)) {

            return true;
        }
        return false;
    }

    public List<Sound> returnSound()
    {
        //foreach (Sound soundClip in ljud)
        //{
        //    Debug.Log(soundClip.source);
        //   // soundClip.source = gameObject.AddComponent<AudioSource>();
        //    soundClip.source.clip = soundClip.audioClip;
        //    soundClip.source.volume = soundClip.volume;
        //    soundClip.source.pitch = soundClip.pitch;
        //    soundClip.source.loop = soundClip.loop;
        //    soundClip.source.mute = soundClip.mute;

        //}
        Debug.Log(ljud[0].name);
        Debug.Log(ljud[0].source);  // source är null, var tar den vägen?!?!
        return ljud;
        
    }
}




