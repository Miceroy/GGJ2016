﻿using UnityEngine;
using System.Collections;

public class PlayerCharacterController : MonoBehaviour {
   // private bool m_moveEnabled;

    public void DisableMovement()
    {
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().haltMovement();
    }

    ActionInterface[] m_actionInterfaces = null;

    private void findActionIntrerfaces(Collider other)
    {
        ActionInterface[] res = other.gameObject.GetComponentsInChildren<ActionInterface>();
        if (res.Length > 0)
            m_actionInterfaces = res;
    }

    void Update()
    {
        if (m_actionInterfaces != null && Input.GetAxis("Fire1") > 0.1f)
        {
            for (int i = 0; i < m_actionInterfaces.Length; ++i)
            {
                Debug.Log("dfsdfsdfsd");
                m_actionInterfaces[i].doAction(gameObject);
            }

            m_actionInterfaces = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Police")
        {
            GameSceneController sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSceneController>();
            sceneController.onLevelFailed();
            other.gameObject.GetComponent<CustomAIMoveScript>().ActivateBashing();
        }

        findActionIntrerfaces(other);
    }

    void OnTriggerStay(Collider other)
    {
        findActionIntrerfaces(other);
    }

    void OnTriggerExit(Collider other)
    {
        m_actionInterfaces = null;
    }
}
