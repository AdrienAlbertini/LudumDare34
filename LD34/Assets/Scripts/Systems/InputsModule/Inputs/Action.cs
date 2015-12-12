using UnityEngine;
using System.Collections.Generic;
using System;

namespace Custom.Inputs.Actions
{
    #region "Enums"

    public enum ActionType
    {
        Undefined,
        Jump,
        Restart,
        Left,
        Right
    }

    #endregion

    [System.Serializable]
    public class Action
    {
        [SerializeField]
        private ActionType _type = ActionType.Undefined;

        [SerializeField]
        private List<KeyCode> _linkedKeys = new List<KeyCode>();

        [SerializeField]
        private List<Custom.Inputs.InputsManager.MouseButton> _linkedMouseButtons = new List<Custom.Inputs.InputsManager.MouseButton>();

        #region "Accessors and Mutators"

        public ActionType Type
        {
            get { return this._type; }
            set { this._type = value; }
        }

        public List<KeyCode> LinkedKeys
        {
            get { return this._linkedKeys; }
        }

        public List<Custom.Inputs.InputsManager.MouseButton> LinkedMouseButtons
        {
            get { return this._linkedMouseButtons; }
        }

        #endregion

        public void Init(ActionType pType)
        {
            this._type = pType;
        }

        public bool IsLinkedKeys()
        {
            return this._linkedKeys.Count != 0;
        }

        public bool IsLinkedMouseButtons()
        {
            return this._linkedMouseButtons.Count != 0;
        }

        public bool IsKeyInAction(KeyCode pKey)
        {
            if (this.IsLinkedKeys())
            {
                for (int i = 0; i < this._linkedKeys.Count; ++i)
                {
                    if (this._linkedKeys[i] == pKey)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsMouseButtonInAction(Custom.Inputs.InputsManager.MouseButton pMouseButton)
        {
            if (this.IsLinkedMouseButtons())
            {
                for (int i = 0; i < this._linkedMouseButtons.Count; ++i)
                {
                    if (this._linkedMouseButtons[i] == pMouseButton)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
