using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : SingletonBehaviour<MonoBehaviour>
{
    public List<AudioPair> audioList = new List<AudioPair>();
    private AudioSource _audioSource;

    // Use this for initialization
    void Start()
    {
        this._audioSource = Instance.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
