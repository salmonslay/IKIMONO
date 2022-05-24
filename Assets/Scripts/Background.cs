using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using IKIMONO.Pet;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite _day;
    [SerializeField] private Sprite _night;
    private SoundDictionary<string, List<Sound>> test;
    
    private bool isDaytime;
    private bool isAllowed = true;
    private bool isMusicAllowed = true;
    private string stringNameOfCurrentAmbSound;
    
    private Pet _pet;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.Instance.PlaySound("Music", "Two");
        // när det är dag


    }

    // Update is called once per frame
    void Update()
    {
        
        //What time is it now and what mode am I in
        const int SUNUPHOUR = 6;
        const int SUNDOWNHOUR = 20;

        //Get current hour
        System.DateTime now = System.DateTime.Now;

        //Debug.Log($"timmen är nu: {now.Hour}");

        if (now.Hour > SUNUPHOUR && now.Hour < SUNDOWNHOUR)
        {
            //Daymode
            //Debug.Log("nu är jag i dag ifsatsen");
            GetComponent<Image>().sprite = _day;
            isDaytime = true;


        }

        if ((now.Hour > SUNDOWNHOUR && now.Hour < 24) || (now.Hour > 0 && now.Hour < SUNUPHOUR))
        {
            //Nightmode
            //Debug.Log("nu är jag i natt ifsatsen");
            GetComponent<Image>().sprite = _night;
            isDaytime = false;
            


        }


        Debug.Log("Time of day: " + isDaytime);   // Städa sen
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
      
        if (isDaytime == true)
        {
            
            stringNameOfCurrentAmbSound = "DayAmb";
            Debug.Log(stringNameOfCurrentAmbSound);
            AudioManager.Instance.PlaySound(stringNameOfCurrentAmbSound, "One");
        }
       
        if (isDaytime == false)
        {
            stringNameOfCurrentAmbSound = "NightAmb";
            AudioManager.Instance.PlaySound(stringNameOfCurrentAmbSound, "One");
        }
        
       
        isAllowed = false;
        
        
    }
    
    

    
}
