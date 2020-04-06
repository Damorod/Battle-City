using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovementTirangule : StateMachineBehaviour
{

    public GameObject player;
    public Transform transform;
    public Transform barrilTop;
    public Rigidbody2D r;
    public LineRenderer lr;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform = animator.GetComponent<Transform>();
        r = animator.GetComponent<Rigidbody2D>();
        barrilTop = GameObject.Find("BarrilTop").GetComponent<Transform>();
        lr = GameObject.Find("Line").GetComponent<LineRenderer>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform.up = player.transform.position - transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(barrilTop.position, barrilTop.up);
        if (!hitInfo.collider.CompareTag("ExtrasTileMap"))
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), 3f * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < 1.8f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), -3f * Time.deltaTime);
            }
            if ((hitInfo.collider.CompareTag("Player") || hitInfo.collider.CompareTag("Shield")) && !animator.GetBool("AttackRage"))
            {
                animator.SetTrigger("Attack");
                lr.enabled = false;
            }
            else
            {
                lr.enabled = false;
            }
        }
    }

    // OnStateExit is called when a transition ends and thesstate machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}
