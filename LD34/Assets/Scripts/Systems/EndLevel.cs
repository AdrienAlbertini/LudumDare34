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
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            coll.gameObject.SetActive(false);

            GameObject cc = GameObject.FindWithTag("CharacterController");

            foreach (Platformer2DUserControl p in cc.GetComponentsInChildren<Platformer2DUserControl>(false))
            {
                p.calculateIfIcanGrow();
            }

            //foreach (Rigidbody2D rb in cc.GetComponentsInChildren<Rigidbody2D>(false))
            //{
            //    rb.gravityScale = 3;
            //}

            ++this._playerCount;
            if (this._playerCount == 2)
            {
                Debug.Log("Switch to next lvl");
                LevelsManager.Instance.SwitchToNextScene();
            }
        }
    }
}
