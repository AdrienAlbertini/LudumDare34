using UnityEngine;
using System.Collections.Generic;

namespace Custom.Inputs.VO
{
    public interface IInputVO
    {
        int GetInputsCount();
        float GetMouseWheel();
        Inputs.InputsManager.InputState GetInputState();
    }

    public interface IActionVO : IInputVO
    {
        Custom.Inputs.Actions.ActionType GetActionType();
    }

    public interface IKeyInputVO : IInputVO
    {
        KeyCode GetKeyCode();
    }

    public interface IMouseInputVO : IInputVO
    {
        InputsManager.MouseButton GetMouseButton();
    }

}
