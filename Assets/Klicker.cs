//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Klicker : MonoBehaviour
//{
   
//    void Start()
//    {
//        AudioManager.Instance.PlaySound("Music", "Two");
//        AudioManager.Instance.PlaySound("DayAmb", "One");
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetButtonDown("Jump"))
//        {

//            AudioManager.Instance.RandomizeSound("Bubbles");
//            //AudioManager.Instance.RandomizeSound("Happy");
//            //FindObjectOfType<AudioManager>().randomizeSound("Eating");
//            //AudioManager.Instance.playSound("Jump", "One"); 

//            Debug.Log("Spacetestknappfungerar");
//        }
//        if (Input.GetButtonDown("Submit"))
//        {
//            Debug.Log("Return/Enter key pressed");
//            AudioManager.Instance.StopSound("Music", "Two");
//        }

//    }
//}
