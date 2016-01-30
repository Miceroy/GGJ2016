using UnityEngine;
using System.Collections;

public class DynamicPropsController : MonoBehaviour, ActionInterface
{
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
            Rigidbody body = GetComponent<Rigidbody>();
            if (body != null)
            {
                Vector3 delta = gameObject.transform.position - other.transform.position;
                delta.Normalize();
                body.AddExplosionForce(10.0f * body.mass,
                    other.transform.position, 1.0f, 1.0f, ForceMode.Impulse);
                m_cooldown = 0.5f;
            }
        }
    }
}
