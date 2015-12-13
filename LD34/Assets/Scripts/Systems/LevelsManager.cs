﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelsManager : SingletonBehaviour<LevelsManager>
{
    public List<string> scenes = new List<string>();
    private int _currentSceneId = 0;
    private bool _isNextSceneLoaded = false;
    private AsyncOperation _asyncOp;
    private bool _isLoading = false;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Debug.Log(Application.persistentDataPath);
        SceneFader.Instance.fadeOver += ScenefadeOver;
    }

    private bool _fadeIsOver;
    private void ScenefadeOver(object sender, System.EventArgs e)
    {
        this._fadeIsOver = true;
    }

    void OnLevelWasLoaded()
    {
        this._isLoading = false;
    }

    private IEnumerator _StartLoad(string scene, bool fade, float timer = 0.0f)
    {
        yield return new WaitForSeconds(timer);
        this._isLoading = true;
        if (fade)
        {
            this._fadeIsOver = false;
            SceneFader.Instance.EndScene();
            while (this._fadeIsOver == false)
            {
                yield return new WaitForFixedUpdate();
            }
            //yield return new WaitForSeconds(SceneFader.Instance.fadeSpeed);
        }
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        while (this._isLoading)
            yield return new WaitForFixedUpdate();
        if (fade)
        {
            SceneFader.Instance.StartScene();
            //yield return new WaitForSeconds(SceneFader.Instance.fadeSpeed);
        }
        if (scene.StartsWith("Lvl"))
        {
            SaveManager.data.levelID = this._currentSceneId;
            SaveManager.instance.save();
        }
    }

    public void LoadScene(int sceneId, bool fade = true, float timer = 0.0f)
    {
        if (sceneId >= 0 && sceneId < this.scenes.Count)
        {
            this._currentSceneId = sceneId;
            StartCoroutine(this._StartLoad(this.scenes[sceneId], fade, timer));
        }
    }

    public void LoadScene(string scene, bool fade = true, float timer = 0.0f)
    {
        if (this.scenes.Contains(scene))
        {
            this._currentSceneId = this.scenes.IndexOf(scene);
            StartCoroutine(this._StartLoad(scene, fade, timer));
        }
    }

    public void ReloadScene(bool fade = true, float timer = 0.0f)
    {
        this.LoadScene(this._currentSceneId, true, timer);
    }

    public void SwitchToNextScene()
    {
        if (this._currentSceneId >= 0 && this._currentSceneId < this.scenes.Count)
        {
            ++this._currentSceneId;
            this.LoadScene(this._currentSceneId);
        }
    }
}
