using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klicker : MonoBehaviour
{
   
    void Start()
    {
        AudioManager.Instance.playSound("Music", "Two");
        AudioManager.Instance.playSound("DayAmb", "One");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //AudioManager.Instance.randomizeSound("Happy");
            //FindObjectOfType<AudioManager>().randomizeSound("Eating");
            AudioManager.Instance.playSound("Jump", "One"); 

            Debug.Log("Spacetestknappfungerar");
        }

    }
}
