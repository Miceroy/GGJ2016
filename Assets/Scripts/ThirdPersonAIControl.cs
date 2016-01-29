using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonAIControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_character; // A reference to the ThirdPersonCharacter on the object
        private Vector3 m_move;
        private bool m_jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        private bool m_crouch;
        private Transform player;
        
        private void Start()
        {
            // get the third person character ( this should never be null due to require component )
            m_character = GetComponent<ThirdPersonCharacter>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }


        private void Update()
        {
            m_move = player.position - gameObject.transform.position;
            m_move.Normalize();
            m_move = m_move * 0.8f;
            /*m_move.x = 1.0f;
            m_move.y = 0.0f;
            m_move.z = 0.0f;*/
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {

            // pass all parameters to the character control script
            m_character.Move(m_move, m_crouch, m_jump);
            m_jump = false;
        }
    }
}
