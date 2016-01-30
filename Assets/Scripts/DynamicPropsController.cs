using UnityEngine;
using System.Collections;

public class DynamicPropsController : MonoBehaviour, ActionInterface
{
    float m_cooldown;
    GameObject m_collidingPlayer;

    public void Start()
    {
        m_collidingPlayer = null; 
        m_cooldown = 0.0f;
    }

    public void Update()
    {
        m_cooldown -= Time.deltaTime;
    }

    public void doAction()
    {
        if( m_cooldown < 0.0f )
        {
            if( m_collidingPlayer != null)
            {
                Rigidbody body = GetComponent<Rigidbody>();
                if (body != null)
                {
                    Vector3 delta = gameObject.transform.position - m_collidingPlayer.transform.position;
                    delta.Normalize();
                    body.AddExplosionForce(10.0f * body.mass,
                        m_collidingPlayer.transform.position, 1.0f, 1.0f, ForceMode.Impulse);
                    m_cooldown = 0.5f;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "Player" )
        {
            if( m_collidingPlayer != null ) Debug.Break();

            m_collidingPlayer = other.gameObject;
        }
    }

    void OnTriggerStay(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
       if( other.gameObject.tag == "Player" )
       {
            m_collidingPlayer = null;
       }
    }
}
