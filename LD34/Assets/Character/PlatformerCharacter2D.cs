using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxJumpVelocity = 50.0f;  
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private bool m_LeftCollide;
        private bool m_RightCollide;
        public Transform[] m_GroundCheck;   // A position marking where to check for ceilings
        private Transform m_TopCheck;   // A position marking where to check for ceilings
        private Transform m_DownCheck;   // A position marking where to check for ceilings
        
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
       // private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private BoxCollider2D m_collider;

        private void Awake()
        {
            // Setting up references.
            m_TopCheck = transform.Find("TopCheck");
            m_DownCheck = transform.Find("BottomCheck");
         //   m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<BoxCollider2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            m_LeftCollide = false;
            m_RightCollide = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            bool m_transferForce = false;
            GameObject m_PLayertotransfer = null;
            foreach (Transform tra in m_GroundCheck)
            {
                 RaycastHit2D[] colliders = Physics2D.RaycastAll(tra.position, (this.transform.up * -1), k_GroundedRadius, m_WhatIsGround);
                 for (int i = 0; i < colliders.Length; i++)
                 {
                     if (colliders[i].transform.gameObject != gameObject)
                     {
                      m_Grounded = true;
                      if (colliders[i].transform.gameObject.transform.tag == "Player")
                      {
                          m_transferForce = true;
                          m_PLayertotransfer = colliders[i].transform.gameObject;
                      }
                     }
                 }   
            }
            
            if (m_transferForce)
            {
                Vector2 vel =  m_PLayertotransfer.GetComponent<Rigidbody2D>().velocity;
                Vector2 _vel = this.m_Rigidbody2D.velocity;
                _vel.x += vel.x;
                this.m_Rigidbody2D.velocity = _vel;
            }
            
            RaycastHit2D[] collidersLeftTop = Physics2D.RaycastAll(m_TopCheck.position, (this.transform.right * -1), 0.5f * this.transform.localScale.x + 0.05f, m_WhatIsGround);
            for (int i = 0; i < collidersLeftTop.Length; i++)
            {
                if (collidersLeftTop[i].transform.gameObject != gameObject)
                    m_LeftCollide = true;
            }
            
            RaycastHit2D[] collidersLeftBottom = Physics2D.RaycastAll(m_DownCheck.position, (this.transform.right * -1), 0.5f * this.transform.localScale.x +  0.05f, m_WhatIsGround);
            for (int i = 0; i < collidersLeftBottom.Length; i++)
            {
                if (collidersLeftBottom[i].transform.gameObject != gameObject)
                    m_LeftCollide = true;
            }
            
            RaycastHit2D[] collidersRightTop = Physics2D.RaycastAll(m_TopCheck.position, (this.transform.right), 0.5f * this.transform.localScale.x +  0.05f, m_WhatIsGround);
            for (int i = 0; i < collidersRightTop.Length; i++)
            {
                if (collidersRightTop[i].transform.gameObject != gameObject)
                    m_RightCollide = true;
            }
            
            Debug.DrawRay(m_DownCheck.position, this.transform.right * 0.51f * this.transform.localScale.x, Color.white, 0.1f);
            RaycastHit2D[] collidersRightBottom = Physics2D.RaycastAll(m_DownCheck.position, (this.transform.right), 0.5f * this.transform.localScale.x + 0.05f, m_WhatIsGround);
            for (int i = 0; i < collidersRightBottom.Length; i++)
            {
                if (collidersRightBottom[i].transform.gameObject != gameObject)
                {
                    m_RightCollide = true;
                }
            }
           // m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
           // m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool jump)
        {

            if (m_LeftCollide == true && move < 0.0f)
                move = 0.0f;
            if (m_RightCollide == true && move > 0.0f)
                move = 0.0f;

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                ///  m_Anim.SetFloat("Speed", Mathf.Abs(move));


                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
              //  m_Anim.SetBool("Ground", false);
               // m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
              Vector2 tmp =  m_Rigidbody2D.velocity;
              float velocityY =  m_JumpForce * this.transform.localScale.x;
              if (velocityY > m_MaxJumpVelocity)
                velocityY = m_MaxJumpVelocity;
              tmp.y = velocityY;
              m_Rigidbody2D.velocity = tmp;
            }
        }

       

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
