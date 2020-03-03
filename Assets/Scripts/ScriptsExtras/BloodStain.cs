using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStain : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] blood;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        int random = Random.Range(0, blood.Length);
        rend.sprite = blood[random];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
