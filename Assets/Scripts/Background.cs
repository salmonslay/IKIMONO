using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IKIMONO.Pet;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite _day;
    [SerializeField] private Sprite _night;

    [SerializeField] UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    private SoundDictionary<string, List<Sound>> test;
    
    private const int SunUpHour = 6;
    private const int SunDownHour = 20;
    private static OverrideMode _overrideMode = OverrideMode.None;
    
    /// <summary>
    /// Checks if it currently is day or not, with overrides in mind.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static bool IsDay { get
    {
        return _overrideMode switch
        {
            OverrideMode.None => DateTime.Now.Hour >= SunUpHour && DateTime.Now.Hour <= SunDownHour,
            OverrideMode.Day => true,
            OverrideMode.Night => false,
            _ => throw new ArgumentOutOfRangeException()
        };
    }}
    
    private bool isAllowed = true;
    private bool isMusicAllowed = true;
    private string stringNameOfCurrentAmbSound;

    private float minIntensity = 1f;
    private float maxIntensity = 5f;

    private Pet _pet;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        //What time is it now and what mode am I in


        //Get current hour
        System.DateTime now = System.DateTime.Now;

        //Debug.Log($"timmen är nu: {now.Hour}");

        if (IsDay)
        {
            //Daymode
            //Debug.Log("nu är jag i dag ifsatsen");
            GetComponent<Image>().sprite = _day;
            globalLight.intensity = Mathf.PingPong(now.Hour * minIntensity, maxIntensity); // Ökar mellan tiden på dygnet , värdena behövs ställas


        }
        else
        {
            //Nightmode
            //Debug.Log("nu är jag i natt ifsatsen");
            GetComponent<Image>().sprite = _night;
            globalLight.intensity = Mathf.PingPong(now.Hour * maxIntensity, minIntensity);
            


        }


        Debug.Log("Time of day: " + IsDay);   // Städa sen
        Debug.Log("IsAllowed: " + isAllowed);


        if (isAllowed)
        {
            setPlayAmbianceSounds();

        }

        if (isMusicAllowed)
        {
           
            Debug.Log("isMusicAllowed SPELA");
            AudioManager.Instance.PlaySound("Music", "One");

            
            //AudioManager.Instance.PlaySound("SleepMusic", "One");
            isMusicAllowed = false;
            
           
                
            
         
            
            //playMusic();
        }

        
        if(Input.GetKeyDown(KeyCode.D))
            _overrideMode = OverrideMode.Day;
        else if (Input.GetKeyDown(KeyCode.N))
            _overrideMode = OverrideMode.Night;
        else if (Input.GetKeyDown(KeyCode.R))
            _overrideMode = OverrideMode.None;
    }

    private void playMusic()
    {
        if (_pet.Energy.IsSleeping)  // Fungerar inte
        {
            Debug.Log("SOVLJUD BÖR SPELAS");
            AudioManager.Instance.PlaySound("MinigameMusic", "One");
        }else if (!_pet.Energy.IsSleeping)
        {
            Debug.Log("SOVER INTE SÅ LJUDET BORDE BYTAS TILL VANLIGT LJUD");
        }
       
        isMusicAllowed = false;
    }

    private void setPlayAmbianceSounds()
    {
      
        if (IsDay)
        {
            
            stringNameOfCurrentAmbSound = "DayAmb";
            Debug.Log(stringNameOfCurrentAmbSound);
            AudioManager.Instance.PlaySound(stringNameOfCurrentAmbSound, "One");
        }
        else
        {
            stringNameOfCurrentAmbSound = "NightAmb";
            AudioManager.Instance.PlaySound(stringNameOfCurrentAmbSound, "One");
        }
        
       
        isAllowed = false;
        
        
    }



    /// <summary>
    /// Whether or not the current time should be overridden, and if so, what mode it should be in.
    /// </summary>
    private enum OverrideMode
    {
        None,
        Day,
        Night,
    }
}