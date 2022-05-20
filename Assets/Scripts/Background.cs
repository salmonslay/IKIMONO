using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite _day;
    [SerializeField] private Sprite _night;
    private SoundDictionary<string, List<Sound>> test;
    public bool isMinigameOn;
    private bool isDaytime = true;
    public bool isAllowed = true;  // Sätts till true förest men behöver ändras till true fler gånger på andra ställen utan att påverka Update 
    private string soundList;
    
    
    

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



        //Debug.Log(System.DateTime.Now);
        //double testtime = 16.55;
        //Debug.Log("isAllowed" + isAllowed);

        

        //if (now.Hour == 6)
        //{
        //    isAllowed = true;
        //}else if (now.Minute == testtime)
        //{
        //    isAllowed = true;
        //}else if (isMinigameOn)
        //{
        //    isAllowed = true;
        //}


        if (isAllowed)
        {
            setPlayAmbianceSounds();
           
        }
        else
        {
            //Do while istället?
        }
    }

    private void setPlayAmbianceSounds()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // för att kolla vilken scen
       

        if (isDaytime)
        {
            
            soundList = "DayAmb";
        }
        else if (isMinigameOn)
        {
            
            soundList = "GameAmb";
            Debug.Log("THIS SHOULD BE MINIGAMEAMBIANCESOUNDS");
            Debug.Log(currentScene);
            
        }
        else
        {
            soundList = "NightAmb";
        }
        
        
        AudioManager.Instance.PlaySound(soundList, "One");
        isAllowed = false;
        
        



    }
    
    

    
}
