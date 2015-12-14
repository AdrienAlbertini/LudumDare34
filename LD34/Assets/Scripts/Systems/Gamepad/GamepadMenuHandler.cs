using UnityEngine;
using System.Collections;

public class GamepadMenuHandler : MonoBehaviour
{
    public GamepadButtonHandler currentSelectedButton;
    public float untriggerThreshold = 0.4f;
    public float triggerThreshold = 0.6f;
    private bool _hTriggered, _vTriggered, _pressTriggered;
    

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKeyUp(KeyCode.JoystickButton0))
            this._pressTriggered = false;
        if (Input.GetKeyDown(KeyCode.JoystickButton0) && this._pressTriggered == false)
        {
            this._pressTriggered = true;
            AudioManager.Instance.PlaySound("MenuJoystickPush");
            currentSelectedButton.TriggerButton();
            if (this.currentSelectedButton.onTriggerNextButton != null)
                this.currentSelectedButton = this.currentSelectedButton.onTriggerNextButton;
        }
        else
        {
            if (h >= -untriggerThreshold && h <= untriggerThreshold)
            {
                this._hTriggered = false;
            }
            if (v >= -untriggerThreshold && v <= untriggerThreshold)
            {
                this._vTriggered = false;
            }
            if (h >= triggerThreshold)
            {
                if (currentSelectedButton.right != null && !this._hTriggered)
                {
                    this._hTriggered = true;
                    this._vTriggered = false;
                    this._NewSelectedButton(currentSelectedButton.right);
                }
            }
            else if (h <= -triggerThreshold)
            {
                if (currentSelectedButton.left != null && !this._hTriggered)
                {
                    this._hTriggered = true;
                    this._vTriggered = false;
                    this._NewSelectedButton(currentSelectedButton.left);
                }
            }
            if (v >= triggerThreshold)
            {
                if (currentSelectedButton.up != null && !this._vTriggered)
                {
                    this._vTriggered = true;
                    this._hTriggered = false;
                    this._NewSelectedButton(currentSelectedButton.up);
                }
            }
            else if (v <= -triggerThreshold)
            {
                if (currentSelectedButton.down != null && !this._vTriggered)
                {
                    this._vTriggered = true;
                    this._hTriggered = false;
                    this._NewSelectedButton(currentSelectedButton.down);
                }
            }
        }
    }

    private void _NewSelectedButton(GamepadButtonHandler newButton)
    {
        Debug.Log("New Selected Button: " + newButton.gameObject.name);
        this.currentSelectedButton = newButton;
        AudioManager.Instance.PlaySound("MenuJoystickMove");
    }
}
