using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : SingletonBehaviour<AudioManager>
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

    public void PlaySound(string soundName, float volumeScale = 1.0f)
    {
       for (int i = 0; i < this.audioList.Count; ++i)
        {
            if (this.audioList[i].audioName == soundName)
            {
                this._audioSource.PlayOneShot(this.audioList[i].audioClip, volumeScale);
                return;
            }
        }
    }
}
