using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
	public GameObject[] Cameras;
	int current = 0;
	// Use this for initialization
	void Start () {
		int tmp = 0;
		foreach (GameObject cam in Cameras)
		{
			if (tmp == current)
				cam.SetActive(true);
			else 
				cam.SetActive(false);
			tmp++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SwitchNext()
	{
		int tmp = 0;
		current += 1;
		foreach (GameObject cam in Cameras)
		{
			if (tmp == current)
				cam.SetActive(true);
			else 
				cam.SetActive(false);
			tmp++;
		}
	}
	
	void Switch(int nb)
	{
		int tmp = 0;
		current = nb;
		foreach (GameObject cam in Cameras)
		{
			if (tmp == current)
				cam.SetActive(true);
			else 
				cam.SetActive(false);
			tmp++;
		}
	}
}
