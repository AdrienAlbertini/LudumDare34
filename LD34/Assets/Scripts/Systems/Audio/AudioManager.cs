using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    public AudioSource audioSource;
    public AudioSource musicSource;
    public List<AudioPair> audioList = new List<AudioPair>();
    public List<AudioPair> musicList = new List<AudioPair>();
    [HideInInspector]
    public string playingMusic = "";

    // Use this for initialization
    void Start()
    {
        this.audioSource = Instance.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.musicSource.isPlaying == false)
            this.playingMusic = "";
    }

    public void PlaySound(string soundName, float volumeScale = 1.0f)
    {
       for (int i = 0; i < this.audioList.Count; ++i)
        {
            if (this.audioList[i].audioName == soundName)
            {
                this.audioSource.PlayOneShot(this.audioList[i].audioClip, volumeScale);
                return;
            }
        }
    }

    public void PlayMusic(string musicName)
    {
        for (int i = 0; i < this.audioList.Count; ++i)
        {
            if (this.musicList[i].audioName == musicName)
            {
                this.musicSource.clip = this.musicList[i].audioClip;
                this.playingMusic = this.musicList[i].audioName;
                this.musicSource.Play();
                return;
            }
        }
    }
}
