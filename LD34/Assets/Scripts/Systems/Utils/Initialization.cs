using UnityEngine;
using System.Collections;

public class Initialization : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        ConfigurationData.Instance.Initialize();
        SaveManager.instance.load();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
