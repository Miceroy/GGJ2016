using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomAIMoveScript : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();

    public bool useDefaultWaypoints = true;
    public int waypointIndex = 0;
    public float moveSpeed = 2.0f;
    float waypointActivationRange = 0.1f;

    float m_RunCycleLegOffset = 0.2f;

    public float animSpeed = 1.0f;

    public Vector3 m_move;
    private Transform player;
    Transform t;
    Vector3 prevPos = Vector3.zero;
    NavMeshAgent brains;

    Animator m_Animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        t = GetComponent<Transform>();
        brains = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        prevPos = t.position;
        //m_Animator.applyRootMotion = true;
    }


    private void Update()
    {
        
    }

    
    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        RaycastHit rh;
        //Some layer test code, futher inspection required
        int omitPoliceLayerMask = 1 << 9;
        omitPoliceLayerMask = ~omitPoliceLayerMask;

        omitPoliceLayerMask = -1;
        //Overrode it to look thru all layers


        Vector3 targetPos = player.position;
        if (Physics.Raycast(t.position, Vector3.Normalize(targetPos - t.position), out rh, 80))
        {
            if (rh.collider.gameObject.tag == "Player")
            {
                useDefaultWaypoints = false;
            }
        }
        if (useDefaultWaypoints && waypoints.Count > 0)
        {
            
            targetPos = waypoints[waypointIndex].transform.position;
            if (Vector3.Distance(targetPos, t.position) <= waypointActivationRange )
            {
                waypoints[waypointIndex].GetComponent<Waypoint>().StartTimer();
                if (waypoints[waypointIndex].GetComponent<Waypoint>().IsDone())
                {
                    ++waypointIndex;
                    if (waypointIndex >= waypoints.Count)
                    {
                        waypointIndex = 0;
                    }
                    targetPos = waypoints[waypointIndex].transform.position;
                }
            }
            
        }
        brains.destination = targetPos;
        UpdateAnimator(t.position - prevPos);
        prevPos = t.position;

    }
    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        float m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_Animator.SetFloat("Forward", move.z, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        
        //m_Animator.SetBool("Crouch", m_Crouching);
        //m_Animator.SetBool("OnGround", m_IsGrounded);
        //if (!m_IsGrounded)
        //{
        //    m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        //}

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        //float runCycle =
         //   Mathf.Repeat(
         //       m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        //float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
        //if (m_IsGrounded)
        //{
        //    m_Animator.SetFloat("JumpLeg", jumpLeg);
        //}

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        //if (/*m_IsGrounded &&*/ move.magnitude > 0)
        //{
            m_Animator.speed = animSpeed;
        //}
        //else
        //{
            // don't use that while airborne
        //    m_Animator.speed = 1;
        //}
    }
}
