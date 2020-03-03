using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public Animator camAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void shaker()
    {
        camAnimation.SetTrigger("Shake");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
