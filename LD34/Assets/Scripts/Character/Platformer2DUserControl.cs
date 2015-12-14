using System;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    [SerializeField]
    public bool IsPLayerA = false;
    [SerializeField]
    CharacterManager MainController;
    [SerializeField]
    Transform[] RightCheck;
    [SerializeField]
    Transform[] LefttCheck;
    [SerializeField]
    Transform[] BottomtCheck;
    [SerializeField]
    Transform[] TopCheck;
    private PlatformerCharacter2D m_Character;
    private bool canJump = true;
    public event EventHandler growStart;
    public event EventHandler growOver;
    public bool LastFrameGrowingPushed = false;
    public bool Grow = false;
    private bool FirstTimeGrowing = true;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }


    private void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (IsPLayerA)
        {
            this.transform.localScale = new Vector3(MainController.SizePlayerA, MainController.SizePlayerA, MainController.SizePlayerA);
        }
        else
        {
            this.transform.localScale = new Vector3(MainController.SizePlayerB, MainController.SizePlayerB, MainController.SizePlayerB);
        }
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = 0.0f;
        float v = 0.0f;
        if (IsPLayerA)
        {
            h = CrossPlatformInputManager.GetAxis("Horizontal");
            v = CrossPlatformInputManager.GetAxisRaw("Vertical");
        }
        else
        {
            h = CrossPlatformInputManager.GetAxis("HorizontalB");
            v = CrossPlatformInputManager.GetAxisRaw("VerticalB");
        }
        bool Jump = false;
        Grow = false;
        if (v > 0)
        {
            Jump = true;
        }
        if (v < 0)
        {
            Grow = true;
        }
        bool GrowingPushed = false;
        GrowingPushed = MainController.Grow(Grow, IsPLayerA);
        if (LastFrameGrowingPushed == false && GrowingPushed == true)
        {
            this.growStart(this, EventArgs.Empty);
            //TODO Start song;
        }

        if (LastFrameGrowingPushed == true && GrowingPushed == false)
        {
            this.growOver(this, EventArgs.Empty);
            //TODO Stop le son de gonflement;
        }
        LastFrameGrowingPushed = GrowingPushed;
        // Pass all parameters to the character control script.
        if (canJump == false)
            Jump = false;
        m_Character.Move(h, crouch, Jump);
        Jump = false;
    }


    public bool calculateIfIcanGrow()
    {
        bool ret = false;
        float MinDistanceXRight = 500.0f;
        float MinDistanceXLeft = 500.0f;
        float MinDistanceYTop = 500.0f;
        float MinDistanceYBottom = 500.0F;
        float MinDistanceX = 0.0f;
        float MinDistanceY = 0.0f;
        float Size = 0.0f;


        if (IsPLayerA)
        {
            Size = MainController.SizePlayerA;
        }
        else
        {
            Size = MainController.SizePlayerB;
        }


        foreach (Transform tra in RightCheck)
        {
            if (tra != null)
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
        }
        foreach (Transform tra in LefttCheck)
        {
            if (tra != null)
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
        }

        foreach (Transform tra in TopCheck)
        {
            if (tra != null)
            {
                RaycastHit2D[] collidersLeftTop = Physics2D.RaycastAll(tra.transform.position, (this.transform.up), 3.0f);
                for (int i = 0; i < collidersLeftTop.Length; i++)
                {
                    if (collidersLeftTop[i].transform.gameObject != gameObject && collidersLeftTop[i].transform.gameObject.transform.tag != "Player")
                    {
                        if (MinDistanceYTop > collidersLeftTop[i].distance)
                            MinDistanceYTop = collidersLeftTop[i].distance;
                    }
                }
            }
        }
        foreach (Transform tra in BottomtCheck)
        {
            if (tra != null)
            {
                RaycastHit2D[] collidersLeftTop = Physics2D.RaycastAll(tra.transform.position, (this.transform.up * -1), 3.0f);
                for (int i = 0; i < collidersLeftTop.Length; i++)
                {
                    if (collidersLeftTop[i].transform.gameObject != gameObject && collidersLeftTop[i].transform.gameObject.transform.tag != "Player")
                    {
                        if (MinDistanceYBottom > collidersLeftTop[i].distance)
                            MinDistanceYBottom = collidersLeftTop[i].distance;
                    }
                }
            }
        }

        /*   if (MinDistanceYBottom == 500.0f)
              MinDistanceYBottom = -1.0f;
           if (MinDistanceYTop == 500.0f)
              MinDistanceYTop = -1.0f;
          */
        /* if (MinDistanceXRight == 500.0f)
            MinDistanceXRight = -1.0f;
         if (MinDistanceXLeft == 500.0f)
            MinDistanceXLeft = -1.0f;*/
        MinDistanceY = MinDistanceYBottom + MinDistanceYTop;
        MinDistanceX = MinDistanceXLeft + MinDistanceXRight;
        if (MinDistanceX < 0)
            ret = true;
        else if (MinDistanceX == 0)
            ret = false;
        else if (MinDistanceX > 0.1f)
            ret = true;
        else
            ret = false;
        if (ret == false)
        {
            m_Character.GetComponent<Rigidbody2D>().gravityScale = 0;
            Vector2 vec = m_Character.GetComponent<Rigidbody2D>().velocity;
            vec.y = 0.0f;
            m_Character.GetComponent<Rigidbody2D>().velocity = vec;
            canJump = false;
        }
        else
        {
            if (m_Character != null)
            {
                m_Character.GetComponent<Rigidbody2D>().gravityScale = 3;
                canJump = true;
            }
        }

        bool ret2 = false;
        if (MinDistanceY < 0)
            ret2 = true;
        else if (MinDistanceY == 0)
            ret2 = false;
        else if (MinDistanceY > 0.1f)
            ret2 = true;
        else
            ret2 = false;
        if (ret == true && ret2 == true)
            return true;
        else
            return false;


    }
}
