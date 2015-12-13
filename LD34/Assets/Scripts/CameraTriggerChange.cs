using UnityEngine;
using System.Collections;

public class CameraTriggerChange : MonoBehaviour {

	public CameraSwitcher switcher;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Trigger");
        if (other.transform.tag == "Player")
		{
			switcher.SwitchNext();
			this.transform.gameObject.SetActive(false);
		}
    }
}
