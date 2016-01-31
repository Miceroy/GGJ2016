using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoliceCarController : MonoBehaviour {

    public List<GameObject> waypoints = new List<GameObject>();

    public int waypointIndex = 0;
    public float moveSpeed = 2.0f;
    float waypointActivationRange = 0.8f;
    bool shouldMove = false;

    public float activationTimer = 30.0f;
    public bool activateCountdown = false;

    public GameObject modelToActivate;

    public int amountOfCopsInCar = 4;
    public GameObject copTemplate;

    bool stopAfterLastWaypoint = true;

    public Vector3 m_move;
    public Vector3 targetPos;
    Transform t;
    // Use this for initialization
    void Start () {
        modelToActivate.SetActive(false);
        t = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (activateCountdown)
            activationTimer -= Time.deltaTime;
        if (activationTimer <= 0 && activateCountdown)
        {
            activationTimer = 0;
            activateCountdown = false;
            shouldMove = true;
            modelToActivate.SetActive(true);
        }
	
	}
    void DestinationReached()
    {
        shouldMove = false;
        if (copTemplate == null)
            Debug.Log("This was a ghost car");
        else
            SpawnCops();

    }
    void SpawnCops()
    {
        for (int a = 0; a < amountOfCopsInCar; ++a)
        {
            Vector3 spawnVec = new Vector3(t.position.x - 1, t.position.y, t.position.z - a + 2);
            GameObject cop = (GameObject)Instantiate(copTemplate, spawnVec, Quaternion.identity);
        }
    }
    private void FixedUpdate()
    {
        if (shouldMove)
        {
            
            RaycastHit rh;
            //Some layer test code, futher inspection required
            int omitPoliceLayerMask = 1 << 9;
            omitPoliceLayerMask = ~omitPoliceLayerMask;

            omitPoliceLayerMask = -1;
            //Overrode it to look thru all layers


            targetPos = Vector3.zero;
            if (waypoints.Count > 0)
            {

                targetPos = waypoints[waypointIndex].transform.position;
                if (Vector3.Distance(targetPos, t.position) <= waypointActivationRange)
                {
                    waypoints[waypointIndex].GetComponent<Waypoint>().StartTimer();
                    if (waypoints[waypointIndex].GetComponent<Waypoint>().IsDone())
                    {
                        ++waypointIndex;
                        if (waypointIndex >= waypoints.Count)
                        {
                            waypointIndex = 0;
                            if (stopAfterLastWaypoint)
                            {
                                shouldMove = false;
                                DestinationReached();
                            }
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


            m_move.y = 0;
            Quaternion q = Quaternion.LookRotation(m_move);
            t.rotation = Quaternion.Slerp(t.rotation, q, Time.deltaTime * 20);
            t.position += t.forward * moveSpeed * Time.deltaTime;

        }
    }
    
}
