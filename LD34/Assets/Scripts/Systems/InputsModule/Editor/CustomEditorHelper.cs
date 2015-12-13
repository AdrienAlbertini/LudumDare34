using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class CustomEditorHelper
{
    public static void OnGUI(this Custom.Inputs.Actions.Action pAction, Custom.Inputs.Actions.ActionHolder pActionHolder, int pIndex)
    {
        GUILayout.Label("Action: " + pAction.Type, EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(90.0f));
        {
            Custom.Inputs.Actions.ActionType actionType = (Custom.Inputs.Actions.ActionType)EditorGUILayout.EnumPopup("Action Type:", pAction.Type);

            for (int i = 0; i < pActionHolder.actions.Count; ++i)
            {
                if (pIndex != i
                    && pActionHolder.actions[i].Type == actionType)
                {
                    pActionHolder.actions[i].Type = Custom.Inputs.Actions.ActionType.Undefined;
                }
            }

            pAction.Type = actionType;

            EditorGUILayout.Separator();

            GUILayout.Label("Keys", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Box");
            {
                for (int i = 0; i < pAction.LinkedKeys.Count; ++i)
                {
                    KeyCode key = (KeyCode)EditorGUILayout.EnumPopup("Linked Key: ", pAction.LinkedKeys[i]);

                    if (key != pAction.LinkedKeys[i])
                    {
                        if (pAction.LinkedKeys.Contains(key) == true)
                        {
                            for (int n = 0; n < pAction.LinkedKeys.Count; ++n)
                            {
                                if (n != i && pAction.LinkedKeys[n] == key)
                                {
                                    pAction.LinkedKeys[n] = KeyCode.None;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            pAction.LinkedKeys[i] = key;
                        }
                    }
                }

                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Add", GUILayout.Width(200.0f)))
                    {
                        pAction.LinkedKeys.Add(KeyCode.None);
                    }
                    if (GUILayout.Button("Remove", GUILayout.Width(200.0f)))
                    {
                        if (pAction.LinkedKeys.Count > 0)
                        {
                            pAction.LinkedKeys.RemoveAt(pAction.LinkedKeys.Count - 1);
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            {
                GUILayout.Label("Mouse Buttons", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical("Box");
                {
                    for (int i = 0; i < pAction.LinkedMouseButtons.Count; ++i)
                    {
                        Custom.Inputs.InputsManager.MouseButton button = (Custom.Inputs.InputsManager.MouseButton)EditorGUILayout.EnumPopup("Linked Button: ", pAction.LinkedMouseButtons[i]);

                        if (button != pAction.LinkedMouseButtons[i])
                        {
                            if (pAction.LinkedMouseButtons.Contains(button) == true)
                            {
                                for (int n = 0; n < pAction.LinkedMouseButtons.Count; ++n)
                                {
                                    if (n != i && pAction.LinkedMouseButtons[n] == button)
                                    {
                                        pAction.LinkedMouseButtons[n] = Custom.Inputs.InputsManager.MouseButton.None;
                                    }
                                }
                            }
                            else
                            {
                                pAction.LinkedMouseButtons[i] = button;
                            }
                        }
                    }

                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Add", GUILayout.Width(200.0f)))
                        {
                            pAction.LinkedMouseButtons.Add(Custom.Inputs.InputsManager.MouseButton.None);
                        }
                        if (GUILayout.Button("Remove", GUILayout.Width(200.0f)))
                        {
                            if (pAction.LinkedMouseButtons.Count > 0)
                            {
                                pAction.LinkedMouseButtons.RemoveAt(pAction.LinkedMouseButtons.Count - 1);
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }
}
