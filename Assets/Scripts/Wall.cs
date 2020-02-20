using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private Vector2 screen;

    // Start is called before the first frame update
    void Start()
    {
        screen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screen.x, screen.x * -1);
        viewPos.y = Mathf.Clamp(viewPos.y, screen.y, screen.y * -1);
        transform.position = viewPos;
    }
}
