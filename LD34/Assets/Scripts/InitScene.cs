using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        LevelsManager.Instance.LoadScene("MainMenu", false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
