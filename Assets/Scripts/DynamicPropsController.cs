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
                Vector3 delta = gameObject.transform.position - m_collidingPlayer.transform.position;
                delta.Normalize();
                body.AddRelativeForce(10*body.mass*delta, ForceMode.Impulse);
                m_cooldown = 1.0f;
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
