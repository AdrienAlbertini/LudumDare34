using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] 
    private GameObject PlayerPrefab;
    [SerializeField]
    private GameObject SpawnA;
    [SerializeField]
    private GameObject SpawnB;
    private GameObject PlayerA;
    private GameObject PlayerB;
    void Awake()
    {

        if (this.PlayerPrefab == null)
            Debug.LogError("No player prefab selected");
        else
        {
            GameObject item = Instantiate(this.PlayerPrefab, this.transform.position, Quaternion.identity) as GameObject;
            PlayerA = item.transform.FindChild("CharacterPLayerA(Clone)").gameObject;
            PlayerB = item.transform.FindChild("CharacterPLayerB(Clone)").gameObject;
            PlayerA.transform.position = SpawnA.transform.position;
            PlayerB.transform.position = SpawnB.transform.position;

            Custom.Inputs.InputsManager.Instance.BindReceiveSingleAction(Custom.Inputs.Actions.ActionType.Restart, this.OnRestart);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            LevelsManager.Instance.ReloadScene();
        }
    }

    void OnRestart(Custom.Inputs.VO.IActionVO pAction)
    {
        if (pAction.GetInputState() == Custom.Inputs.InputsManager.InputState.Up)
        {
            LevelsManager.Instance.ReloadScene();
        }
    }
}
