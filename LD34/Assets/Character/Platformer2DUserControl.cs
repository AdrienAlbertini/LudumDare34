using System;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        [SerializeField] bool IsPLayerA = false;
        [SerializeField] CharacterManager MainController;
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            
            if (IsPLayerA)
            {
                this.transform.localScale = new Vector3(MainController.SizePlayerA,MainController.SizePlayerA,MainController.SizePlayerA);
            }
            else
            {
                this.transform.localScale = new Vector3(MainController.SizePLayerB,MainController.SizePLayerB,MainController.SizePLayerB);
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = 0.0f;
            float v = 0.0f;
            if (IsPLayerA)
            {
                 h = CrossPlatformInputManager.GetAxis("Horizontal");
                 v = CrossPlatformInputManager.GetAxis("Vertical");
            }
            else
            {
                 h = CrossPlatformInputManager.GetAxis("HorizontalB");
                 v = CrossPlatformInputManager.GetAxis("VerticalB");
            }
            bool Jump = false;
            bool Grow = false;
            if (v > 0)
            {
                Jump = true;
            }
            else if (v < 0)
            {
                Grow = true;
            }
            MainController.Grow(Grow, IsPLayerA);
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, Jump);
            m_Jump = false;
            Jump = false;
        }
    }
