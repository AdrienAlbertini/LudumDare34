using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class EndLevel : MonoBehaviour
{

    private int _playerCount;

	void Awake()
    {
        this._playerCount = 0;
	}
	
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            coll.gameObject.SetActive(false);
            ++this._playerCount;
            if (this._playerCount == 2)
            {
                Debug.Log("Switch to next lvl");
                LevelsManager.Instance.SwitchToNextScene();
            }
        }
    }
}
