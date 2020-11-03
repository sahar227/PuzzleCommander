using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    int seconds, minutes, hours;
    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        minutes = 0;
        hours = 0;
        InvokeRepeating("UpdateTimer", 0, 1);
    }

    // Update is called once per frame
    void UpdateTimer()
    {
        seconds++;
        if(seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }
        if(minutes >=60 )
        {
            minutes = 0;
            hours++;
        }
        string secString= "", minString = "", HrString = "";
        if (seconds < 10)
            secString += "0";
        if (minutes < 10)
            minString += "0";
        if (hours < 10)
            HrString += "0";

        secString += seconds.ToString();
        minString += minutes.ToString();
        HrString += hours.ToString();

        this.GetComponent<TextMeshProUGUI>().text = $"Time: {HrString} : {minString} : {secString}";
    }
}
