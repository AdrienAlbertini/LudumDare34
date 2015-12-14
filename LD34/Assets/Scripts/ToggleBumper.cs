using UnityEngine;
using System.Collections;
using System;

public class ToggleBumper : MonoBehaviour, PressurePlateListener
{
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPressurePlateTriggerIn(PressurePlate sender)
    {
        this.GetComponent<Bumper>().bumperOn = true;
    }

    public void OnPressurePlateTriggerOut(PressurePlate sender)
    {
        this.GetComponent<Bumper>().bumperOn = false;
    }
}
