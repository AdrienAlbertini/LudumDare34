using UnityEngine;
using System.Collections;

public class CameraSwitching : MonoBehaviour {

	public GameObject[] Cameras;
	int AcutalCam = 0;
	// Use this for initialization
	void Start () {
				int tmp = 0;
		foreach (GameObject camera in Cameras)
		{
			if (tmp == AcutalCam)
			{
				camera.SetActive(true);
			}
			else
			{
				camera.SetActive(false);
			}
			tmp++;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SwitchNext()
	{
		AcutalCam += 1;
		int tmp = 0;
		foreach (GameObject camera in Cameras)
		{
			if (tmp == AcutalCam)
			{
				camera.SetActive(true);
			}
			else
			{
				camera.SetActive(false);
			}
			tmp++;
		}	
	}
	
	void SwitchCamera()
	{
		int tmp = 0;
		foreach (GameObject camera in Cameras)
		{
			if (tmp == AcutalCam)
			{
				camera.SetActive(true);
			}
			else
			{
				camera.SetActive(false);
			}
			tmp++;
		}
	}
}
