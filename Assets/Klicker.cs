using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klicker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //FindObjectOfType<AudioManager>().playSound("Happy", "ett");
            FindObjectOfType<AudioManager>().randomizeSound("Happy");
            Debug.Log("Spacetestknappfungerar");
        }

    }
}
