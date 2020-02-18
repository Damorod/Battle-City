using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    // Start is called before the first frame update
    float height;
    float widht;
    public Rigidbody2D r;
    void Start()
    {
        height = transform.localScale.y;
        widht = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        float x = Input.GetAxisRaw("Horizontal") * 5;
        float y = Input.GetAxisRaw("Vertical") * 5;
        if ((transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + height / 2 && y < 0) ||
                (transform.position.y > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y - height / 2 && y > 0))
        {
            y = 0;
        }

        if ((transform.position.x > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x - widht / 2 && x > 0) ||
            (transform.position.x < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + widht / 2 && x < 0))
        {
            x = 0;
        }
        r.AddForce(new Vector2(x, y));
        //transform.Translate(x, y, 0);
    }
}
