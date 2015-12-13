using UnityEngine;
using System.Collections;

public class ToggleBumper : MonoBehaviour, PressurePlateListener
{
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPressurePlateTriggered(PressurePlate sender)
    {
        this.GetComponent<Bumper>().bumperOn = true;
    }
}
