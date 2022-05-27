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
    [SerializeField] private GameObject _clouds;

    [SerializeField] UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    
    
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
    
      

    private float minIntensity = 2.5f;
    private float maxIntensity = 4.5f;
    public bool dayTime;


    
    
   
    
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
            globalLight.intensity = maxIntensity;
            dayTime = true;

        }
        else
        {
            //Nightmode
            //Debug.Log("nu är jag i natt ifsatsen");
            GetComponent<Image>().sprite = _night;
            globalLight.intensity = minIntensity;
            dayTime = false;
        }

        if(Input.GetKeyDown(KeyCode.D))
            _overrideMode = OverrideMode.Day;
        else if (Input.GetKeyDown(KeyCode.N))
            _overrideMode = OverrideMode.Night;
        else if (Input.GetKeyDown(KeyCode.R))
            _overrideMode = OverrideMode.None;
        
        _clouds.SetActive(IsDay);
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