using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject PlayerPrefab;

    void Awake()
    {
        if (this.PlayerPrefab == null)
            Debug.LogError("No player prefab selected");
        else
        {
            Instantiate(this.PlayerPrefab, this.transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButtonUp("Restart"))
        {
            LevelsManager.Instance.ReloadScene();
        }
    }
}
