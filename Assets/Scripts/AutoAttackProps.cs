using UnityEngine;
using System.Collections;

public class AutoAttackProps : MonoBehaviour {

    DynamicPropsController[] m_actionInterfaces = null;

    private void findDynamicPropsControllers(Collider other)
    {
        DynamicPropsController[] res = other.gameObject.GetComponentsInChildren<DynamicPropsController>();
        if (res.Length > 0)
            m_actionInterfaces = res;
    }

    void Update()
    {
        if( m_actionInterfaces != null )
        {
            for (int i = 0; i < m_actionInterfaces.Length; ++i)
            {
                Debug.Log("Police hits props!!");
                m_actionInterfaces[i].doAction();
            }

            m_actionInterfaces = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        findDynamicPropsControllers(other);
    }

    void OnTriggerStay(Collider other)
    {
        findDynamicPropsControllers(other);
    }

    void OnTriggerExit(Collider other)
    {
        m_actionInterfaces = null;
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        findDynamicPropsControllers(other);
    }

    void OnCollisionStay(Collision collision)
    {
    }
    void OnCollisionLeave(Collision collision)
    {
        m_actionInterfaces = null;
    }*/
}
