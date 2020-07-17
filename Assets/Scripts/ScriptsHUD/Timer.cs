using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    static public int minutes;
    static public float seconds;
    public GameObject screenWon;
    public GameObject screenDeath;
    public Text time;
    public Text displayTimeDeath;
    public Text displayTime;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (screenWon != null && screenWon.activeSelf)
        {
            displayTime.text = "Time: " + minutes.ToString("d2") + ":" + seconds.ToString("00");
        }
        if (screenDeath != null && screenDeath.activeSelf)
        {
            displayTimeDeath.text = "Time: " + minutes.ToString("d2") + ":" + seconds.ToString("00");
        }
        else
        {
            seconds += 1 * Time.deltaTime;
            if ((int)seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
            time.text = minutes.ToString("d2") + ":" + seconds.ToString("00");
        }
    }
    
    public int getMinutes()
    {
        return minutes;
    }
}
