using UnityEngine;
using System.Collections;

public class ControlsMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Continue()
    {
        LevelsManager.Instance.SwitchToNextScene();
    }
}
