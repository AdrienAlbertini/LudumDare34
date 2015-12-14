using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerStay2D(other);
    }

        void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CharacterManager cm = GameObject.FindWithTag("CharacterController").GetComponentInChildren<CharacterManager>();

            cm.KillPlayer(other.gameObject.GetComponentInChildren<Platformer2DUserControl>());
        }
    }
}
