using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite _day;
    [SerializeField] private Sprite _night;  

    // Start is called before the first frame update
    void Start()
    {
        
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

        }

        if ((now.Hour > SUNDOWNHOUR && now.Hour < 24) || (now.Hour > 0 && now.Hour < SUNUPHOUR))
        {
            //Nightmode
            //Debug.Log("nu är jag i natt ifsatsen");
            GetComponent<Image>().sprite = _night;
        }
    }
}
