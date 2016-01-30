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

    public Vector3 m_move;
    private Transform player;
    Transform t;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        t = GetComponent<Transform>();
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

        targetPos.y = t.position.y;
        m_move = (targetPos - t.position).normalized;
        m_move.Normalize();

   
        
        if (Physics.Raycast(t.position, m_move, out rh, 80))
        {
            if (rh.collider.gameObject.tag == "Player")
            { useDefaultWaypoints = false; }
            else
            {
                if (Physics.Raycast(t.position, t.forward, out rh, 80, omitPoliceLayerMask))
                {
                    if (rh.transform != t)
                        m_move += rh.normal * 50;
                }

                Vector3 l_ray = t.position + new Vector3(-2.0f, 0, 0);
                Vector3 r_ray = t.position + new Vector3(2.0f, 0, 0);
                if (Physics.Raycast(l_ray, t.forward, out rh, 80, omitPoliceLayerMask))
                {
                    if (rh.transform != t)
                        m_move += rh.normal * 50;
                }

                if (Physics.Raycast(r_ray, t.forward, out rh, 80, omitPoliceLayerMask))
                {
                    if (rh.transform != t)
                        m_move += rh.normal * 50;
                }
            }
        }


        m_move.y = 0;
        Quaternion q = Quaternion.LookRotation(m_move);
        t.rotation = Quaternion.Slerp(t.rotation, q, Time.deltaTime * 20);
        t.position += t.forward * 3 * Time.deltaTime;

    }
}
