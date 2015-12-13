using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public GameObject holder;
    public GameObject selectLvl;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void quit()
    {
        Application.Quit();
    }

    public void backToMainMenu()
    {
        this.holder.SetActive(true);
        this.selectLvl.SetActive(false);
    }

    public void selectLevel()
    {
        this.holder.SetActive(false);
        this.selectLvl.SetActive(true);
    }

    public void goToLevel(string scene)
    {
        LevelsManager.Instance.LoadScene(scene);
    }

    public void continueGame()
    {
        LevelsManager.Instance.LoadScene(SaveManager.data.levelID);
    }

    public void play()
    {

    }
}
