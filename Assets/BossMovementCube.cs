using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovementCube : StateMachineBehaviour
{
    public GameObject player;
    public Transform transform;
    public Transform barrilTop;
    public Rigidbody2D r;
    public Vector3 target;
    public float speed;

    //public LineRenderer lr;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speed = 3f;
        player = GameObject.FindGameObjectWithTag("Player");
        transform = animator.GetComponent<Transform>();
        r = animator.GetComponent<Rigidbody2D>();
        barrilTop = GameObject.Find("Barril").GetComponent<Transform>();
        target = player.transform.position;
        //lr = GameObject.Find("Line").GetComponent<LineRenderer>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform.up = player.transform.position - transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(barrilTop.position, barrilTop.up, 2);
        if (hitInfo.collider != null && (hitInfo.collider.CompareTag("ExtrasTileMap") || hitInfo.collider.CompareTag("EnemyCircle")))
        {
            target = transform.position;
        }
        else if ((target == transform.position))
        {
            target = player.transform.position;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
