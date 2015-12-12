using UnityEngine;
using System.Collections.Generic;
using Custom.Inputs.VO;

namespace Custom.Inputs
{
    public static class InputsHelper
    {
        public static IActionVO GetIActionVOByType(this List<IActionVO> pActionList, Custom.Inputs.Actions.ActionType pType)
        {
            for (int i = 0; i < pActionList.Count; ++i)
            {
                if (pActionList[i].GetActionType() == pType)
                {
                    return pActionList[i];
                }
            }

            return null;
        }

        public static IKeyInputVO GetIKeyInputVO(this List<IKeyInputVO> pKeys, KeyCode pKeyCode)
        {
            for (int i = 0; i < pKeys.Count; ++i)
            {
                if (pKeys[i].GetKeyCode() == pKeyCode)
                {
                    return pKeys[i];
                }
            }

            return null;
        }

        public static IMouseInputVO GetIMouseInputVO(this List<IMouseInputVO> pMouseInputs, Custom.Inputs.InputsManager.MouseButton pMouseButton)
        {
            for (int i = 0; i < pMouseInputs.Count; ++i)
            {
                if (pMouseInputs[i].GetMouseButton() == pMouseButton)
                {
                    return pMouseInputs[i];
                }
            }

            return null;
        }
    }
}
