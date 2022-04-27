using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klicker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().playSound("Music", "ett");
        FindObjectOfType<AudioManager>().playSound("DayAmb", "ett");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
           FindObjectOfType<AudioManager>().randomizeSound("Happy");
           FindObjectOfType<AudioManager>().randomizeSound("Eating");
            
            Debug.Log("Spacetestknappfungerar");
        }

    }
}
