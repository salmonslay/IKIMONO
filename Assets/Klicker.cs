using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klicker : MonoBehaviour
{
    // För att kalla in ljud skriv : FindObjectOfType<AudioManager>().playSound("LjudetsKey", "LjudetsNamn");
    // För att randomizea en ljudlista skriv : FindObjectOfType<AudioManager>().randomizeSound("LjudetsKey");

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().playSound("Music", "Two");
        FindObjectOfType<AudioManager>().playSound("DayAmb", "One");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
           FindObjectOfType<AudioManager>().randomizeSound("Happy");
           FindObjectOfType<AudioManager>().randomizeSound("Eating");
           FindObjectOfType<AudioManager>().playSound("Button", "One"); 
            
            Debug.Log("Spacetestknappfungerar");
        }

    }
}
