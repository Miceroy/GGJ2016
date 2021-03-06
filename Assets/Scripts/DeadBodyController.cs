﻿using UnityEngine;
using System.Collections;

public class DeadBodyController : MonoBehaviour, ActionInterface
{
    public float impulse = 100.0f;
    public Rigidbody body;

    float m_cooldown;

    public void Start()
    {
        m_cooldown = 0.0f;
    }

    public void Update()
    {
        m_cooldown -= Time.deltaTime;
    }

    public void doAction(GameObject other)
    {
        if( m_cooldown < 0.0f )
        {
            if (body != null)
            {
                Vector3 delta = gameObject.transform.position - other.transform.position;
                delta.Normalize();
                body.AddExplosionForce(impulse,
                    other.transform.position, 10.0f, 1.0f, ForceMode.Impulse);
                m_cooldown = 0.5f;
                Debug.Log("toimii törmäys");
            }
        }
    }
}
