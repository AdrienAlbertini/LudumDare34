using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    public GameObject holder;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void toMainMenu()
    {
        LevelsManager.Instance.LoadScene("MainMenu");
    }

    public void Resume()
    {
        this.holder.SetActive(false);
        Time.timeScale = 1;
    }

    void OnGUI()
    {
        GUILayout.Label("Press Enter To Start Game");
        if (Event.current.Equals(Event.KeyboardEvent("Escape")))
        {
            this.holder.SetActive(true);
            Time.timeScale = 0;
        }

    }
}
