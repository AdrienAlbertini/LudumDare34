using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public GameObject holder;
    public GameObject selectLvl;
    public Button continueButton;
    public bool isAllLevels = false;
    public InputField lvlLoaderField;
    public int startSceneId = 2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1) && this.selectLvl.activeSelf)
        {
            AudioManager.Instance.PlaySound("MenuJoystickPush", 1.0f);
            this.BackToMainMenu();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        this.holder.SetActive(true);
        this.selectLvl.SetActive(false);
    }

    public void SelectLevel()
    {
        this.holder.SetActive(false);
        this.selectLvl.SetActive(true);

        //GETCHILD 0 = backbutton
        if (this.isAllLevels)
        {
            this.lvlLoaderField.gameObject.SetActive(false);
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

    public void LvlLoaderEndEdit()
    {
        this.GoToLevel(this.lvlLoaderField.text);
    }

    public void GoToLevel(string scene)
    {
        LevelsManager.Instance.LoadScene(scene);
    }

    public void NewGame()
    {
        LevelsManager.Instance.LoadScene(this.startSceneId);
        SaveManager.data.levelID = this.startSceneId;
    }

    public void ContinueGame()
    {
        LevelsManager.Instance.LoadScene(SaveManager.data.levelID);
    }

    public void Play()
    {

    }
}
