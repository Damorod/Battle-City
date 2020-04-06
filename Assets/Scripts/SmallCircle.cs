using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCircle : MonoBehaviour
{
    bool vertical;
    float limitX;
    float limitY = 2f;

    Vector2 nextPos;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        int a = Random.Range(0, 2) * 2 - 1;
        //if( a == -1){
        vertical = true;
        nextPos = new Vector2(transform.position.x, limitY);

        //}
        //else
        //{
        //    vertical = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPos, 3f * Time.deltaTime);
            if (Vector2.Distance(transform.position, nextPos) <= 0.1)
            {
                changePos();
            }
        }
    }

    void changePos()
    {
        if (nextPos == new Vector2(transform.position.x, transform.position.y))
        {
            nextPos = new Vector2(transform.position.x, -limitY);
        }
        else
        {
            nextPos = new Vector2(transform.position.x, limitY);
        }
    }
}
