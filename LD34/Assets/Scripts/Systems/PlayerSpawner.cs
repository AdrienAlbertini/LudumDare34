using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject PlayerPrefab;

    void Awake()
    {
        if (this.PlayerPrefab == null)
            Debug.LogError("No player prefab selected");
        else
        {
            Instantiate(this.PlayerPrefab, this.transform.position, Quaternion.identity);
            Custom.Inputs.InputsManager.Instance.BindReceiveSingleAction(Custom.Inputs.Actions.ActionType.Restart, this.OnRestart);
        }
    }

    void FixedUpdate()
    {
        //if (Input.GetButtonUp("Restart"))
        //{
        //    LevelsManager.Instance.ReloadScene();
        //}
    }

    void OnRestart(Custom.Inputs.VO.IActionVO pAction)
    {
        if (pAction.GetInputState() == Custom.Inputs.InputsManager.InputState.Up)
        {
            LevelsManager.Instance.ReloadScene();
        }
    }
}
