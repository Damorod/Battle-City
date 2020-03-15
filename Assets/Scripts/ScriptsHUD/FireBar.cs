using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        fill.color = Color.green;
    }
    // Start is called before the first frame update
    public void Ready(float r)
    {
        if(r == 0)
        {
            fill.color = Color.red;
        }
        else
        {
            fill.color = Color.green;
        }
    }
}
