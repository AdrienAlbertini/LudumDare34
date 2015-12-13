using System;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        [SerializeField] bool IsPLayerA = false;
        [SerializeField] CharacterManager MainController;
        [SerializeField] Transform[] RightCheck;
        [SerializeField] Transform[] LefttCheck;
        [SerializeField] Transform[] BottomtCheck;
        [SerializeField] Transform[] TopCheck;
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


        public bool calculateIfIcanGrow()
        {
         bool ret = false;
         float MinDistanceXRight = 500.0f;
         float MinDistanceXLeft = 500.0f;
         float MinDistanceX = 0.0f;
         float Size = 0.0f;
         
         
            if (IsPLayerA)
            {
                Size = MainController.SizePlayerA;
            }
            else
            {
                Size = MainController.SizePLayerB;
            }
         
         
         foreach (Transform tra in RightCheck)
            {
                 RaycastHit2D[] collidersLeftTop = Physics2D.RaycastAll(tra.transform.position, (this.transform.right), 3.0f);
                 for (int i = 0; i < collidersLeftTop.Length; i++)
                 {
                         if (collidersLeftTop[i].transform.gameObject != gameObject && collidersLeftTop[i].transform.gameObject.transform.tag != "Player")
                        {
                            if (MinDistanceXRight > collidersLeftTop[i].distance)
                                MinDistanceXRight = collidersLeftTop[i].distance;
                         }
                     }
             }
            foreach (Transform tra in LefttCheck)
            {
                 RaycastHit2D[] collidersLeftTop = Physics2D.RaycastAll(tra.transform.position, (this.transform.right * -1), 3.0f);
                 for (int i = 0; i < collidersLeftTop.Length; i++)
                 {
                         if (collidersLeftTop[i].transform.gameObject != gameObject && collidersLeftTop[i].transform.gameObject.transform.tag != "Player")
                        {
                            if (MinDistanceXLeft > collidersLeftTop[i].distance)
                                MinDistanceXLeft = collidersLeftTop[i].distance;
                         }
                     }
             }
             if (MinDistanceXRight == 500.0f)
                MinDistanceXRight = 0.0f;
             if (MinDistanceXLeft == 500.0f)
                MinDistanceXLeft = 0.0f;
             MinDistanceX =  MinDistanceXLeft + MinDistanceXRight;
             if (MinDistanceX < 0)
                return false;
             //Debug.Log(MinDistanceX + "-----" +  0.1  + "   " +  IsPLayerA);
             if (MinDistanceX == 0 || MinDistanceX > 1f)
                return true;
             else
                return false;
            }   
    }
