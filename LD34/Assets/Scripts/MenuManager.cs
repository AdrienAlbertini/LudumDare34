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
        Time.timeScale = 1;
        LevelsManager.Instance.LoadScene("MainMenu");
    }

    public void Resume()
    {
        this.holder.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("Escape")))
        {
            Cursor.visible = true;
            this.holder.SetActive(true);
            Time.timeScale = 0;
        }

    }
}
