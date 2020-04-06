using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTriangule : StateMachineBehaviour
{
    public Transform barrilTop;
    public LineRenderer lr;
    public Transform barrilLeft;
    public LineRenderer lrL;
    public Transform barrilRight;
    public LineRenderer lrR;
    public Transform transform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        barrilTop = GameObject.Find("BarrilTop").GetComponent<Transform>();
        lr = GameObject.Find("Line").GetComponent<LineRenderer>();
        barrilLeft = GameObject.Find("BarrilLeft").GetComponent<Transform>();
        lrL = GameObject.Find("LineLeft").GetComponent<LineRenderer>();
        barrilRight = GameObject.Find("BarrilRight").GetComponent<Transform>();
        lrR = GameObject.Find("LifeRight").GetComponent<LineRenderer>();
        transform = animator.GetComponent<Transform>();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform.RotateAround(transform.position, Vector3.forward, 2f);
        RaycastHit2D hitInfo = Physics2D.Raycast(barrilTop.position, barrilTop.up);
        RaycastHit2D hitInfoL = Physics2D.Raycast(barrilLeft.position, barrilLeft.up);
        RaycastHit2D hitInfoR = Physics2D.Raycast(barrilRight.position, barrilRight.up);
        lr.enabled = true;
        lr.SetPosition(0, new Vector3(barrilTop.position.x, barrilTop.position.y, -1));
        lr.SetPosition(1, new Vector3(hitInfo.point.x, hitInfo.point.y, -1));
        lrL.SetPosition(0, new Vector3(barrilLeft.position.x, barrilLeft.position.y, -1));
        lrL.SetPosition(1, new Vector3(hitInfoL.point.x, hitInfoL.point.y, -1));
        lrR.SetPosition(0, new Vector3(barrilRight.position.x, barrilRight.position.y, -1));
        lrR.SetPosition(1, new Vector3(hitInfoR.point.x, hitInfoR.point.y, -1));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
