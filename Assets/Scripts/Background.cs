using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite _day;
    [SerializeField] private Sprite _night;
    [SerializeField] private GameObject _dayObjects;

    [SerializeField] private Light2D globalLight;
    
    
    private const int SunUpHour = 6;
    private const int SunDownHour = 20;
    
    private static OverrideMode _overrideMode = OverrideMode.None;
    
    /// <summary>
    /// Checks if it currently is day or not, with overrides in mind.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static bool IsDay { get 
    {
        return _overrideMode  switch
        {
            OverrideMode.None => DateTime.Now.Hour >= SunUpHour && DateTime.Now.Hour <= SunDownHour,
            OverrideMode.Day => true,
            OverrideMode.Night => false,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }}

    private const float MinIntensity = 2.0f;
    private const float MaxIntensity = 4.5f;

    private void Update()
    {
        //What time is it now and what mode am I in
        //Get current hour
        DateTime now = DateTime.Now;

        //Debug.Log($"timmen är nu: {now.Hour}");

        if (IsDay)
        {
            GetComponent<Image>().sprite = _day;
            globalLight.intensity = MaxIntensity;

        }
        else
        {
            GetComponent<Image>().sprite = _night;
            globalLight.intensity = MinIntensity;
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.D))
                _overrideMode = OverrideMode.Day;
            else if (Input.GetKeyDown(KeyCode.N))
                _overrideMode = OverrideMode.Night;
            else if (Input.GetKeyDown(KeyCode.R))
                _overrideMode = OverrideMode.None;
        }

        _dayObjects.SetActive(IsDay);
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