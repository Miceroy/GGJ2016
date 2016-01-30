using System;
using UnityEngine;

public class CustomAIMoveScript : MonoBehaviour
{

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
            m_move = (player.position - t.position).normalized;
            m_move.Normalize();

        //Some layer test code, futher inspection required
            int omitPoliceLayerMask = 1 << 9;
            omitPoliceLayerMask = ~omitPoliceLayerMask;

            omitPoliceLayerMask = -1;
        //Overrode it to look thru all layers
            RaycastHit rh;
            if (Physics.Raycast(t.position, m_move, out rh, 80))
            {
                if (rh.collider.gameObject.tag == "Player")
                { }
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
