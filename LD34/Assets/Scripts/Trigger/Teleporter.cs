using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    [SerializeField]
    private GameObject TargetObject = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.TargetObject != null)
            other.transform.position = TargetObject.transform.position;
    }
}
