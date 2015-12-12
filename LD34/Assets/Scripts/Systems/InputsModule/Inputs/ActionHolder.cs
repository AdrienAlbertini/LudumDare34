using UnityEngine;
using System.Collections.Generic;
using System;

namespace Custom.Inputs.Actions
{
    [System.Serializable]
    public class ActionHolder : ScriptableObject
    {
        [SerializeField]
        public List<Custom.Inputs.Actions.Action> actions = new List<Custom.Inputs.Actions.Action>();

        public Custom.Inputs.Actions.Action GetActionByType(Custom.Inputs.Actions.ActionType pActionType)
        {
            Custom.Inputs.Actions.Action action = null;

            for (int i = 0; i < actions.Count; ++i)
            {
                if (actions[i].Type == pActionType)
                {
                    action = actions[i];
                    break;
                }
            }

            return action;
        }

        public void BindAction(Custom.Inputs.Actions.ActionType p_actionType, Custom.Inputs.InputsManager.MouseButton pMouseButton)
        {
            Custom.Inputs.Actions.Action action = this.GetActionByType(p_actionType);
            Custom.Inputs.Actions.Action checkAction = null;

            if (action != null)
            {
                action.LinkedMouseButtons.Remove(pMouseButton);
                action.LinkedMouseButtons.Add(pMouseButton);

                foreach (Custom.Inputs.Actions.ActionType actionType in Enum.GetValues(typeof(Custom.Inputs.Actions.ActionType)))
                {
                    if (actionType != p_actionType && (checkAction = this.GetActionByType(actionType)) != null)
                    {
                        checkAction.LinkedMouseButtons.Remove(pMouseButton);
                    }
                }
            }
        }

        public void BindAction(Custom.Inputs.Actions.ActionType pActionType, KeyCode pKeyCode)
        {
            Custom.Inputs.Actions.Action action = this.GetActionByType(pActionType);
            Custom.Inputs.Actions.Action checkAction = null;

            if (action != null)
            {
                action.LinkedKeys.Remove(pKeyCode);
                action.LinkedKeys.Add(pKeyCode);

                foreach (Custom.Inputs.Actions.ActionType actionType in Enum.GetValues(typeof(Custom.Inputs.Actions.ActionType)))
                {
                    if (actionType != pActionType && (checkAction = this.GetActionByType(actionType)) != null)
                    {
                        checkAction.LinkedKeys.Remove(pKeyCode);
                    }
                }
            }
        }
    }
}
