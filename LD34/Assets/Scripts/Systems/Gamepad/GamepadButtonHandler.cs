using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamepadButtonHandler : MonoBehaviour
{
    public GamepadButtonHandler left;
    public GamepadButtonHandler right;
    public GamepadButtonHandler up;
    public GamepadButtonHandler down;
    public GamepadButtonHandler onTriggerNextButton;
    private Button _button;

    void Awake()
    {
        this._button = this.GetComponent<Button>();
    }

    public void TriggerButton()
    {
        if (this._button != null)
            this._button.onClick.Invoke();
    }
}
