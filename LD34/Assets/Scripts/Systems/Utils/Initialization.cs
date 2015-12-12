using UnityEngine;
using System.Collections;

public class Initialization : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        ConfigurationData.Instance.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
