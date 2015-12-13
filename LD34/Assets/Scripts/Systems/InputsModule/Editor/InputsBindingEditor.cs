using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class InputsBindingEditor : EditorWindow
{
    public static string actionsAssetCreationPath = "Assets/Resources/actionsAsset.asset";

    private Vector2 _scrollPos;
    private Vector2 _pos;
    private Custom.Inputs.Actions.ActionHolder _actionHolder = null;

    [MenuItem("Custom/Inputs Binding Editor")]
    public static void Init()
    {
        EditorWindow window = EditorWindow.GetWindow<InputsBindingEditor>("Inputs Binding Editor", true);

        window.minSize = new Vector2(200, 500);
    }

    void OnEnable()
    {
        ConfigurationData.Instance.Initialize();
        
        if (this._actionHolder == null)
        {
            if ((this._actionHolder = ConfigurationData.Instance.actionHolder) == null)
            {
                Debug.Log("CreateActionHolder");

                this._actionHolder = CreateInstance<Custom.Inputs.Actions.ActionHolder>();

                AssetDatabase.CreateAsset(this._actionHolder, actionsAssetCreationPath);
                EditorUtility.SetDirty(this._actionHolder);
                AssetDatabase.SaveAssets();
            }
        }
    }

    private void OnDisable()
    {
        AssetDatabase.SaveAssets();
    }

    private void OnGUI()
    {
        this._pos = EditorGUILayout.BeginScrollView(this._pos, false, true);

        if (this._actionHolder != null
            && this._actionHolder.actions != null)
        {
            for (int i = 0; i < this._actionHolder.actions.Count; ++i)
            {
                this._actionHolder.actions[i].OnGUI(this._actionHolder, i);
                EditorGUILayout.Separator();
            }
            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Add Action", GUILayout.Width(200f)))
                {
                    Custom.Inputs.Actions.Action newAction = new Custom.Inputs.Actions.Action();

                    newAction.Init(Custom.Inputs.Actions.ActionType.Undefined);
                    this._actionHolder.actions.Add(newAction);
                }
                if (GUILayout.Button("Remove Action", GUILayout.Width(200f)))
                {
                    if (this._actionHolder.actions.Count > 0)
                    {
                        this._actionHolder.actions.RemoveAt(this._actionHolder.actions.Count - 1);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Save", GUILayout.Width(200f)))
                {
                    EditorUtility.SetDirty(this._actionHolder);
                    AssetDatabase.SaveAssets();
                }
                if (GUILayout.Button("Reset", GUILayout.Width(200f)))
                {
                    this._actionHolder.actions.Clear();
                }

            }
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            this.OnEnable();
        }
        EditorGUILayout.EndScrollView();
    }
}
