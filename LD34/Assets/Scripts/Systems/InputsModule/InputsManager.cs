using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Custom.Inputs.VO;

namespace Custom.Inputs
{
    public class InputsManager : SingletonBehaviour<InputsManager>
    {
        #region "Enums & Nested Classes"

        public enum MouseButton
        {
            None = -1,
            LeftClick = 0,
            RightClick = 1,
            MiddleClick = 2
        }

        public enum InputState
        {
            Undefined,
            Down,
            Up,
            Maintained
        }

        private class InputVO : IInputVO
        {
            public int inputsCount = 0;
            public InputsManager.InputState inputState = InputsManager.InputState.Undefined;
            public float mouseWheel = 0.0f;

            public float GetMouseWheel()
            {
                return this.mouseWheel;
            }

            public int GetInputsCount()
            {
                return this.inputsCount;
            }

            public InputsManager.InputState GetInputState()
            {
                return this.inputState;
            }
        }

        private class ActionVO : InputVO, IActionVO
        {
            public Custom.Inputs.Actions.ActionType action = Custom.Inputs.Actions.ActionType.Undefined;

            public Custom.Inputs.Actions.ActionType GetActionType()
            {
                return this.action;
            }
        }

        private class KeyInputVO : InputVO, IKeyInputVO
        {
            public KeyCode keyCode = 0;

            public KeyCode GetKeyCode()
            {
                return this.keyCode;
            }
        }

        private class MouseInputVO : InputVO, IMouseInputVO
        {
            public InputsManager.MouseButton mouseButton = InputsManager.MouseButton.LeftClick;

            public InputsManager.MouseButton GetMouseButton()
            {
                return this.mouseButton;
            }
        }

        #endregion

        #region "Events & Delegates"

        public delegate void ReceiveSingleAction(IActionVO pAction);
        public delegate void ReceiveActions(List<IActionVO> pActions);
        public delegate void ReceiveKeyInputs(List<IKeyInputVO> pInputs);
        public delegate void ReceiveMouseInputs(List<IMouseInputVO> pInputs);
        public delegate void ReceiveMouseWheel(float pMouseWheel);

        private static event ReceiveActions OnReceiveActions = null;
        private static event ReceiveKeyInputs OnReceiveKeyInputs = null;
        private static event ReceiveMouseInputs OnReceiveMouseInputs = null;
        private static event ReceiveMouseInputs OnReceiveSingleAction = null;
        private static event ReceiveMouseWheel OnReceiveMouseWheel = null;

        #endregion

        #region "Privates members"

        private Custom.Inputs.Actions.ActionHolder _actionHolder = ScriptableObject.CreateInstance<Custom.Inputs.Actions.ActionHolder>();

        private Dictionary<Custom.Inputs.Actions.ActionType, Custom.Inputs.Actions.Action> _actions = new Dictionary<Custom.Inputs.Actions.ActionType, Custom.Inputs.Actions.Action>();
        private List<ActionVO> _actionsVO = new List<ActionVO>();
        private List<KeyInputVO> _keysVO = new List<KeyInputVO>();
        private List<MouseInputVO> _mouseVO = new List<MouseInputVO>();

        private Dictionary<Custom.Inputs.Actions.ActionType, ReceiveSingleAction> _singleActionCallbacks = new Dictionary<Actions.ActionType, ReceiveSingleAction>();
        private Dictionary<Custom.Inputs.Actions.ActionType, ActionVO> _actionsInputs = new Dictionary<Custom.Inputs.Actions.ActionType, ActionVO>();
        private Dictionary<KeyCode, KeyInputVO> _keysInputsVO = new Dictionary<KeyCode, KeyInputVO>();
        private Dictionary<MouseButton, MouseInputVO> _mouseInputsVO = new Dictionary<MouseButton, MouseInputVO>();

        private Dictionary<KeyCode, bool> _keysActivations = new Dictionary<KeyCode, bool>();
        private Dictionary<MouseButton, bool> _mouseActivations = new Dictionary<MouseButton, bool>();

        private List<ActionVO> _sendActionsVO = new List<ActionVO>();
        private List<KeyInputVO> _sendKeysVO = new List<KeyInputVO>();
        private List<MouseInputVO> _sendMouseVO = new List<MouseInputVO>();

        private float _mouseWheel = 1.0f;

        #endregion

        #region "Public members"
        public string mouseWheelAxisName = "Mouse ScrollWheel";
        #endregion

        void Start()
        {
            Array keycodes = Enum.GetValues(typeof(KeyCode));
            Array mouseButtons = Enum.GetValues(typeof(MouseButton));

            foreach (KeyCode key in keycodes)
            {
                KeyInputVO vo = new KeyInputVO();

                vo.keyCode = key;

                if (!this._keysVO.Contains(vo))
                {
                    this._keysVO.Add(vo);
                }
                if (!this._keysInputsVO.ContainsKey(key))
                {
                    this._keysInputsVO.Add(key, vo);
                }
                if (!this._keysActivations.ContainsKey(key))
                {
                    this._keysActivations.Add(key, false);
                }
            }

            foreach (MouseButton button in mouseButtons)
            {
                MouseInputVO vo = new MouseInputVO();

                vo.mouseButton = button;

                if (!this._mouseVO.Contains(vo))
                {
                    this._mouseVO.Add(vo);
                }
                if (!this._mouseInputsVO.ContainsKey(button))
                {
                    this._mouseInputsVO.Add(button, vo);
                }
                if (!this._mouseActivations.ContainsKey(button))
                {
                    this._mouseActivations.Add(button, false);
                }
            }
        }

        void Update()
        {
            Array keycodes = Enum.GetValues(typeof(KeyCode));
            Array mouseButtons = Enum.GetValues(typeof(MouseButton));

            this._mouseWheel = Input.GetAxis(this.mouseWheelAxisName);

            foreach (KeyCode key in keycodes)
            {
                this.CheckUpdateKeyCode(key);
            }

            foreach (MouseButton button in mouseButtons)
            {
                this.CheckUpdateMouseButton(button);
            }

            if (OnReceiveMouseWheel != null && this._mouseWheel != 0.0f)
            {
                OnReceiveMouseWheel(this._mouseWheel);
            }
        }

        void FixedUpdate()
        {
            Array keycodes = Enum.GetValues(typeof(KeyCode));
            Array mouseButtons = Enum.GetValues(typeof(MouseButton));

            foreach (KeyCode key in keycodes)
            {
                if (this._keysActivations.ContainsKey(key)
                    && this._keysActivations[key] == true && this._keysInputsVO.ContainsKey(key))
                {
                    KeyInputVO vo = this._keysInputsVO[key];
                    Custom.Inputs.Actions.Action action = null;

                    vo.mouseWheel = this._mouseWheel;

                    if ((action = this.FindActionByKeyCode(key)) != null)
                    {
                        this._actionsInputs[action.Type].inputsCount = vo.inputsCount;
                        this._actionsInputs[action.Type].inputState = vo.inputState;
                        this._actionsInputs[action.Type].mouseWheel = this._mouseWheel;
                        this._sendActionsVO.Add(this._actionsInputs[action.Type]);
                    }

                    this._sendKeysVO.Add(vo);
                }
            }

            foreach (MouseButton button in mouseButtons)
            {
                if (this._mouseActivations.ContainsKey(button)
                    && this._mouseActivations[button] == true && this._mouseInputsVO.ContainsKey(button))
                {
                    MouseInputVO vo = this._mouseInputsVO[button];
                    Custom.Inputs.Actions.Action action = null;

                    vo.mouseWheel = this._mouseWheel;

                    if ((action = this.FindActionByMouseButton(button)) != null)
                    {
                        this._actionsInputs[action.Type].inputsCount = vo.inputsCount;
                        this._actionsInputs[action.Type].inputState = vo.inputState;
                        this._actionsInputs[action.Type].mouseWheel = this._mouseWheel;
                        this._sendActionsVO.Add(this._actionsInputs[action.Type]);
                    }

                    this._sendMouseVO.Add(vo);
                }
            }

            List<IActionVO> actionsVOInterfaces = this._sendActionsVO.ConvertAll(o => (IActionVO)o);
            List<IKeyInputVO> keysVOInterfaces = this._sendKeysVO.ConvertAll(o => (IKeyInputVO)o);
            List<IMouseInputVO> mouseVOInterfaces = this._sendMouseVO.ConvertAll(o => (IMouseInputVO)o);

            if (actionsVOInterfaces.Count > 0)
            {
                if (OnReceiveActions != null)
                {
                    OnReceiveActions(actionsVOInterfaces);
                }

                for (int i = 0; i < actionsVOInterfaces.Count; ++i)
                {
                    Custom.Inputs.Actions.ActionType actionType = actionsVOInterfaces[i].GetActionType();

                    if (this._singleActionCallbacks.ContainsKey(actionType)
                        && this._singleActionCallbacks[actionType] != null)
                    {
                        this._singleActionCallbacks[actionType](actionsVOInterfaces[i]);
                    }
                }
            }
            if (OnReceiveKeyInputs != null && keysVOInterfaces.Count > 0)
            {
                OnReceiveKeyInputs(keysVOInterfaces);
            }
            if (OnReceiveMouseInputs != null && mouseVOInterfaces.Count > 0)
            {
                OnReceiveMouseInputs(mouseVOInterfaces);
            }

            this.ResetInputs();
        }

        private Custom.Inputs.Actions.Action FindActionByKeyCode(KeyCode pKey)
        {
            Array actionTypes = Enum.GetValues(typeof(Custom.Inputs.Actions.ActionType));

            foreach (Custom.Inputs.Actions.ActionType types in actionTypes)
            {
                if (this._actions.ContainsKey(types))
                {
                    Custom.Inputs.Actions.Action action = this._actions[types];

                    if (action.IsKeyInAction(pKey))
                    {
                        return action;
                    }
                }
            }

            return null;
        }

        private Custom.Inputs.Actions.Action FindActionByMouseButton(MouseButton pMouseButton)
        {
            Array actionTypes = Enum.GetValues(typeof(Custom.Inputs.Actions.ActionType));

            foreach (Custom.Inputs.Actions.ActionType types in actionTypes)
            {
                if (this._actions.ContainsKey(types))
                {
                    Custom.Inputs.Actions.Action action = this._actions[types];

                    if (action.IsMouseButtonInAction(pMouseButton))
                    {
                        return action;
                    }
                }
            }

            return null;
        }

        private void CheckUpdateKeyCode(KeyCode pKey)
        {
            if (Input.GetKey(pKey))
            {
                if (Input.GetKeyDown(pKey))
                {
                    this._keysActivations[pKey] = true;
                    ++this._keysInputsVO[pKey].inputsCount;
                    this._keysInputsVO[pKey].inputState = InputState.Down;
                }
                else if (!Input.GetKeyUp(pKey))
                {
                    this._keysActivations[pKey] = true;
                    ++this._keysInputsVO[pKey].inputsCount;
                    this._keysInputsVO[pKey].inputState = InputState.Maintained;
                }
            }
            else if (Input.GetKeyUp(pKey))
            {
                this._keysActivations[pKey] = true;
                this._keysInputsVO[pKey].inputsCount = 0;
                this._keysInputsVO[pKey].inputState = InputState.Up;
            }
        }

        private void CheckUpdateMouseButton(MouseButton pMouseButton)
        {
            if (pMouseButton != MouseButton.None)
            {
                if (Input.GetMouseButton((int)pMouseButton))
                {
                    if (Input.GetMouseButtonDown((int)pMouseButton))
                    {
                        this._mouseActivations[pMouseButton] = true;
                        ++this._mouseInputsVO[pMouseButton].inputsCount;
                        this._mouseInputsVO[pMouseButton].inputState = InputState.Down;
                    }
                    else if (!Input.GetMouseButtonUp((int)pMouseButton))
                    {
                        this._mouseActivations[pMouseButton] = true;
                        ++this._mouseInputsVO[pMouseButton].inputsCount;
                        this._mouseInputsVO[pMouseButton].inputState = InputState.Maintained;
                    }
                }
                else if (Input.GetMouseButtonUp((int)pMouseButton))
                {
                    this._mouseActivations[pMouseButton] = true;
                    this._mouseInputsVO[pMouseButton].inputsCount = 0;
                    this._mouseInputsVO[pMouseButton].inputState = InputState.Up;
                }
            }
        }

        private void ResetInputs()
        {
            Array keycodes = Enum.GetValues(typeof(KeyCode));
            Array mouseButtons = Enum.GetValues(typeof(MouseButton));

            foreach (KeyCode key in keycodes)
            {
                this._keysActivations[key] = false;
                this._keysInputsVO[key].inputState = InputState.Undefined;
            }

            foreach (MouseButton button in mouseButtons)
            {
                this._mouseActivations[button] = false;
                this._mouseInputsVO[button].inputState = InputState.Undefined;
            }

            for (int i = 0; i < this._actionsVO.Count; ++i)
            {
                this._actionsVO[i].inputsCount = 0;
                this._actionsVO[i].inputState = InputState.Undefined;
            }

            this._sendActionsVO.Clear();
            this._sendKeysVO.Clear();
            this._sendMouseVO.Clear();
        }

        public void LoadActions(string pActionsAssetPath)
        {
            this._actionHolder = null;
            this._actions.Clear();
            this._actionsVO.Clear();
            this._actionsInputs.Clear();

            if ((this._actionHolder = Resources.Load<Custom.Inputs.Actions.ActionHolder>(pActionsAssetPath)) != null)
            {
                for (int i = 0; i < this._actionHolder.actions.Count; ++i)
                {
                    if (this._actionHolder.actions[i].Type != Actions.ActionType.Undefined)
                    {
                        if (this._actions.ContainsKey(this._actionHolder.actions[i].Type))
                        {
                            this._actions[this._actionHolder.actions[i].Type] = this._actionHolder.actions[i];
                        }
                        else
                        {
                            this._actions.Add(this._actionHolder.actions[i].Type, this._actionHolder.actions[i]);
                        }
                    }
                }

                foreach (Custom.Inputs.Actions.ActionType actionType in Enum.GetValues(typeof(Custom.Inputs.Actions.ActionType)))
                {
                    if (this._actions.ContainsKey(actionType))
                    {
                        Custom.Inputs.Actions.Action action = this._actions[actionType];
                        ActionVO vo = new ActionVO();

                        vo.action = action.Type;

                        if (!this._actionHolder.actions.Contains(action))
                        {
                            this._actionHolder.actions.Add(action);
                        }
                        if (!this._actionsVO.Contains(vo))
                        {
                            this._actionsVO.Add(vo);
                        }
                        if (!this._actionsInputs.ContainsKey(actionType))
                        {
                            this._actionsInputs.Add(actionType, vo);
                        }
                    }
                }
            }
        }

        public void BindReceiveSingleAction(Custom.Inputs.Actions.ActionType pActionType, ReceiveSingleAction pCallback)
        {
            if (pCallback != null)
            {
                if (!this._singleActionCallbacks.ContainsKey(pActionType))
                {
                    this._singleActionCallbacks.Add(pActionType, null);
                }
                this._singleActionCallbacks[pActionType] += pCallback;
            }
        }

        public void UnBindReceiveSingleAction(Custom.Inputs.Actions.ActionType pActionType, ReceiveSingleAction pCallback)
        {
            if (pCallback != null && this._singleActionCallbacks[pActionType] != null)
            {
                this._singleActionCallbacks[pActionType] -= pCallback;
            }
        }

        public void BindAction(Custom.Inputs.Actions.ActionType pActionType, MouseButton pMouseButton)
        {
            Custom.Inputs.Actions.Action action = this._actions[pActionType];

            action.LinkedMouseButtons.Remove(pMouseButton);
            action.LinkedMouseButtons.Add(pMouseButton);

            foreach (Custom.Inputs.Actions.ActionType actionType in Enum.GetValues(typeof(Custom.Inputs.Actions.ActionType)))
            {
                if (actionType != pActionType && this._actions.ContainsKey(actionType))
                {
                    action = this._actions[actionType];
                    action.LinkedMouseButtons.Remove(pMouseButton);
                }
            }
        }

        public void BindAction(Custom.Inputs.Actions.ActionType pActionType, KeyCode pKeyCode)
        {
            Custom.Inputs.Actions.Action action = this._actions[pActionType];

            action.LinkedKeys.Remove(pKeyCode);
            action.LinkedKeys.Add(pKeyCode);

            foreach (Custom.Inputs.Actions.ActionType actionType in Enum.GetValues(typeof(Custom.Inputs.Actions.ActionType)))
            {
                if (actionType != pActionType && this._actions.ContainsKey(actionType))
                {
                    action = this._actions[actionType];
                    action.LinkedKeys.Remove(pKeyCode);
                }
            }
        }

        public void BindReceiveActionsCallback(ReceiveActions pCallback)
        {
            OnReceiveActions -= pCallback;
            OnReceiveActions += pCallback;
        }

        public void UnBindReceiveActionsCallback(ReceiveActions pCallback)
        {
            OnReceiveActions -= pCallback;
        }

        public void BindReceiveKeysCallback(ReceiveKeyInputs pCallback)
        {
            OnReceiveKeyInputs -= pCallback;
            OnReceiveKeyInputs += pCallback;
        }

        public void UnBindReceiveKeysCallback(ReceiveKeyInputs pCallback)
        {
            OnReceiveKeyInputs -= pCallback;
        }

        public void BindReceiveMouseCallback(ReceiveMouseInputs pCallback)
        {
            OnReceiveMouseInputs -= pCallback;
            OnReceiveMouseInputs += pCallback;
        }

        public void UnBindReceiveMouseCallback(ReceiveMouseInputs pCallback)
        {
            OnReceiveMouseInputs -= pCallback;
        }

        public void BindReceiveMouseWheelCallback(ReceiveMouseWheel pCallback)
        {
            OnReceiveMouseWheel -= pCallback;
            OnReceiveMouseWheel += pCallback;
        }

        public void UnBindReceiveMouseWheelCallback(ReceiveMouseWheel pCallback)
        {
            OnReceiveMouseWheel -= pCallback;
        }

        #region "Accessors & Mutators"

        public Custom.Inputs.Actions.ActionHolder InputsActionHolder
        {
            get { return this._actionHolder; }
            set { this._actionHolder = value; }
        }

        #endregion
    }
}
