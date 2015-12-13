using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public GameObject holder;
    public GameObject selectLvl;
    public bool isAllLevels = false;
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

        //GETCHILD 0 = backbutton
        if (this.isAllLevels == true)
        {
            Button[] buttons = this.selectLvl.transform.GetComponentsInChildren<Button>();

            for (int i = 0; i < buttons.Length; ++i)
                buttons[i].interactable = true;
        }
        else
        {
            for (int i = 0; i < SaveManager.data.levelID; i++)
            {
                this.selectLvl.transform.GetChild(i + 1).GetComponent<Button>().interactable = true;
            }
        }
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
